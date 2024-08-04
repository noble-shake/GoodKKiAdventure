using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinGatcha : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Transform CurrentTrs;
    Transform CanvasTr;
    [SerializeField] Image image;
    [SerializeField] Image Item;
    [SerializeField] bool isMagnetic;

    IEnumerator coinEffector;
    IEnumerator skipEffector;

    public Transform setMagnetic()
    { 
        isMagnetic = true;
        return transform;
    }

    public void OpenItem()
    {
        Item.sprite = DataManager.instance.TossItem();
        StartCoroutine(CoinEffect());
    }

    IEnumerator CoinEffect()
    {
        
        StartCoroutine(skipEffector);
        storeRoom.instance.EffectPlaying = true;
        isMagnetic = true;
        float t = 3.5f;
        float shakePower = 10f;
        Vector2 origin = transform.position;

        while (t > 0f)
        {
            t -= 0.05f;
            transform.position = origin + (Vector2)Random.insideUnitCircle * shakePower *  t;
            yield return null;
        }

        float scaleX = 1f;
        float colorValue = 255f;
        while (image.transform.localScale.x < 3f)
        {
            colorValue -= Time.deltaTime * 200f ;
            if (colorValue < 0f)
            {
                colorValue = 0f;
            }
            scaleX += Time.deltaTime * 1.5f;
            image.color = new Color(colorValue / 255f, colorValue / 255f, colorValue / 255f);
            
            Vector3 scaleScaler = new Vector3(scaleX, scaleX, 1f);
            image.transform.localScale = scaleScaler;
            yield return null;
        }

        StopCoroutine(skipEffector);
        Item.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        // storeRoom.instance.MoneyUpdate(10);
        yield return new WaitUntil(() => Input.anyKeyDown);
        Debug.Log("Open !!");
        storeRoom.instance.EffectPlaying = false;
        storeRoom.instance.UnLockCount();
        Destroy(gameObject);
    }

    IEnumerator SkipCoinEffect()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        StopCoroutine(coinEffector);
        image.color = Color.black;
        image.transform.localScale = new Vector3(3f, 3f, 1f);

        // Change Instant Change
        Item.gameObject.SetActive(true);


        yield return new WaitForSeconds(0.2f);
        // storeRoom.instance.MoneyUpdate(10);
        yield return new WaitUntil(() => Input.anyKeyDown);
        Debug.Log("Open !!");
        storeRoom.instance.EffectPlaying = false;
        storeRoom.instance.UnLockCount();
        Destroy(gameObject);
    }

    public void Start()
    {
        CurrentTrs = transform.parent.transform;
        CanvasTr = MainMenuManager.instance.getCanvasTrs();
        coinEffector = CoinEffect();
        skipEffector = SkipCoinEffect();
    }


    public void setCanvasTransform(Transform _trs)
    {
        CanvasTr = _trs;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMagnetic || storeRoom.instance.EffectPlaying) return;
        transform.SetParent(CanvasTr);
        transform.position = Input.mousePosition;
        image.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMagnetic || storeRoom.instance.EffectPlaying) return;
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("CoinSlot"))
        {
            transform.position = eventData.pointerEnter.transform.position;
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

            
    public void OnEndDrag(PointerEventData eventData)
    {
        image.GetComponent<Image>().raycastTarget = true;
        if (isMagnetic || storeRoom.instance.EffectPlaying) return;
        transform.SetParent(CurrentTrs);
    }
}
