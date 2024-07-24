using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField] LogoEffect TitlePrefab;
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] Transform CanvasTrs;


    [Header("Room Anim")]

    bool DragOn;
    float dragStartPtX;
    float dragUpdatePtX;
    [SerializeField] float rotateSpeed = 50f;
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

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(TitlePrefab, GameObject.FindGameObjectWithTag("UI").transform);
        Instantiate(TitlePrefab, CanvasTrs);
        
    }

    public void OnMainMenu()
    {
        Instantiate(MainMenuPrefab, CanvasTrs);
        PlayerObject = Instantiate(PlayerObject);
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMoveEffect();
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

        float playerRotZ = PlayerObject.transform.rotation.eulerAngles.y;

        if (!DragOn)
        {
            if (playerRotZ != 0f)
            {
                if (playerRotZ < 0f)
                {
                    playerRotZ += Time.deltaTime * (rotateSpeed / 2);
                    if (playerRotZ > 0f)
                    { 
                        playerRotZ = 0f;
                    }
                }
                else
                {
                    playerRotZ -= Time.deltaTime * (rotateSpeed / 2);
                    if (playerRotZ < 0f)
                    {
                        playerRotZ = 0f;
                    }
                }

                PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotZ, 0f);
            }

            return;
        }

        Debug.Log(Input.mousePosition);
        dragUpdatePtX = Input.mousePosition.x;



        if (dragStartPtX < dragUpdatePtX)
        {
            playerRotZ -= Time.deltaTime * rotateSpeed;
            PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotZ, 0f);
        }
        else if (dragStartPtX > dragUpdatePtX)
        {
            playerRotZ += Time.deltaTime * rotateSpeed;
            PlayerObject.transform.rotation = Quaternion.Euler(0f, playerRotZ, 0f);
        }

    }
}
