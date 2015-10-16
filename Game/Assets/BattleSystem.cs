using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour {

    public enum BattleStates
    {
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        LOSE,
        WIN
    }

    private BattleStates CurrentState;


	// Use this for initialization
	void Start ()
    {
        CurrentState = BattleStates.START;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(CurrentState);
	    switch (CurrentState) //kiest tussen de volgende case
        {
            case (BattleStates.START):
                //alles wat er bij START gebeurt
                break;

            case (BattleStates.PLAYERCHOICE):
                //alles wat er bij de keuzes van de speler gebeurt
                break;

            case (BattleStates.ENEMYCHOICE):
                //alles wat er bij de keuze van de "enemy" gebeurt
                break;

            case (BattleStates.LOSE):
                //alles wat er bij het verliezen van de battle gebeurt 
                //game over
                break;

            case (BattleStates.WIN):
                //alles wat er bij het winnen van de battle gebeurt
                break;
        }
	}

    void OnGUI() //werkt
    {
        if (GUILayout.Button("Next state"))
        {
            if (CurrentState == BattleStates.START)
            {
                CurrentState = BattleStates.PLAYERCHOICE;
            }
            else if (CurrentState == BattleStates.PLAYERCHOICE)
            {
                CurrentState = BattleStates.ENEMYCHOICE;
            }
            else if (CurrentState == BattleStates.ENEMYCHOICE)
            {
                CurrentState = BattleStates.LOSE;
            }
            else if (CurrentState == BattleStates.LOSE)
            {
                CurrentState = BattleStates.WIN;
            }
            else if (CurrentState == BattleStates.WIN)
            {
                CurrentState = BattleStates.START;
            }

        }
    }

}
