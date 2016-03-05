using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fireball : NetworkBehaviour {

    public float fireballspeed = 5.0F;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.right * fireballspeed * Time.deltaTime;
        Destroy(gameObject, 3);

        //beweging afhankelijk van positie van waaruit player kijkt
        //rotations

    }
}
