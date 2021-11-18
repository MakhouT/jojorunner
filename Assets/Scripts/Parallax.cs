using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public string menubg;
    public float depth = 1;
    public float realVelocity;
    public float end;
    public float start;
    Player player;

    private void Awake()
    {
        if (menubg != "menubg")
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(menubg != "menubg")
        {
            if(menubg == "front")
            {
                 realVelocity = player.velocity.x;
            }
            else
            {
                 realVelocity = player.velocity.x / depth;
            }
           
          
            Vector2 pos = transform.position;

            pos.x -= realVelocity * Time.fixedDeltaTime;

            if (pos.x <= end)
                pos.x = start;

            transform.position = pos;
        }
        else
        {
            
            float realVelocity = 10 / depth;
            Vector2 pos = transform.position;

            pos.x -= realVelocity * Time.fixedDeltaTime;

            if (pos.x <= end)
                pos.x = start;

            transform.position = pos;
        }
      
    }
}
