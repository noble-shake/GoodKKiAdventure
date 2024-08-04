using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionRoom : MonoBehaviour
{

    bool InGame;
    [SerializeField] GameObject DevModPanel;
    [SerializeField] Button btnBack;
    [SerializeField] Button btnMoneyAdd;
    [SerializeField] Button btnStageUnLock;
    [SerializeField] Button btnDataReset;
    // Warning DataReset;
    // Music Audio Mixer
    // Effect Audio Mixer

    private void Start()
    {
        
        btnBack.GetComponent<Button>();
        btnBack.onClick.AddListener(OnBack);

        btnMoneyAdd.GetComponent<Button>();
        btnMoneyAdd.onClick.AddListener(OnMoneyAdd);

        btnStageUnLock.GetComponent<Button>();
        btnStageUnLock.onClick.AddListener(OnStageUnLock);

        btnDataReset.GetComponent<Button>();
        btnDataReset.onClick.AddListener(OnDataReset);

        // Warning DataReset SetActive(false)
    }


    // Enum Mixer Type;
    private float setVolume()
    {
        return 0f;
    }


    public void SetIngameMode()
    {
        InGame = true;
        DevModPanel.SetActive(false);
        btnBack.onClick.RemoveListener(OnBack);
        btnBack.onClick.AddListener(OnBackInGame);
    }

    public void OnBack()
    { 
        
    }

    public void OnBackInGame()
    { 
        
    }

    public void OnMoneyAdd()
    {

    }

    public void OnStageUnLock()
    {

    }

    public void OnDataReset()
    { 
        
    }
}
