using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    private Animator animator;
    public int maxHealth = 5;
    public int health;
    public float hitTimer = 0f;
    public float timeBetweenHits = 2f;
    public HealthDisplay hd;
    public int maxMana = 5;
    public int mana;
    public float manaTimer = 0f;

    private int manaPots = 0;
    private int healthPots = 0;
    public int keys = 0;
    public GameObject dodgeIgnore;


    float currentDashTime;

    bool canDash = true;
    bool dodge = false;
   
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        mana = maxMana;
        hd.maxHealth = health;
        hd.maxMana = this.maxMana;
        hd.healthPots = this.healthPots;
        hd.manaPots = this.manaPots;

    }
    void Update()
    {
        if(health <= 0){
            SceneManager.LoadScene("MainMenu");
        }
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        animator.SetFloat("XInput", moveHorizontal);
        animator.SetFloat("YInput", moveVertical);
        if(moveHorizontal == 0 && moveVertical == 0)
            animator.SetBool("IsWalking", false);
        else
        {
             animator.SetBool("IsWalking", true);
        }
        rb2d.velocity = new Vector2 (moveHorizontal*speed, moveVertical*speed);
        hd.health = this.health;
        hd.mana = this.mana;



        if (Input.GetKeyDown(KeyCode.H) && healthPots > 0)
        {
            health = maxHealth;
            healthPots--;
            hd.healthPots = this.healthPots;
        }
        if (Input.GetKeyDown(KeyCode.M) && manaPots > 0)
        {
            mana = maxMana;
            manaPots--;
            hd.manaPots = this.manaPots;
        }

        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(new Vector2(.75f, .75f)));
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(new Vector2(-.75f, .75f)));
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(new Vector2(.75f, -.75f)));
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(new Vector2(-.75f, -.75f)));
            }
            else if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(Vector2.up));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
            }

            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(Vector2.down));
            }

            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
            }

            

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if((other.gameObject.CompareTag("Enemy") && hitTimer > timeBetweenHits) || other.gameObject.CompareTag("Dart")){   
            health--;
            hitTimer = 0f;
        }

        if(other.gameObject.CompareTag("HealthPot")){
            healthPots++;
            Destroy(other.gameObject);
            hd.healthPots = this.healthPots;
        }
        else if(other.gameObject.CompareTag("ManaPot")){
            manaPots++;
            Destroy(other.gameObject);
            hd.manaPots = this.manaPots;
        }
        else if(other.gameObject.CompareTag("Key")){
            keys++;
            Destroy(other.gameObject);
            hd.keys = this.keys;
        }
        else if(other.gameObject.CompareTag("Flame")){
            health--;
        }

        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(!dodge){
            health--;
        }
    }

    

    IEnumerator Dash(Vector2 direction)
    {   
        
        
        
        dodge = true;
        canDash = false;

        currentDashTime = .3f; 

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            rb2d.velocity = direction * (speed*2); 

            yield return null; 
        }
        rb2d.velocity = new Vector2(0f, 0f); 

        canDash = true;
        dodge = false;
    }




    public void FixedUpdate(){
        manaTimer++;
        if(manaTimer > 250 && mana < 5){
            mana++;
            manaTimer = 0;
        }
        hitTimer += 1f;
    }
    public void loseMana(int amt){
        mana--;
    }


}
