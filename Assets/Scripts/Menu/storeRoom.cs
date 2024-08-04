using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class storeRoom : MonoBehaviour
{
    int ButtonLock;

    public static storeRoom instance;
    Transform storeTrs;
    bool isEffectPlaying;

    [SerializeField] TMP_Text CurMoney;

    [SerializeField] CoinGatcha coinPrefab;
    [SerializeField] GameObject CreateSectionLeft;
    [SerializeField] GameObject CreateSectionRight;

    [SerializeField] Button btnBack;
    [SerializeField] Button btnSingleGatcha;
    [SerializeField] Button btnMultiGatcha;
    

    public bool EffectPlaying { get { return isEffectPlaying; } set { isEffectPlaying = value; } }

    public Transform StoreTrs { get { return storeTrs; } }
    public string StoreMoney { get { return CurMoney.text; } set { CurMoney.text = value; } }

    public void UnLockCount()
    {
        ButtonLock--;
    }

    public void MoneyUpdate(int _money = 0)
    {
        int currentMoney = DataManager.instance.money;
        if (_money == 0)
        {
            CurMoney.text = currentMoney.ToString();
        }
        else
        {
            currentMoney -= _money;
            DataManager.instance.money = currentMoney;
            CurMoney.text = currentMoney.ToString();
        }

        DataManager.instance.GameSave();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        storeTrs = transform;
        ButtonLock = 0;

        Button BtnBack = btnBack.GetComponent<Button>();
        BtnBack.onClick.AddListener(OnBack);

        Button BtnSingle = btnSingleGatcha.GetComponent<Button>();
        BtnSingle.onClick.AddListener(OnSingle);

        Button BtnMulti = btnMultiGatcha.GetComponent<Button>();
        BtnMulti.onClick.AddListener(OnMultiple);

        MoneyUpdate();
    }

    private void Update()
    {
        if (ButtonLock < 0)
        {
            ButtonLock = 0;
        }

        if (ButtonLock != 0)
        {
            btnBack.interactable = false;
            btnSingleGatcha.interactable = false;
            btnMultiGatcha.interactable = false;
        }
        else
        {
            btnBack.interactable = true;
            btnSingleGatcha.interactable = true;
            btnMultiGatcha.interactable = true;
        }

        // Money Check
        if (DataManager.instance.money < 10)
        {
            btnSingleGatcha.interactable = false;
            btnMultiGatcha.interactable = false;
        }
        else if (DataManager.instance.money < 100)
        {
            btnMultiGatcha.interactable = false;
        }
        else
        {
            btnSingleGatcha.interactable = true;
            btnMultiGatcha.interactable = true;
        }
    }

    public void OnBack()
    {
        if (ButtonLock != 0) return;
        
        MainMenuManager.instance.OpenUI(enumMenuPrefabs.MainMenu);
        Destroy(gameObject);
    }

    public void OnSingle()
    {
        if (ButtonLock != 0) return;
        ButtonLock++;
        StartCoroutine(getGatchaChance());
    }

    public void OnMultiple()
    {
        if (ButtonLock != 0) return;
        for (int i = 0; i < 10; i++)
        {
            ButtonLock++;
            StartCoroutine(getGatchaChance());
        }
    }

    IEnumerator getGatchaChance()
    {
        yield return new WaitForSeconds(0.3f);
        // random
        int toss = Random.Range(0, 2);
        GameObject CreateSection = (toss == 0) ? CreateSectionLeft : CreateSectionRight;
        RectTransform CreateRect = CreateSection.GetComponent<RectTransform>();
        float width = CreateRect.rect.width;
        float height = CreateRect.rect.height;


        int cX = (toss == 0) ? Random.Range(0, (int)width) : Random.Range(0, -(int)width);
        int halfY = (int)(height * 0.5f);
        int cY = Random.Range(-halfY, halfY);

        float MoveDistance = 1000f;
        Vector2 InstancePos = new Vector2(CreateRect.localPosition.x + cX, CreateRect.localPosition.y + cY + MoveDistance);

        CoinGatcha coin = Instantiate(coinPrefab, storeTrs);
        coin.GetComponent<RectTransform>().anchoredPosition = InstancePos;

        float flowTime = 0f;
        while (MoveDistance > 0f)
        {
            flowTime += Time.deltaTime;
            MoveDistance -= Mathf.Exp(flowTime) * 9.81f;
            if (MoveDistance < 0f)
            {
                MoveDistance = 0f;
            }

            coin.GetComponent<RectTransform>().anchoredPosition = new Vector3(CreateRect.localPosition.x + cX, CreateRect.localPosition.y + cY + MoveDistance, 0f);
            yield return null;
        }

        //coin.transform.position = InstancePos;
        // Money Change
    }
}
