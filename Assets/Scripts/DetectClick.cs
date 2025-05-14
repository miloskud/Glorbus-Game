using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    public GameObject select;
    public GameObject objToSpawn;
    private GameObject obj;
    Vector3 spawnOffset;
    public float xOffset;
    public float yOffset;
    private bool canClick;
    public GameObject player;
    private bool canRockSlam = true;
    Animator myAnimator;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnOffset = new Vector3(xOffset, yOffset, 0);
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
   {
        try{
       if(canClick && GameObject.FindGameObjectsWithTag("Enemy") != null && Vector2.Distance(transform.position, FindClosestEnemy().transform.position) < 10 && player.GetComponent<PlayerMovement>().mana > 0){
            player.GetComponent<PlayerMovement>().manaTimer = 0;
            if(gameObject.tag == "Stone"){
                player.GetComponent<PlayerMovement>().loseMana(1);
                gameObject.GetComponent<StoneController>().Fling();
                try{
                    Destroy(obj);
                }
                catch{}
            }
            else if(gameObject.tag == ("Rock")){
                if(canRockSlam == true){
                    player.GetComponent<PlayerMovement>().loseMana(1);
                    myAnimator.SetTrigger("StartSlam");
                    StartCoroutine(WaitCoroutine());
                    
                    
                    canRockSlam = false;
                    }
                
            }
            else if(gameObject.tag == "Fire"){
                GameObject proj = Instantiate(objToSpawn, transform.position + spawnOffset, transform.rotation);
                player.GetComponent<PlayerMovement>().loseMana(1);
                myAnimator.SetTrigger("Activate");

            }
            else{
                
            }
            
            
        }
        }
        catch{}
   }
   IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1);
        GameObject proj = Instantiate(objToSpawn, transform.position, transform.rotation);
        canRockSlam = true;

    }

    void OnMouseEnter()
    {
        canClick = true;
        obj = Instantiate(select, transform.position + spawnOffset, transform.rotation);
        
    }
    void OnMouseExit()
    {
        canClick = false;
        Destroy(obj);
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
