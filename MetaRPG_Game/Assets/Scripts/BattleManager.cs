using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

    public List<CharacterTurnManager> characters = new List<CharacterTurnManager>();
    int currentCharacter;

    bool hasMoved;
    bool hasAttacked;

    void Start()
    {
        currentCharacter = 0;

        characters[currentCharacter].switchTurn();
    }

    public void checkInfo(bool moved, bool attacked)//this is called whenever a character either moves or attacks to verify what they just did.
    {
        if (moved)
        {
            hasMoved = true;
        }

        if (attacked)
        {
            hasAttacked = false;
        }
    }

    public void AdvanceTurn()//when the turn is advanced, it should do the following
    {
        if (currentCharacter == characters.Count - 1)//if we are ob the last character in the scene, then it will go back to 0
        {
            currentCharacter = 0;
        }
        else//otherwise iterate by one
        {
            currentCharacter++;
        }

        //then activate the character switcher
        characters[currentCharacter].switchTurn();

    }

}
