using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject objToSpawn;

    public HealthBar healthBar;
    
    public float speed = .1f;
    public float distance;
    public bool active = false;
    public float attackDistance = 5f;
    public float moveDistance = 4f;
    private Rigidbody2D rb;
    public int health = 10;
    public bool fireDamage = false;
    public float time = 0f;

    public float attTime = 2f;

    public bool moving = false;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        
        if(distance > moveDistance && distance < 6){
            Vector2 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
            moving = true;
        }
        if(moving && distance < moveDistance){
            rb.velocity -= rb.velocity;
        }

        healthBar.SetHealth(health);
        if(health <= 0)
            Destroy(gameObject);

        //Attack distance
        if(distance < attackDistance){
            active = true;
        }
        else if(active == true && distance >= attackDistance){
            active = false;
        }
    }
    void FixedUpdate(){
        time+=Time.fixedDeltaTime;
        if(time >= attTime){
            attTime+=2;
            if(active){
                GameObject proj = Instantiate(objToSpawn, transform.position, transform.rotation);
            }
        }
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
