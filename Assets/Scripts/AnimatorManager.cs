using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator Anim { get; private set; }
    
    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        Anim.applyRootMotion = isInteracting;
        Anim.SetBool("IsInteracting", isInteracting);
        Anim.CrossFade(targetAnim, 0.2f);
    }
}
