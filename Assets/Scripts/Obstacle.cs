using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject enemybullet;
    public float range;
    public LayerMask playermask;
    Player player;
    public GameObject effect;
    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        timeBtwSpawn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        targetplayer();
    }


    private void FixedUpdate()
    {
        /*
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
        */
    }
    public void getkilled()
    {
        Destroy(this.gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
    }
    void attack()
    {
       


            if (timeBtwSpawn <= 0)
            {

                Instantiate(enemybullet, transform.position, Quaternion.identity);
                timeBtwSpawn = startTimeBtwSpawn;
            }

            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        
    }
    public void targetplayer()
    {
        RaycastHit2D rightattack = Physics2D.Raycast(transform.position, Vector2.left, range, playermask);
     
        if(rightattack.collider != null)
        {
            attack();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - range, transform.position.y));
    }
}
