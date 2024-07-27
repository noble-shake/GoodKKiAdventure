using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragable : MonoBehaviour
{
    Image slotImage;
    string description;
    [SerializeField] Item Parent;

    private void Start()
    {
        slotImage = GetComponent<Image>();
    }


    public Sprite getImage()
    { 
        return slotImage.sprite;
    }

    public void Equip()
    { 
        Parent.OnEquipped();
    }

    public string getDescription()
    {
        return Parent.Descript;
    }
}
