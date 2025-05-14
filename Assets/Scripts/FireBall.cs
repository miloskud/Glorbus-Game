using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject target;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        if(FindClosestEnemy() != null)
            target = FindClosestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            if(FindClosestEnemy() == null)
                Destroy(this.gameObject);
            else
                target = FindClosestEnemy();
        }
        else{
            //Movement
            Vector2 direction = target.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        
            //Rotation
            Vector3 direction2 = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){   
            Destroy(gameObject);
        }
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
}
