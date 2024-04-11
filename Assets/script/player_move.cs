using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    Animator anim;   
    void Awake() 
    {
       rigid = GetComponent<Rigidbody2D>();   
       spriteRenderer = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }
    
   void Update()
   {
      

      if(Input.GetButtonDown("Jump") && !anim.GetBool("isJumping")) {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
       }

      if(Input.GetButtonUp("Horizontal")){
          rigid.velocity = new Vector2(rigid.velocity.normalized.x*0f, rigid.velocity.y);
      }

      //방향전환 
      if (Input.GetButton("Horizontal"))
      {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
      }

      if(rigid.velocity.normalized.x == 0)
         anim.SetBool("isRunning", false);
      else
         anim.SetBool("isRunning", true);
   }
    

    // Update is called once per frame
    void FixedUpdate()
    {   // move by  control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxSpeed) //right maxSpeed
           rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if(rigid.velocity.x < maxSpeed*(-1))//left maxSpeed
           rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

           //landing
           if(rigid.velocity.y < 0) {
           Debug.DrawRay(rigid.position, Vector2.down, new Color(0,1,0));

           RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2  , LayerMask.GetMask("platform"));

           if(rayHit.collider != null) {
              if(rayHit.distance < 0.5f)
                 anim.SetBool("isJumping", false);

      }        
      }
    }
  } 

