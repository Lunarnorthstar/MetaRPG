using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CharacterSwitcher : MonoBehaviour
{
    public PointAndClick character1;
    public PointAndClick character2;
    public PointAndClick character3;

    public GameObject enemy;

    [Space]
    public int characterSelectionIndex;

    public bool isInABattle;

    public BattleManager battleManager;

    [Space]
    public Image characterHeadshot;
    public Sprite character1Sprite;
    public Sprite character2Sprite;
    public Sprite character3Sprite;
    public Sprite character4Sprite;

    [Space]
    public CinemachineVirtualCamera virtualCam;

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
                swtichCharacter();
            }
        }
    }
    public void swtichCharacter(int whoTo)
    {
        characterSelectionIndex = whoTo;

        switch (whoTo)
        {

            case 1:
                if (isInABattle)
                {
                    characterHeadshot.sprite = character1Sprite;
                }

                if (character1 != null)
                {
                    character1.swtichCharacter(true);
                }

                if (character2 != null)
                {
                    character2.swtichCharacter(false);
                }
                if (character3 != null)
                {
                    character3.swtichCharacter(false);
                }

                break;

            case 2:
                if (isInABattle)
                {
                    characterHeadshot.sprite = character2Sprite;
                }
                if (character1 != null)
                {
                    character1.swtichCharacter(false);
                }

                if (character2 != null)
                {
                    character2.swtichCharacter(true);
                }
                if (character3 != null)
                {
                    character3.swtichCharacter(false);
                }

                break;

            case 3:
                if (isInABattle)
                {
                    characterHeadshot.sprite = character3Sprite;
                }
                if (character1 != null)
                {
                    character1.swtichCharacter(false);
                }

                if (character2 != null)
                {
                    character2.swtichCharacter(false);
                }
                if (character3 != null)
                {
                    character3.swtichCharacter(true);
                }

                break;

            case 4:
                characterHeadshot.sprite = character4Sprite; //no need to check if in battle bcs it neva goes to 4 otherwise.

                if (character1 != null)
                {
                    character1.swtichCharacter(false);
                }

                if (character2 != null)
                {
                    character2.swtichCharacter(false);
                }
                if (character3 != null)
                {
                    character3.swtichCharacter(false);
                }

                virtualCam.Follow = enemy.transform;

                break;
        }
    }

    public void swtichCharacter()
    {
        Debug.Log("ghetto switch");
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

