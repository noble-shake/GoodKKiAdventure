using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Net;

public class customRoomSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] customRoom parentObject; // STATUS Text Change
    [SerializeField] Image img;
    Item itemProxy;
    [SerializeField] Sprite DefaultSprite;
    bool isEquipped;

    public bool equipCheck { get { return isEquipped; } }

    public void OnUnEquip()
    {
        isEquipped = false;
        img.sprite = DefaultSprite;
        parentObject.changeOriginStat();
        if (itemProxy != null)
        {
            itemProxy.OnUnEquipped();
        }
        DataManager.instance.UnEquipped();

    }

    public void EquipRegistry(Item _obj)
    {
        isEquipped = true;
        if (itemProxy != null)
        {
            itemProxy.OnUnEquipped();
        }
        itemProxy = _obj;
        itemProxy.OnEquipped();
        img.sprite = itemProxy.image;
        parentObject.descript = itemProxy.Descript;
        statusContainer statIncrease = itemProxy.stat;
        parentObject.changeStat(statIncrease);

        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("item"))
        {
            Debug.Log("Dropped object was: " + eventData.pointerDrag);
            
            EquipRegistry(eventData.pointerDrag.GetComponent<Item>());

            DataManager.instance.EquipAdjust(eventData.pointerDrag.GetComponent<Item>());
        }
    }


}
