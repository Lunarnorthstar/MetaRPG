using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public PointAndClick character1;
    public PointAndClick character2;
    public PointAndClick character3;

    public int characterSelectionIndex;

    public bool isInABattle;

    public BattleManager battleManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isInABattle)
            {
                swtichCharacter();
            }
            else
            {
                if (battleManager.isAlliedTurn)
                {
                    swtichCharacter();
                }
            }
        }
    }

    void swtichCharacter()
    {

        if (characterSelectionIndex == 3)
        {
            characterSelectionIndex = 1;
        }
        else
        {
            characterSelectionIndex++;
        }

        switch (characterSelectionIndex)
        {

            case 1:
                character1.swtichCharacter(true);
                character2.swtichCharacter(false);
                character3.swtichCharacter(false);

                break;

            case 2:
                character1.swtichCharacter(false);
                character2.swtichCharacter(true);
                character3.swtichCharacter(false);

                break;

            case 3:
                character1.swtichCharacter(false);
                character2.swtichCharacter(false);
                character3.swtichCharacter(true);

                break;
        }
    }
}

