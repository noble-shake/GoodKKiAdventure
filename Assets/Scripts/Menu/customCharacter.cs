using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterChoice
{ 
    LEFT,
    RIGHT,
}

public class customCharacter : MonoBehaviour
{
    int currentTarget;
    int currentMain;

    [SerializeField] Button BtnMain;
    [SerializeField] Button BtnSub;
    [SerializeField] Button BtnLeft;
    [SerializeField] Button BtnRight;
    [SerializeField] Button BtnBack;

    [SerializeField] TMP_Text CharName;
    [SerializeField] TMP_Text CharMain;
    [SerializeField] TMP_Text CharSub;
    [SerializeField] TMP_Text CharDescription;
    [SerializeField] TMP_Text HP;
    [SerializeField] TMP_Text ATK;
    [SerializeField] TMP_Text SPD;
    [SerializeField] TMP_Text DEF;
    [SerializeField] TMP_Text CRT;

    [SerializeField] GameObject PlayablesDisplay;
    bool[] LockCheck;

    public string Descript { get { return CharDescription.text; } set { CharDescription.text = value; } }


    private void Start()
    {
        currentMain = DataManager.instance.curCharacter;
        currentTarget = currentMain;
        LockCheck = DataManager.instance.PlayableLockCheck();
        for (int idx = 0; idx < LockCheck.Length; idx++)
        {
            if (LockCheck[idx] == false)
            {
                DataManager.instance.getSpecificRoomPlayerObject(idx).SkinChange(PlayerWaitScript.enumSkin.LOCK);
            }
            else
            {
                DataManager.instance.getSpecificRoomPlayerObject(idx).SkinChange(PlayerWaitScript.enumSkin.ORIGIN);
            }
        }

    }

    public void OnClickRotate(CharacterChoice _ch)
    {
        BtnLeft.interactable = false;
        BtnRight.interactable = false;

        switch (_ch)
        {
            case CharacterChoice.LEFT:
                currentTarget--;
                break;
            case CharacterChoice.RIGHT:
                currentTarget++;
                break;
        }

        if (currentTarget < 0)
        {
            currentTarget = LockCheck.Length - 1;
        }
        else if(currentTarget > LockCheck.Length - 1)
        {
            currentTarget = 0;
        }

        if (LockCheck[currentTarget] == true)
        {
            if (currentTarget != currentMain)
            {
                BtnMain.gameObject.SetActive(true);
                BtnSub.gameObject.SetActive(true);
                BtnMain.interactable = true;
                BtnSub.interactable = true;
            }
            else if (currentTarget == currentMain)
            {
                BtnSub.gameObject.SetActive(true);
                BtnSub.interactable = true;
            }


            
        }
        else
        {
            BtnMain.gameObject.SetActive(false);
            BtnSub.gameObject.SetActive(false);
            BtnMain.interactable = false;
            BtnSub.interactable = false;
        }

        // MoveTo(ori, des)
    }
}
