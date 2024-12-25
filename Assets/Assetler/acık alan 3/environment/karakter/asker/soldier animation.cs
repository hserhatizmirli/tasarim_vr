using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBotController : MonoBehaviour
{
    [Header("References")]
    public Transform player; // XR Origin karakteri
    public Animator animator;

    [Header("Vision Settings")]
    public float smallRadius = 25f;
    public float largeRadius = 50f;
    public float visionAngle = 100f; // Küre diliminin açýsý

    [Header("Animation States")]
    public string idleState = "Idle";
    public string firingState = "Firing";
    public string runningState = "Running";

    private bool isPlayerDetected = false;
    private static Vector3 lastFiringPosition;

    void Update()
    {
        // Küçük küre içinde oyuncuyu algýla
        if (IsPlayerInVisionArea())
        {
            isPlayerDetected = true;
            TriggerFiring();
        }
        else
        {
            isPlayerDetected = false;
            TriggerIdle();
        }

        // Büyük küre içinde diðer botlarý tetikle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(firingState))
        {
            lastFiringPosition = transform.position;
            NotifyNearbyBots();
        }
    }

    bool IsPlayerInVisionArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, smallRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Vector3 directionToPlayer = player.position - transform.position;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer <= visionAngle / 2f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void NotifyNearbyBots()
    {
        Collider[] nearbyBots = Physics.OverlapSphere(transform.position, largeRadius);
        foreach (var bot in nearbyBots)
        {
            if (bot.CompareTag("Enemy") && bot.gameObject != this.gameObject)
            {
                EnemyBotController botController = bot.GetComponent<EnemyBotController>();
                if (botController != null)
                {
                    botController.TriggerRunning(lastFiringPosition);
                }
            }
        }
    }

    void TriggerFiring()
    {
        animator.Play(firingState);
    }

    void TriggerIdle()
    {
        animator.Play(idleState);
    }

    public void TriggerRunning(Vector3 targetPosition)
    {
        animator.Play(runningState);
        StartCoroutine(MoveToPosition(targetPosition));
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float speed = 5f;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, largeRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward * smallRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward * smallRadius;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
