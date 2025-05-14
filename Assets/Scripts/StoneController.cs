using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    public float force = 10f;
    private Rigidbody2D rb;
    Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update(){
        if(FindClosestEnemy() != null){
            float distance = Vector2.Distance(gameObject.transform.position, FindClosestEnemy().transform.position);
            if(distance > 20)
                Destroy(gameObject);

        }
    }

    public void Fling(){
        coll.isTrigger = false;
        GameObject enem = FindClosestEnemy();
        Vector2 direction = (enem.transform.position - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){   
            Destroy(gameObject);
        }
    }
}
