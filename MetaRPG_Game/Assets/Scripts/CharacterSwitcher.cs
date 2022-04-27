using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public PointAndClick character1;
    public PointAndClick character2;
    public PointAndClick character3;

    [Space]
    public int characterSelectionIndex;

    public bool isInABattle;

    public BattleManager battleManager;

    [Space]
    public Image characterHeadshot;
    public Sprite character1Sprite;
    public Sprite character2Sprite;
    public Sprite character3Sprite;

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

    public void swtichCharacter()
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
                if (isInABattle)
                {
                    characterHeadshot.sprite = character1Sprite;
                }
                character1.swtichCharacter(true);
                character2.swtichCharacter(false);
                character3.swtichCharacter(false);

                break;

            case 2:
                if (isInABattle)
                {
                    characterHeadshot.sprite = character2Sprite;
                }
                character1.swtichCharacter(false);
                character2.swtichCharacter(true);
                character3.swtichCharacter(false);

                break;

            case 3:
                if (isInABattle)
                {
                    characterHeadshot.sprite = character3Sprite;
                }
                character1.swtichCharacter(false);
                character2.swtichCharacter(false);
                character3.swtichCharacter(true);

                break;
        }
    }
}

