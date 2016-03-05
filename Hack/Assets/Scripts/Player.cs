using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    Vector3 velocity = Vector3.zero;
    public float speed = 10.0F;
    public float speedfalloff = 10.0F;
    public float MaxSpeed = 3.0F;

    void Start ()
    {
	

	}

	void Update ()
    {
        var HorizontalInput = Input.GetAxis("Horizontal");
        var VerticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(HorizontalInput)<0.03f)
        {
            velocity -= velocity * Time.deltaTime * speedfalloff;
        }

        if (velocity.magnitude >= MaxSpeed)
        {
            velocity = velocity.normalized * MaxSpeed;
        }

        velocity += new Vector3(HorizontalInput, 0, 0) * speed * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}
