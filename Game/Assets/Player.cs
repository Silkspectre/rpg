using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Movementspeed = 1; //de snelheid waarmee het object beweegt
    public float m_WalkDelay = 0.1f;

    private float m_WalkTimer = 0.0f;
    private Vector3 StartingPosition;
    private Vector3 TargetPosition;

    private bool m_CollidingWithNPC = false;
    private GameObject m_NPC = null;

    void OnCollisionEnter(Collision collision) //er gebeurt collision
    {
   
    }

    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Triggered with" + trigger.gameObject.name);

        //dit moet er nog voor zorgen dat je niet precies op het moment dat er collision is space hoeft te klikken

        if (trigger.gameObject.name == "NPC")
        {
            Debug.Log("Hey, that NPC is colliding with us!");
            m_CollidingWithNPC = true;
            m_NPC = trigger.gameObject;
        }


       // {
            //Debug.Log("Hello mister!");
       // }
        
            

        
        //if (trigger.gameObject.name == "NPC" && Input.GetButton("Jump"))

       // {
            //Debug.Log("Hello mister!");
       // }
    }

    public GameObject SpeechBubble;

    // Use this for initialization
    void Start()
    {
        SpeechBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_WalkTimer -= Time.deltaTime; // tel af naar 0

        //player niet teleporteert van 1 plek naar de andere, maar dat de beweging van de player naar de plek ook wordt getoond

        // Maak een boolean die zegt of de speler aan het lopen is
        // Dat doe je door te comtroleren of walktimer boven 0 is

        bool PlayerWalking = m_WalkTimer >= 0;

        // Als we niet aan het lopen zijn en als we een toets indrukken
        // Zet de current position en de target position in CurrentPosition en TargetPosition

        if (Input.GetKey("left") && PlayerWalking == false) //wat er moet gebeuren per toets wanneer de walktimer <=0 is
        {
            // We moeten bewegen.
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + (Vector3.left * Movementspeed);// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("right") && m_WalkTimer <= 0.0f)
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + (Vector3.right * Movementspeed);// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("up") && m_WalkTimer <= 0.0f)
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + (Vector3.forward * Movementspeed);// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("down") && m_WalkTimer <= 0.0f)
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + (Vector3.back * Movementspeed);// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        // Als de speler beweegt dan lerp tussen start en end position
        if (PlayerWalking)
        {
            // begin = WalkingDelay = 0.1f == tijd op 0
            // end = 0 == tijd op 1
            
            float tijd = (m_WalkDelay - Mathf.Max(0.0f, m_WalkTimer)) * (1.0f/m_WalkDelay);
            transform.position = Vector3.Lerp(StartingPosition, TargetPosition, tijd);
        }

        if (m_CollidingWithNPC && Input.GetButtonUp("Jump"))
        {
            //m_CollidingWithNPC = false;
            Debug.Log("Dialog!");
            SpeechBubble.SetActive(true);
        }

        Debug.Log("Colliding: " + m_CollidingWithNPC);

        //Translate
    }
        

}
