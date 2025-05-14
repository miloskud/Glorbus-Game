using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballs : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.AddForce(transform.position * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
            transform.position += transform.up * Time.deltaTime * 4;
    }
    void OnCollisionEnter2D(){
        Destroy(gameObject);
    }
}
