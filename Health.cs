using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

public class Health : MonoBehaviour
{
    public int health;
    public int MAX_HEALTH = 100;
    public int heal;
    private int MAX_HEAL = 3;
    public int Respawn;
    public HealthBar healthBar;
    public Potion potion;
    private Animator anim;

    void Start()
    {
        health = MAX_HEALTH;
        heal = MAX_HEAL;
        potion.SetMaxHeal(MAX_HEAL);
        healthBar.SetMaxHealth(MAX_HEALTH);
    }


    private void Awake()
    {    
        anim = GetComponent<Animator>();
    }

    
    void Update()// Update is called once per frame
    {
        if (Input.GetKeyDown(KeyCode.H)) // player heal
        {
            if (this.CompareTag("Player"))
            {
                if (MAX_HEAL >= 1)
                {
                    healing();// calling healing function
                    MAX_HEAL = MAX_HEAL - 1;
                    healthBar.SetHealth(health);
                    potion.SetMaxHeal(MAX_HEAL);
                    
                }
            }
        }
    }

//////////////////////////////////////////////////////////////

    
    private void healing()//Function for healing
    {
        Heal(20);// calling heal function
        anim.SetTrigger("heal");
    }

    
    public void SetHealth(int maxHealth, int health)//setting the max amount of health and health
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    
    private IEnumerator VisualIndicator(Color color)// Added for Visual Indicators
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    
    public void Damage(int amount)//code for damage
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        StartCoroutine(VisualIndicator(Color.red)); // Added for Visual Indicators

        if (health <= 0)
        {
            anim.SetTrigger("die");
            Die();//calling Die function
            GameplayController.instance.EnemyKilled();

            if (CompareTag("Player"))
            {
                //SceneManager.LoadScene(Respawn);
                PlayerDied();
            }
        }
        healthBar.SetHealth(health);
    }
    
    private void PlayerDied() //Used to load death screen
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    
    public void Heal(int amount)//function for healing the player, used by "healing" function
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
        //StartCoroutine(VisualIndicator(Color.green)); // Added for Visual Indicators

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
        //potion.SetHeal(heal);
    }

    
    private void Die()//the dying function
    {
        
        Debug.Log("I am Dead!");
        //Task.Delay(20000);
        Destroy(gameObject);

        if (this.CompareTag("Player"))
        {
            //Time.timeScale = 0;
            //OnPlayerDeath?.Invoke();
        }
        else
        {
            //OnEnemyDeath?.Invoke();
        }
    }
}