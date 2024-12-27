using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class onTargetReached : MonoBehaviour
{
    public float treshold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;
    private bool wasReached=false;

    private void FixedUpdate()
    {
        float distence =Vector3.Distance(transform.position, target.position);
        if (distence < treshold && !wasReached)
        {
            //reached the target
            OnReached.Invoke();
            wasReached = true;   
        }
        else if (distence >=treshold)
        {
            wasReached = false;
        }
    }
}
