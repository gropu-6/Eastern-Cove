using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnimator : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        if(GetComponent<SpriteRenderer>().sprite == null)
            animator.SetBool("glow", false);
        else
            animator.SetBool("glow", true);
    }
}
