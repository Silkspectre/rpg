using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RandomColour : NetworkBehaviour
{
    [SyncVar(hook = "SetColour")]
    Color m_Colour;

    void SetColour(Color a_Colour)
    {
        GetComponent<SpriteRenderer>().color = RandomPastel();
    }

    Color RandomPastel()
    {
        Color t_Colour = Color.black;

        int t_Colours = Random.Range(1, 3);
        for(int i = 0; i < t_Colours; i++)
        {
            int t_Position;

            do t_Position = Random.Range(0, 4); while (t_Colour[t_Position] != 0.0f);

            t_Colour[t_Position] = 0.66f;
        }

        return t_Colour;
    }


	void Start ()
    {
        if(isServer)
            m_Colour = RandomPastel();
	}

	void Update ()
    {
	
	}
}
