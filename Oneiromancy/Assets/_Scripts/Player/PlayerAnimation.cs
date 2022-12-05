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
        mAnimator.SetTrigger("Running");
    }
    public void PlayerIdle()
    {
        mAnimator.SetTrigger("Idle");
    }
    public void PlayerAttack()
    {
        mAnimator.SetTrigger("Attack");
    }
    public void PlayerCast()
    {
        mAnimator.SetTrigger("Spell");
    }
}
