using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum enumMenuPrefabs
{ 
    MainMenu,
    Store,
    Custom,
    Gatcha,
    Option,
    StageSelection,
}

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    GameObject CurrentUI;

    [SerializeField] GameObject RoomObject;
    Transform RoomObjectTrs;

    [SerializeField] LogoEffect TitlePrefab;    
    [SerializeField] Transform CanvasTrs;

    [Header("Menu Prefabs")]
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] GameObject StorePrefab;
    [SerializeField] GameObject CustomPrefab;
    [SerializeField] GameObject GatchaPrefab;
    [SerializeField] GameObject OptionPrefab;
    [SerializeField] GameObject StagePrefab;

    [Header("Room Anim")]
    bool isCustom;
    bool DragOn;
    float dragStartPtX;
    float dragUpdatePtX;
    [SerializeField] float rotateSpeed;
    [SerializeField] PlayerWaitScript PlayerObject;
    [SerializeField] GameObject CustomCam;

    public PlayerWaitScript currentPlayable { get { return PlayerObject; } }

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
        //Instantiate(TitlePrefab, GameObject.FindGameObjectWithTag("UI").transform);
        Instantiate(TitlePrefab, CanvasTrs);
        RoomObject = Instantiate(RoomObject);
        RoomObjectTrs = RoomObject.transform;
    }

    void Update()
    {
        CharacterMoveEffect();
    }

    public Transform getCanvasTrs()
    {
        return CanvasTrs;
    }

    public void OpenUI(enumMenuPrefabs _enum)
    {
        switch (_enum)
        {
            case enumMenuPrefabs.MainMenu:
                OnMainMenu();
                break;
            case enumMenuPrefabs.Custom:
                OnCustom();
                break;
            case enumMenuPrefabs.Store:
                OnStore();
                break;
            case enumMenuPrefabs.Option:
                OnOption();
                break;
            case enumMenuPrefabs.Gatcha:
                OnGatcha();
                break;
            case enumMenuPrefabs.StageSelection:
                OnStage();
                break;
        }
    }

    private void OnMainMenu()
    {
        CurrentUI = Instantiate(MainMenuPrefab, CanvasTrs);
        if (PlayerObject != null)
        { 
            PlayerObject.gameObject.SetActive(false);
        }

        PlayerObject = DataManager.instance.getRoomPlayerObject();
        PlayerObject.LevelCheck();
        PlayerObject.transform.SetParent(RoomObjectTrs);
        PlayerObject.gameObject.SetActive(true);
        // PlayerObject = Instantiate(PlayerObject, RoomObjectTrs);
        CustomCam.SetActive(false);
        isCustom = false;
    }

    private void OnCustom()
    {
        CurrentUI = Instantiate(CustomPrefab, CanvasTrs);
        CustomCam.SetActive(true);
        isCustom = true;
    }

    private void OnOption()
    {
        CurrentUI = Instantiate(OptionPrefab, CanvasTrs);
    }

    private void OnStore()
    {
        CurrentUI = Instantiate(StorePrefab, CanvasTrs);
    }

    private void OnGatcha()
    {
        CurrentUI = Instantiate(GatchaPrefab, CanvasTrs);
    }

    private void OnStage()
    {
        CurrentUI = Instantiate(StagePrefab, CanvasTrs);
    }

    private void CharacterMoveEffect()
    {
        if (PlayerObject == null || isCustom) return;

        if (Input.GetMouseButtonDown(0))
        {
            DragOn = true;
            dragStartPtX = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
        { 
            DragOn = false;
        }

        float playerRotY = PlayerObject.transform.rotation.eulerAngles.y; // 0 ~ 360

        if (!DragOn)
        {
            if (playerRotY != 0f)
            {
                if (playerRotY > 0f && playerRotY < 180f)
                {
                    playerRotY -= Time.deltaTime * rotateSpeed * 2;
                    if (playerRotY < 0f)
                    {
                        playerRotY = 0f;
                    }
                }
                else if(playerRotY < 360f && playerRotY > 180f)
                {
                    playerRotY += Time.deltaTime * rotateSpeed * 2;
                    if (playerRotY < 0f)
                    {
                        playerRotY = 0f;
                    }
                }

                PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotY, 0f);
            }

            return;
        }
        dragUpdatePtX = Input.mousePosition.x;



        if (dragStartPtX < dragUpdatePtX)
        {
            playerRotY -= Time.deltaTime * rotateSpeed;
            PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotY, 0f);
        }
        else if (dragStartPtX > dragUpdatePtX)
        {
            playerRotY += Time.deltaTime * rotateSpeed;
            PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotY, 0f);
        }

    }
}
