using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Attacker attacker = other.gameObject.GetComponent<Attacker>();

        if (attacker!=null)
        {
            animator.SetTrigger("underAttack trigger");
        }
    }
}
