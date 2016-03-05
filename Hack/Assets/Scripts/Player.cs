using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float speed = 10.0F;
    public float speedfalloff = 10.0F;
    public float MaxSpeed = 3.0F;

    void Start ()
    {
	

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), coll.collider);
        }
    }

    void Update ()
    {
        var HorizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(HorizontalInput)<0.03f)
        {
            velocity -= velocity * Time.deltaTime * speedfalloff;
        }

        if (Mathf.Abs(velocity.x) >= MaxSpeed)
        {
            velocity.x = (velocity.x / Mathf.Abs(velocity.x)) * MaxSpeed;
        }

        if (Input.GetButton("Jump"))
        {
            velocity += Vector3.up;
        }

        //als laatste
        velocity += new Vector3(HorizontalInput, 0, 0) * speed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }
}
