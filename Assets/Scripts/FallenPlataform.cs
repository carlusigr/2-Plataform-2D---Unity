using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenPlataform : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;


    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }
    //se bateu no personagem
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            //cair em segundos definido
            Invoke("Falling", fallingTime);
        }
    }

    //se cair no objeto que seja trigger que tenha a layer 9 ele sera destruido
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
