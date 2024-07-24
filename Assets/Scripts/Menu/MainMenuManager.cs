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
    [SerializeField] Transform RoomObjectTrs;

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

    bool DragOn;
    float dragStartPtX;
    float dragUpdatePtX;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject[] Playables;

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
    }

    void Update()
    {
        CharacterMoveEffect();
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
        PlayerObject = Instantiate(PlayerObject, RoomObjectTrs);
    }

    private void OnCustom()
    {
        CurrentUI = Instantiate(CustomPrefab, CanvasTrs);
        Destroy(PlayerObject);
        PlayerObject = null;
    }

    private void OnOption()
    {
        CurrentUI = Instantiate(OptionPrefab, CanvasTrs);
        Destroy(PlayerObject);
        PlayerObject = null;
    }

    private void OnStore()
    {
        CurrentUI = Instantiate(StorePrefab, CanvasTrs);
        Destroy(PlayerObject);
        PlayerObject = null;
    }

    private void OnGatcha()
    {
        CurrentUI = Instantiate(GatchaPrefab, CanvasTrs);
        Destroy(PlayerObject);
        PlayerObject = null;
    }

    private void OnStage()
    {
        CurrentUI = Instantiate(StagePrefab, CanvasTrs);
        Destroy(PlayerObject);
        PlayerObject = null;
    }

    private void CharacterMoveEffect()
    {
        if (PlayerObject == null) return;

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
