using UnityEngine;

public class TriggerOuterAnimation : MonoBehaviour
{
    public Animator outerAnimator; // D�� objenin Animator bile�eni

    public void PlayOuterAnimation()
    {
        if (outerAnimator != null)
        {
            outerAnimator.SetTrigger("Play"); // D�� animasyonu tetiklemek i�in bir trigger
        }
    }
}
