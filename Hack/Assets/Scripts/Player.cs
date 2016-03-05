using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
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
    }

    void OnCollisionEnter2D(Collision2D a_Collision)
    {
        if(a_Collision.collider.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), a_Collision.collider);
        }
    }
	
	void FixedUpdate ()
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

        if (!isLocalPlayer)
        {
            return;
        }

        //als laatste
        velocity += new Vector3(HorizontalInput, 0, 0) * speed * Time.deltaTime;
    }
}
