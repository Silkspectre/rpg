using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Statholder m_Stats;

    public float Movementspeed = 1; //de snelheid waarmee het object beweegt
    public float m_WalkDelay = 0.1f;

    private float m_WalkTimer = 0.0f;
    private Vector3 StartingPosition;
    private Vector3 TargetPosition;

    private bool m_CollidingWithNPC = false;
    private GameObject m_NPC = null;
    private Vector3 m_ContactNormal = Vector3.zero;

    void OnCollisionStay(Collision collision)
    {
        m_WalkTimer = -1.0f;
        
        if (collision.contacts.Length>0)
        {
            int t_Count = 0;
            foreach (ContactPoint contact in collision.contacts)
            {
                m_ContactNormal += contact.normal;
                t_Count++;
            }
            m_ContactNormal /= t_Count;

            //m_ContactNormal.Normalize();

            transform.position += m_ContactNormal*0.01f;
        }
    }

    void OnCollisionExit()
    {
    }

    bool CanWalkInDirection(Vector3 a_Direction)
    {
        // De contactnormal is een genormaliseerde vector die wijst naar het object vanuit de muur
        // als het verschil tussen de loopdirectie en de normal 2 is (squared), dan staan ze tegenover elkaar
        // dus loop je tegen een contactpunt, dus mag je niet lopen.

        // Mathf.Abs geeft een absolute waarde, alles onder nul wordt dezelfde waarde boven nul (-1 => 1, -333 => 333)
        // Ik doe dit omdat floats onprecies zijn, soms wordt 0.0f iets als 0.000001f.
        // 0.0f != 0.000001f, dus moet ik het absolute verschil hebben, waarbij ik controleer of het verschil minder dan 0.01f is.

        if (Mathf.Abs((m_ContactNormal - a_Direction.normalized).sqrMagnitude-2*2) < 0.01f)
            return false;

        m_ContactNormal = Vector3.zero;
        return true;
    }

    void OnTriggerEnter(Collider trigger)
    {
        //dit moet er nog voor zorgen dat je niet precies op het moment dat er collision is space hoeft te klikken

        if (trigger.gameObject.tag == "NPC")
        {
            m_CollidingWithNPC = true;
            m_NPC = trigger.gameObject;
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.tag == "NPC")
        {
            m_CollidingWithNPC = false;
            m_NPC.GetComponent<Dialog>().EndConversation();
            m_NPC = null;
        }
    }

    Speech Speech;

    // Use this for initialization
    void Start()
    {
        Speech = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Speech>();
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

        if (Input.GetKey("left") && PlayerWalking == false && CanWalkInDirection(Vector3.left)) //wat er moet gebeuren per toets wanneer de walktimer <=0 is
        {
            // We moeten bewegen.
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + Vector3.left;// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("right") && m_WalkTimer <= 0.0f && CanWalkInDirection(Vector3.right))
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + Vector3.right;// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("up") && m_WalkTimer <= 0.0f && CanWalkInDirection(Vector3.forward))
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + Vector3.forward;// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        if (Input.GetKey("down") && m_WalkTimer <= 0.0f && CanWalkInDirection(Vector3.back))
        {
            StartingPosition = transform.position;// De positie waar we nu staan (tijd 0%)
            TargetPosition = StartingPosition + Vector3.back;// De positie waar we over 0.1 sec staan (tijd 100%)
            m_WalkTimer = m_WalkDelay;
            m_CollidingWithNPC = false;
        }

        // Als de speler beweegt dan lerp tussen start en end position
        if (PlayerWalking)
        {
            float tijd = (m_WalkDelay - Mathf.Max(0.0f, m_WalkTimer)); // ipv 0.1 tot 0.0 (walkdelay = 0.1) wordt het 0.0 tot 0.1
            tijd = tijd * (1.0f/m_WalkDelay) * Movementspeed;          // dan 1/0.1 = 10 en dat doe ik keer de tijd (0.0 tot 1.0)

            transform.position = Vector3.Lerp(StartingPosition, TargetPosition, tijd);

            // Linear interpolation van 0.0 tot 1.0 (tijd)
            // Kies een punt tussen StartinPosition en TargetPosition, gebaseerd op 'tijd'.
        }

        if (m_CollidingWithNPC && Input.GetButtonUp("Jump"))
        {
            m_NPC.GetComponent<Dialog>().Speak();
        }

        //Translate
    }
        

}
