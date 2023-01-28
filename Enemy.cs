using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int damage = 5;
    [SerializeField] private EnemyData data;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private Animator anim;

    private GameObject player;

    //private GameObject attackArea = default;
    //private bool attacking = false;
    //private float timeToAttack = 0.25f;
    //private float timer = 0f;
    public Health health;

    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-speed, 0f);
        }
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //anim.SetBool("attack",true);
            if (collider.GetComponent<Health>() != null)
            {
                //Task.Delay(200);

                collider.GetComponent<Health>().Damage(damage);
                //anim.SetBool("attack",false);
                //this.GetComponent<Health>().Damage(damage);
            }
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }
}