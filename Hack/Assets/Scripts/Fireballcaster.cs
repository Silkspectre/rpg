using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fireballcaster : NetworkBehaviour {

    public float fireballspeed = 5.0F;
    public GameObject fireballobject;
    private float fireballtimer = 0.0F;
    public float fireballdelay = 0.5F;

    private bool Castingfireball;

    // Use this for initialization
    void Start () {
	
  
	}
	
	// Update is called once per frame
	void Update () {
    
        //laat de timer terugtellen naar 0
    fireballtimer -= Time.deltaTime;
        //je bent casting als de timer boven de 0 zit
    Castingfireball = fireballtimer >= 0.0F;

        if (isLocalPlayer && Input.GetButton("Fire1") && Castingfireball == false)
    {
            GameObject t_Fireball = Network.Instantiate(fireballobject, transform.position + Vector3.up * 0.3F, Quaternion.identity, 0) as GameObject;
            fireballtimer = fireballdelay;
    }


       
    //go to where the player is facing
    //collision with objects

	}
}
