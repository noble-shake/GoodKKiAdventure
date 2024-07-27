using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainRoomScript : MonoBehaviour
{
    [Header("UI Setup")]
    [SerializeField] Button btnStart;
    [SerializeField] Button btnStatus;
    [SerializeField] Button btnStore;
    [SerializeField] Button btnGatcha;
    [SerializeField] Button btnOption;
    [SerializeField] TMP_Text CurrentLevel;
    [SerializeField] TMP_Text CurrentMoney;
    [SerializeField] Slider CurrentExp;

    // Slider : Set Max Value and Current Value using Data Container
    // Money : Set Current Value using Data Container
    // UserLevel : Set Current Value using Data Container

    private void Start()
    {
        Button startButton = btnStart.GetComponent<Button>();
        startButton.onClick.AddListener(StartEvent);

        Button statusButton = btnStatus.GetComponent<Button>();
        statusButton.onClick.AddListener(StatusEvent);

        Button storeButton = btnStore.GetComponent<Button>();
        storeButton.onClick.AddListener(StoreEvent);

        Button gatchaButton = btnGatcha.GetComponent<Button>();
        gatchaButton.onClick.AddListener(GatchaEvent);

        Button optionButton = btnOption.GetComponent<Button>();
        optionButton.onClick.AddListener(OptionEvent);

        // data collection.
    }

    private void OnEnable()
    {
        setDisplayInfo();
    }

    private void setDisplayInfo()
    {
        float reqExp = 100f;
        for (int i = 1; i < DataManager.instance.level; i++)
        {
            reqExp += reqExp * 0.2f;
        }

        CurrentExp.maxValue = reqExp;
        CurrentExp.value = DataManager.instance.exp;
        CurrentLevel.text = DataManager.instance.level.ToString();
        CurrentMoney.text = DataManager.instance.money.ToString();

    }

    public void StartEvent()
    {
        // Instantiate Stage Selection
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.StageSelection);
        Destroy(gameObject);
    }

    public void StatusEvent()
    {
        // Instantiate Custom
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.Custom);
        Destroy(gameObject);
    }

    public void StoreEvent()
    {
        // Instantiate Store;
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.Store);
        Destroy(gameObject);
    }

    public void OptionEvent()
    {
        // Instantiate Store;
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.Option);
        Destroy(gameObject);
    }

    public void GatchaEvent()
    {
        // Instantiate Gatcha
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.Gatcha);
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }

}
