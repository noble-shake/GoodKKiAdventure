using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionResetWarning : MonoBehaviour
{
    [SerializeField] Button btnCheck;
    [SerializeField] Button btnCancel;

    private void Start()
    {
        btnCancel.onClick.AddListener(OnCancel);
        btnCheck.onClick.AddListener(OnCheck);
        
    }

    public void OnCancel()
    { 
    
    }

    public void OnCheck()
    { 
            
    }
}

