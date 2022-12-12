using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator mAnimator;
    private void Start() {
        mAnimator = GetComponent<Animator>();
    }
    public void PlayerRunning()
    {
        
        if(!mAnimator.GetBool("isWalking"))
            mAnimator.SetBool("isWalking", true);
    }
    public void PlayerRunToIdle()
    {
        if(mAnimator.GetBool("isWalking"))
            mAnimator.SetBool("isWalking", false);
    }
    public void PlayerAttack()
    {
        mAnimator.SetTrigger("Attack");
    }
    public void PlayerCast()
    {
        mAnimator.SetTrigger("Spell");
    }
    public void PlayerDamage()
    {
        mAnimator.SetTrigger("GetHit");
    }
    public void PlayerDeath()
    {
        mAnimator.SetTrigger("Death");
    }
}
