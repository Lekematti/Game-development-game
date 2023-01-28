using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private Animator anim;
    private Movement movement;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    float nextAttackTime = 0f;
    public float attackRate = 2f;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();

    }
    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        

        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }

        }
    }

    private void Attack()
    {
        attacking = true;
        anim.SetTrigger("L_Attack");
        attackArea.SetActive(attacking);
    }
}
