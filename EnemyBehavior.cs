using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private Animator anim;

    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    void Start()
    {
       myRigidbody = GetComponent<Rigidbody2D>();
       myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        } else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }

        /*
        anim.SetBool("Walk", isGrounded());
        anim.SetBool("attack", isGrounded());
        */
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }

    /*private bool isGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            return raycastHit.collider != null;
        } */
}
