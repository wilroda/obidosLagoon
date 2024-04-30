using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Play Animations")]
public class ActionPlayAnimations : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private bool                loop;
    [SerializeField]
    private Animator            animator;
    [SerializeField]
    private string[]            triggerName;

    private int animIndex = -1;

    private void Reset()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }


    protected override bool OnRun()
    {
        if (animator == null) return true;

        animIndex++;
        if (triggerName.Length <= animIndex)
        {
            if (loop) animIndex = 0;
            else
            {
                animIndex--;
                return true;
            }
        }

        animator.SetTrigger(triggerName[animIndex]);

        return true;
    }
}
