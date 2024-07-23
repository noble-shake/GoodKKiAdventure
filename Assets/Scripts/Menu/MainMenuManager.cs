using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    bool isTitleOn;
    [SerializeField] LogoEffect TitlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(TitlePrefab, GameObject.FindGameObjectWithTag("UI").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
