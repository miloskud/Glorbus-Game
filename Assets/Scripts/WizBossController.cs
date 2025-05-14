using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizBossController : MonoBehaviour
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

    public GameObject fireObject;
    public GameObject quakeSpawn;
    

    public float attTimer = 0;

    public float timer;

    private Animator animator;

    float currentDashTime;

    

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
        Vector2 direction;
        //Health
        healthBar.SetHealth(health);
        if(health <= 0){
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
        //Attack distance
        distance = Vector2.Distance(transform.position, player.transform.position);
        float xMovement;
        float yMovement;
        if(distance < attackDistance){
            active = true;
            direction = player.transform.position - transform.position;
            xMovement = direction.x;
            yMovement = direction.y;
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
            isMoving = true;

            animator.SetBool("Moving", isMoving);
            animator.SetFloat("xMovement", xMovement);
            animator.SetFloat("YMovement", direction.y);

            
            
        }
        else if(active == true && distance >= attackDistance){
            active = false;
            FreezePos();
        }
        else{
            isMoving = false;
        }
    }

            //animator.set
        //Animation
        //.SetBool("isMoving", isMoving);
    void FixedUpdate(){
        attTimer += Time.deltaTime;
        if(attTimer >= 4){
            randomSelection();
            attTimer = 0;
        }

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
            StartCoroutine(FireCoroutine());
        }
        if(col.gameObject.CompareTag("Quake")){
            health-=2;
        }
    }

    public void LoseHealth(int dmg){
        health -= dmg;
    }

    void randomSelection(){
        //int[] operators = { 1, 2, 3, 4 };
        int randomVal = Random.Range(1, 4);
        //FireAttack();
        //QuakeAttack();
        //DashAttack();
        
        switch(randomVal)
        {  
            case 1 : 
            FireAttack();
            break;

            case 2 : 
            QuakeAttack();
            break;

            case 3 :
            case 4 :
            DashAttack();
            break;
        }
    }

    void FireAttack(){
        

        int steps = 9;

        float radius = .5f;
        float angleStep = 40f;

        for (int i = 0; i < steps; i++)
        {
            float a = angleStep * i;
            float x = Mathf.Cos(Mathf.Deg2Rad * a) * radius;

            float y = Mathf.Sin(Mathf.Deg2Rad * a) * radius;
            Vector2 spawnPosition = new Vector2(x + transform.position.x, y + transform.position.y);

            //Vector2 Point_1 = new Vector2([pos_x],[pos_y]);
            //Vector2 Point_2 = new Vector2([pos_x],[pos_y]);
            float angle = Mathf.Atan2(spawnPosition.y - transform.position.y , spawnPosition.x-transform.position.x) * 180 / Mathf.PI - 90;

            Instantiate(fireObject, spawnPosition, Quaternion.AngleAxis(angle, Vector3.forward));
        }
    }

    void QuakeAttack(){

        GameObject proj = Instantiate(quakeSpawn, transform.position, transform.rotation);
    }

    void DashAttack(){
        Vector2 direction = (player.transform.position - transform.position).normalized;
        StartCoroutine(Dash(direction));
    }

    IEnumerator FireCoroutine()
    {
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;
        yield return new WaitForSeconds(1);
        health-=1;   
    }
    IEnumerator Dash(Vector2 direction)
    {   


        currentDashTime = .7f; 

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            rb.velocity = direction * 8; 

            yield return null; 
        }
        rb.velocity = new Vector2(0f, 0f); 

    }
}
