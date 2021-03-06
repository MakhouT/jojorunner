using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestJump : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float groundHeight = 10;
    public bool isGrounded = false;
    public float jumpVelocity = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        transform.position = pos;
    }
}
