using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public HealthBar healthBar;
    public float distance;
    public float speed;
    public bool active = false;
    public float attackDistance = 2.5f;
    private Rigidbody2D rb;
    public int health = 10;
    public bool fireDamage = false;
    public bool isMoving = false;

    private Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health);
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Health
        healthBar.SetHealth(health);
        if(health <= 0)
            Destroy(gameObject);

        //Attack distance
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < attackDistance){
            active = true;
            Vector2 direction = player.transform.position - transform.position;
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
            isMoving = true;
        }
        else if(active == true && distance >= attackDistance){
            active = false;
            FreezePos();
        }
        else{
            isMoving = false;
        }
        
        //Animation
        animator.SetBool("isMoving", isMoving);
    }
    void FreezePos(){
        rb.velocity -= rb.velocity;
    }
    void OnCollisionEnter2D(Collision2D col){
        //if(col.gameObject.CompareTag("Player"))
            //rb.AddForce(-player.transform.position - transform.position * 50);
        if(col.gameObject.CompareTag("Stone")){
            health-=5;
            healthBar.SetHealth(health);
        }
        if(col.gameObject.CompareTag("Flame")){
            FireDamage();
        }
        if(col.gameObject.CompareTag("Quake")){
            health-=2;
        }
    }
    public void FireDamage(){
        StartCoroutine(WaitCoroutine());
        
    }
    public void LoseHealth(int dmg){
        health -= dmg;
    }
    IEnumerator WaitCoroutine()
    {
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;

        
    }

}
