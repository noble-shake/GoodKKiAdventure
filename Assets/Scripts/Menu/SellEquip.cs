using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellEquip : MonoBehaviour
{
    [SerializeField] TMP_Text PrevMoneyText;
    [SerializeField] TMP_Text AftMoneyText;
    [SerializeField] Button btnYes;
    [SerializeField] Button btnNo;
    int targetEquip;


    private void Start()
    {

        Button BtnYes = btnYes.GetComponent<Button>();
        BtnYes.onClick.AddListener(OnYes);

        Button BtnNo = btnNo.GetComponent<Button>();
        BtnNo.onClick.AddListener(OnNo);

        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        PrevMoneyText.text = DataManager.instance.money.ToString();
    }

    public void setAfterMoney(int _cost, int _target)
    {
        targetEquip = _target;
        AftMoneyText.text = (DataManager.instance.money + _cost).ToString();
    }

    public void OnYes()
    {
        DataManager.instance.ItemSell(targetEquip);
        gameObject.SetActive(false);
    }

    public void OnNo()
    {
        GetComponentInParent<customRoom>().sellCheck = false;
        GetComponentInParent<customRoom>().setDefaultText();
        gameObject.SetActive(false);
    }
}
