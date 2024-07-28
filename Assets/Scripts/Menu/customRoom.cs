using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class customRoom : MonoBehaviour
{
    [SerializeField] Button BtnCharacterChange;
    [SerializeField] Button BtnUnarmed;
    [SerializeField] Button BtnBack;
    [SerializeField] Button BtnSell;
    [SerializeField] TMP_Text EquipDescription;
    string defaultText = "\n 장비를 드래그  해서 아래 슬롯에 놓아 착용하세요.";
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text atkText;
    [SerializeField] TMP_Text spdText;
    [SerializeField] TMP_Text defText;
    [SerializeField] TMP_Text crtText;

    Item SelectedItem;
    bool isSell;

    customRoomSlot slot;
    [SerializeField] SellEquip sellDisplay;
    [SerializeField] Transform ItemGridTrs;


    public bool sellCheck { get { return isSell; } set { isSell = value; } }

    public string descript { get { return EquipDescription.text; } set { EquipDescription.text = value; } }

    public Item ItemSelect { get { return SelectedItem; } set { SelectedItem = value; } }


    private void Update()
    {
        if (SelectedItem != null)
        {
            if (DataManager.instance.getCurrentEquip() != DataManager.instance.getSelectedEquipID(SelectedItem))
            {
                BtnSell.interactable = true;
            }
            else
            {
                BtnSell.interactable = false;
            }
        }
        else
        {
            BtnSell.interactable = false;
        }

        if (GetComponentInChildren<customRoomSlot>().equipCheck)
        {
            BtnUnarmed.interactable = true;
        }
        else
        {
            BtnUnarmed.interactable = false;
        }

    }



    private void OnEnable()
    {
        setDefaultText();
        int idx = DataManager.instance.getCurrentEquip();
        slot = GetComponentInChildren<customRoomSlot>();

        foreach (Item item in DataManager.instance.ItemList)
        {
            item.gameObject.SetActive(true);
            item.transform.SetParent(ItemGridTrs);
        }

        if (idx != -1)
        { 
            slot.EquipRegistry(DataManager.instance.ItemList[idx]);
            DataManager.instance.ItemList[idx].OnEquipped();
        }


        // DataManager.instance
    }

    private void Start()
    {
        Button btnBack = BtnBack.GetComponent<Button>();
        btnBack.onClick.AddListener(OnBack);

        Button btnSell = BtnSell.GetComponent<Button>();
        btnSell.onClick.AddListener(OnSell);

        Button btnUnarmed = BtnUnarmed.GetComponent<Button>();
        btnUnarmed.onClick.AddListener(OnBtnUnarmed);

        Button btnCharacterChange = BtnCharacterChange.GetComponent<Button>();
        btnCharacterChange.onClick.AddListener(OnCharacterChange);

        BtnSell.interactable = false;
    }

    private void OnDisable()
    {
        setDefaultText();
        if (DataManager.instance == null) return;

        foreach (Item item in DataManager.instance.ItemList)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(DataManager.instance.transform);
        }
    }
    public void OnBack()
    {
        if (isSell) return;

        setDefaultText();
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.MainMenu);
        Destroy(gameObject);
    }


    public void setDefaultText()
    {
        EquipDescription.text = defaultText;
    }

    public void OnBtnUnarmed()
    {
        if (isSell) return;

        GetComponentInChildren<customRoomSlot>().OnUnEquip();
        setDefaultText();
        BtnUnarmed.interactable = false;
    }

    public void OnSell()
    {
        if (isSell) return;

        if (SelectedItem == null) return;

        int idx = DataManager.instance.getSelectedEquipID(SelectedItem);
        int itemValue = DataManager.instance.ItemList[idx].data.Cost;

        sellDisplay.gameObject.SetActive(true);
        sellDisplay.setAfterMoney(itemValue, idx);
        sellCheck = true;

    }

    public void OnCharacterChange()
    {
        if (isSell) return;

    }

    public void changeStat(statusContainer _stat)
    {
        MainMenuManager.instance.currentPlayable.LevelCheck();
        setHP(_stat.hp);
        setATK(_stat.atk);
        setSPD(_stat.spd);
        setDEF(_stat.def);
        setCRT(_stat.crt);
    }

    public void changeOriginStat()
    {
        MainMenuManager.instance.currentPlayable.LevelCheck();
        setHP();
        setATK();
        setSPD();
        setDEF();
        setCRT();
    }

    public void setHP(int _add=0)
    {
        int curhp = MainMenuManager.instance.currentPlayable.data.Stat.hp;
        // get from dataManager;
        if (_add == 0f)
        {
            hpText.text = curhp.ToString();
        }
        else
        {
            hpText.text = $"{curhp + _add} (+{_add})";
        }
    }
    public void setATK(int _add = 0)
    {
        int curAtk = MainMenuManager.instance.currentPlayable.data.Stat.atk;

        // get from dataManager;
        if (_add == 0f)
        {
            atkText.text = curAtk.ToString();
        }
        else
        {
            atkText.text = $"{curAtk + _add} (+{_add})";
        }
    }

    public void setSPD(int _add = 0)
    {
        int curSpd= MainMenuManager.instance.currentPlayable.data.Stat.spd;

        // get from dataManager;
        if (_add == 0f)
        {
            spdText.text = $"+{curSpd}%";
        }
        else
        {
            spdText.text = $"{curSpd + _add}% (+{_add}%))";
        }
    }

    public void setDEF(int _add = 0)
    {
        int curDef = MainMenuManager.instance.currentPlayable.data.Stat.def;

        if (_add == 0f)
        {
            defText.text = $"+{curDef}%";
        }
        else
        {
            defText.text = $"{curDef + _add}% (+{_add}%))";
        }
    }

    public void setCRT(int _add = 0)
    {
        int curCrt = MainMenuManager.instance.currentPlayable.data.Stat.crt;

        if (_add == 0f)
        {
            crtText.text = $"+{curCrt}%";
        }
        else
        {
            crtText.text = $"{curCrt + _add}% (+{_add}%))";
        }
    }
}
