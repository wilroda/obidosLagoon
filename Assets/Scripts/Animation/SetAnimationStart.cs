using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationStart : MonoBehaviour
{
    [SerializeField] 
    private string initialState;
    [SerializeField, Range(0.0f, 1.0f)] 
    private float animationPercentage = 1.0f;

    void Start()
    {
        Animator animator = GetComponent<Animator>();

        int initialStateId;
        if (initialState == "")
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            initialStateId = stateInfo.fullPathHash;
        }
        else
        {
            initialStateId = Animator.StringToHash(initialState);
        }
        

        animator.Play(initialStateId, 0, animationPercentage);
    }
}
