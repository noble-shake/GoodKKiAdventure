using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitAnimControl : MonoBehaviour
{
    Animator animCtr;

    // Start is called before the first frame update
    void Start()
    {
        animCtr = GetComponent<Animator>();
    }

    public void OnClicked()
    {
        animCtr.SetTrigger("ClickBounce");
    }
}
