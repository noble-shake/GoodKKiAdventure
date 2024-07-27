using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDeselectHandler
{
    bool Equipped;
    [SerializeField] Button btnEquip;
    [SerializeField] Transform ItemBackground;
    [SerializeField] GameObject EquippedImage;
    Sprite ItemImage;
    [SerializeField] TMP_Text EquipName;
    [SerializeField] string EquipDescript;
    [SerializeField] Image ItemAttach;
    [SerializeField] Image ItemDetach;
    ItemSO Data;

    statusContainer ItemStatus;

    public Sprite image { get { return ItemImage; } }

    public statusContainer stat { get { return ItemStatus; } }

    public string Name { get { return EquipName.text; } }
    public string Descript { get { return EquipDescript; } }

    public ItemSO data { get { return Data; } set { Data = value; dataDisassemble(); } }

    private void dataDisassemble()
    {
        EquipName.text = Data.Name;
        EquipDescript = Data.Descript;
        ItemAttach.sprite = Data.Attach;
        ItemDetach.sprite = Data.Attach;
        ItemImage = ItemAttach.sprite;
        ItemDetach.color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        ItemStatus = Data.Stat;
    }

    public void OnClicked()
    {
        // btnEquip.Select();

        customRoom crm = GetComponentInParent<customRoom>();
        crm.descript = Data.Descript;
    }

    public void OnUnClicked()
    {
        customRoom crm = GetComponentInParent<customRoom>();
        crm.setDefaultText();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemAttach.transform.SetParent(MainMenuManager.instance.getCanvasTrs());
        ItemAttach.transform.position = Input.mousePosition;
        ItemDetach.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ItemAttach.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemAttach.transform.SetParent(ItemBackground.transform);
        ItemAttach.transform.position = ItemDetach.transform.position;
        ItemDetach.gameObject.SetActive(false);
    }

    public void OnEquipped()
    {
        Equipped = true;
        EquippedImage.gameObject.SetActive(true);
    }

    public void OnUnEquipped()
    {
        Equipped = false;
        EquippedImage.gameObject.SetActive(false);
    }

    void Start()
    {
        Button SelectedButton = btnEquip.GetComponent<Button>();
        SelectedButton.onClick.AddListener(OnClicked);
        // EquippedImage.gameObject.SetActive(false);
        ItemDetach.gameObject.SetActive(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnUnClicked();
    }
}
