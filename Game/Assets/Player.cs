using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) //er gebeurt collision
    {
   
    }

    void OnTriggerEnter(Collider trigger)
    {
            
        Debug.Log("Collision with" + trigger.gameObject.name);
            

        if (trigger.gameObject.name == "NPC" && Input.GetButton("Jump"))
        {
            Debug.Log("Hello mister!");
        }
    }

    public float Movementspeed = 1; //de snelheid waarmee het object beweegt

    // Use this for initialization
    void Start()
    {
        
    }

    public float m_WalkDelay = 0.1f;
    private float m_WalkTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        m_WalkTimer -= Time.deltaTime; // tel af naar 0

        if (Input.GetKey("left") && m_WalkTimer <= 0.0f) //wat er moet gebeuren per toets wanneer de walktimer <=0 is
        {
            transform.Translate(Vector3.left * Movementspeed);
            m_WalkTimer = m_WalkDelay;
        }

        if (Input.GetKey("right") && m_WalkTimer <= 0.0f)
        {
            transform.Translate(Vector3.right * Movementspeed);
            m_WalkTimer = m_WalkDelay;
        }

        if (Input.GetKey("up") && m_WalkTimer <= 0.0f)
        {
            transform.Translate(Vector3.forward * Movementspeed);
            m_WalkTimer = m_WalkDelay;
        }

        if (Input.GetKey("down") && m_WalkTimer <= 0.0f)
        {
            transform.Translate(Vector3.back * Movementspeed);
            m_WalkTimer = m_WalkDelay;
        }

    }
        

}
