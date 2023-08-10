using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    //creating instances of objects(rigidbody,animator)
    private Rigidbody2D body;
    private Animator anim;
    private Boolean Grounded;
    private void Awake()
    {
        //grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(HorizontalInput*speed,0);

        //flips player when moving in left/right direction 
        if (HorizontalInput > 0.01f) 
        {
            transform.localScale = Vector3.one;
        }
        else if(HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetKey(KeyCode.Space) && Grounded) 
        {
            Jump();
        }

        //set animator parameters
        anim.SetBool("Run",HorizontalInput !=0);
        anim.SetTrigger("Jump");
        anim.SetBool("Grounded",Grounded);
    }
    private void Jump() 
    {
        body.velocity = new Vector2(0, speed);
        Grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Grounded") {
            Grounded = true;
        }
    }
}
