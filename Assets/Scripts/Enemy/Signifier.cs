using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signifier : MonoBehaviour
{
    public delegate void OnAnimationFinish();
    public event OnAnimationFinish finishAnim;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    void FinishAnim()
    {
        finishAnim.Invoke();
        anim.enabled = false;
    }

    public void PlayAnimation()
    {
        anim.enabled = true;
        anim.Play("signifier");
        
    }

}
