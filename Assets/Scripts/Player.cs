using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variaveis
    public float Speed;
    public float JumpForce;

    public bool isJump;
    public bool doubleJump;


    private Rigidbody2D rig;
    private Animator anim;
   

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //movimentacao
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f ,0f);
        transform.position += movement * Time.deltaTime * Speed;

        //Chamando animacao
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk",false);
        }
    }

    void Jump()
    //pulo e duplo pulo
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJump)
            {
                rig.AddForce(new Vector2(0f,JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump",true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f,JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }    
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == 8)
        {
            isJump = false;
            anim.SetBool("jump",false);
        }

        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    {
         if(collision.gameObject.layer == 8)
        {
            isJump = true;
        }
    }
}
