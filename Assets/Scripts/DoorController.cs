using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpening;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().keys > 0 && !isOpening){  
            isOpening = true; 
            other.gameObject.GetComponent<PlayerMovement>().keys--;
            animator.SetTrigger("Open");
            other.gameObject.GetComponent<HealthDisplay>().keys--;
            StartCoroutine(WaitCoroutine());
        }

    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        isOpening = false;
    }
}
