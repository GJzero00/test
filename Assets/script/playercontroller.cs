using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int coin = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontal");
        
        //???Ⲿ??
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }

        //???????D
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }
    void SwitchAnim() 
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping")) 
        {
            if (rb.velocity.y < 0) 
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }   
            else if (coll.IsTouchingLayers(ground))
            {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection") 
        {
            Destroy(collision.gameObject);
            coin += 1;
        }
    }
}
