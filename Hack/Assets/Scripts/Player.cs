using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class Player : NetworkBehaviour
{
    Stats Stats = null;
    public ResourceStat Health { get { return Stats.Health; } set { Stats.Health = value; } }

    Rigidbody2D RigidBody = null;
    // Velocity is nu bepaald door de rigidbody, deze vector3 past nu dat aan.
    public Vector3 velocity { get { return RigidBody.velocity; } set { RigidBody.velocity = value; } }

    public float speed = 10.0F;
    public float speedfalloff = 10.0F;
    public float MaxSpeed = 3.0F;
    public float JumpStrength = 5.0F;

    void Start ()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Stats = GetComponent<Stats>();
    }

    void OnCollisionEnter2D(Collision2D a_Collision)
    {
        // Units van hetzelfde type colliden niet.
        if(a_Collision.collider.tag == tag)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), a_Collision.collider);
        }
    }

    public void HasDied(object sender, EventArgs e)
    {
        if(isLocalPlayer)
            Debug.Log("We have died!");
    }

	
	void Update ()
    {
        // We can jump if our velocity is close to 0.
        bool CanJump = Mathf.Abs(velocity.y) <= 0.03f;
        
        bool InTheAir = !CanJump;

        var HorizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(HorizontalInput)<0.03f)
        {
            float t_NewXVelocity = velocity.x * Time.deltaTime * speedfalloff;
            velocity = new Vector3(t_NewXVelocity, velocity.y);
        }

        if (Mathf.Abs(velocity.x) >= MaxSpeed)
        {
            float t_NewXVelocity = (velocity.x / Mathf.Abs(velocity.x)) * MaxSpeed;
            velocity = new Vector3(t_NewXVelocity, velocity.y);
        }

        if (CanJump && Input.GetButton("Jump"))
        {
            velocity += Vector3.up * JumpStrength;
        }

        //als laatste
        if (isLocalPlayer)
        {
            velocity += new Vector3(HorizontalInput, 0, 0) * speed * Time.deltaTime;
        }
    }
}
