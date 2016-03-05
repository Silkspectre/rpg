using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    Vector3 velocity = Vector3.zero;
    public float speed = 10.0F;
    public float speedfalloff = 10.0F;
    public float MaxSpeed = 3.0F;


    // Use this for initialization
    void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
        var HorizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(HorizontalInput)<0.03f)
        {
            velocity -= velocity * Time.deltaTime * speedfalloff;
        }

        if (velocity.magnitude >= MaxSpeed)
        {
            velocity = velocity.normalized * MaxSpeed;
        }

        if (Input.GetButton("Jump"))
        {
            velocity += Vector3.up;
        }

        if (!isLocalPlayer)
        {
            return;
        }

        //als laatste
        velocity += new Vector3(HorizontalInput, 0, 0) * speed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }
}
