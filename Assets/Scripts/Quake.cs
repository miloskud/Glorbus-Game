using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake : MonoBehaviour
{
    public float timer = 0f;
    public float growTime = 6f;
    public float maxSize = 10;
    public float delayTime;

    public bool isMaxSize = false;

    void Start()
    {

        if(!isMaxSize)
            StartCoroutine(Grow());
        //private Vector3 initialScale;
    }

    private IEnumerator Grow(){
        
        Vector2 startSize = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize,maxSize);
        do{
            transform.localScale = Vector3.Lerp(startSize, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < growTime);
        
        isMaxSize = true;
        Destroy(gameObject);
    }

    void OnCollisionEnter2d(Collider other){
        
        if(other.gameObject.CompareTag("Player"))
            Debug.Log("AA");
            Destroy(gameObject);
    }
}
