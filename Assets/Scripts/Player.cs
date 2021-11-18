using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jojotracker;
    public float distancekeeper;
    public float xposition;
    public bool islanded;
    public Text hp;
    public Vector2 respawnpoint;
    public int health;
    public GameObject effect;
    private Rigidbody2D rb;
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 70;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1;

    public bool isDead = false;
    private Animator anim;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public bool cd;
    public float range;

    public float velocityspeed;
    public GameObject dust;
    public bool attacking;
    public float RealJumpVelocity;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        upSpeed();
        death();
        Vector2 pos = transform.position;
        Heal();


        //float groundDistance = Mathf.Abs(pos.y - groundHeight);
        playeranim();
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                
                rb.velocity = Vector2.up * RealJumpVelocity;
             
                isHoldingJump = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            if(cd == false)
            {
                Attack();
                StartCoroutine(attack());
            }
    
        }
        


        hp.text = health.ToString("0" + "/ 3 ");
    }

    public void Jump()
    {
        if (isGrounded)
        {
           


                rb.velocity = Vector2.up * RealJumpVelocity;

                isHoldingJump = true;
            
        }
    }

    public void AttackM()
    {
        if (cd == false)
        {
            Attack();
            StartCoroutine(attack());
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + range, transform.position.y));
    }


    public void Attack()
    {
        RaycastHit2D rightattack = Physics2D.Raycast(transform.position, Vector2.right, range, whatIsEnemy);
        anim.SetTrigger("Attack");
        if(rightattack.collider != null)
        {
            rightattack.collider.GetComponent<Obstacle>().getkilled();
            Destroy(rightattack.collider);

        }
    }
    public void Heal()
    {
        if(jojotracker >= 20)
        {
            if(health < 3)
            {
                health += 1;
                jojotracker = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isDead)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            return;
        }

        Vector2 pos = transform.position;

  
        if (pos.y < -20 || pos.x < 0)
        {
            health -= 1;
            transform.position = respawnpoint;
            
        }

    
      
        if (!isGrounded)
        {
            
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;




            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
           
           // pos.y += velocity.y * Time.fixedDeltaTime;
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            /*  float velocityRatio = velocity.x / maxXVelocity;
              acceleration = maxAcceleration * (0.7f - velocityRatio);
              maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

              velocity.x += acceleration * Time.fixedDeltaTime;
              if (velocity.x >= maxXVelocity)
              {
                  velocity.x = maxXVelocity;
              }


            */
            velocity.x = velocityspeed; 

            if(transform.position.x < xposition)
            {
                Debug.Log("Going right");
                transform.Translate(Vector2.right * Time.deltaTime);
            }

        }

     


    }

    public void upSpeed()
    {
        if(velocityspeed <= 50)
        {
            if (distance > distancekeeper)
            {
                velocityspeed += 4;
                distancekeeper += 1000;
            }
        }
     
    }

    public void playeranim (){
        if (!isGrounded)
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Down", false);
                islanded = false;
            }
            else if (rb.velocity.y < 0)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Down", true);
            }
        }
        else if(isGrounded){
            anim.SetBool("Jump", false);
            anim.SetBool("Down", false);

            if(islanded == false)
            {
                FindObjectOfType<AudioManager>().Play("Landing");
                Instantiate(dust, this.transform.position, Quaternion.identity);
            }
            islanded = true;
     
        }
     
        
    
    }


    void hitObstacle(Obstacle obstacle)
    {
      
        Destroy(obstacle.gameObject);
        velocity.x *= 0.7f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            if(attacking == false)
            {
                Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
                FindObjectOfType<camerashaker>().shake();
                Destroy(collision.gameObject);
                health -= 1;
              

            }
            else if( attacking == true)
            {
                distance += 100f;
                Destroy(collision.gameObject);
                Instantiate(effect, transform.position, Quaternion.identity);
            }

     

        }
        

        if(collision.gameObject.tag == "Death")
        {
            isDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            jojotracker += 1;
            distance += 100f;
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }
        if (collision.gameObject.tag == "Death")
        {
            health -= 1;
            transform.position = respawnpoint;  
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if(attacking == false)
            {
                velocity.x *= 0.5f;
             
                health -= 1;
                FindObjectOfType<camerashaker>().shake();
            }
    
        }
    }

    IEnumerator attack()
    {
        attacking = true;
        StartCoroutine(cooldown());
        yield return new WaitForSeconds(0.4f);
    
        attacking = false;
    }

    IEnumerator cooldown()
    {
        cd = true;
        yield return new WaitForSeconds(1f);
        cd = false;
    }

    public void death()
    {
        if(health <= 0)
        {
            isDead = true;
        }
    }

}
