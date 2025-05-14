using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public float force = 10f;
    private Rigidbody2D rb;
    Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        
        GameObject pl = FindPlayer();

        Vector3 direction2 = pl.transform.position - transform.position;
        float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        Vector2 direction = (pl.transform.position - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, FindPlayer().transform.position);
            if(distance > 20)
                Destroy(gameObject);
    }

    public GameObject FindPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
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
    void OnCollisionEnter2D(){
        Destroy(gameObject);
    }
}
