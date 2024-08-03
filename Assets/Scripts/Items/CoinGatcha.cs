using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinGatcha : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Transform CurrentTrs;
    Transform CanvasTr;
    [SerializeField] bool isMagnetic;

    public Transform setMagnetic()
    { 
        isMagnetic = true;
        return transform;
    }

    public void OpenItem()
    {
        StartCoroutine(CoinEffect());
    }

    IEnumerator CoinEffect()
    {
        float t = 2.5f;
        float shakePower = 3f;
        Vector2 origin = transform.position;

        while (t > 0f)
        {
            t -= 0.05f;
            transform.position = origin + (Vector2)Random.insideUnitCircle * shakePower *  t;
            yield return null;
        }

        float scaleX = 1f;
        while (transform.localScale.x < 5f)
        {
            scaleX += Time.deltaTime * 3;
            Vector3 scaleScaler = new Vector3(scaleX, scaleX, 1f);
            transform.localScale = scaleScaler;
            yield return null;
        }

        Debug.Log("Open !!");
        Destroy(gameObject);
    }


    public void Start()
    {
        CurrentTrs = transform.parent.transform;
        CanvasTr = MainMenuManager.instance.getCanvasTrs();
    }


    public void setCanvasTransform(Transform _trs)
    {
        CanvasTr = _trs;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(CanvasTr);
        transform.position = Input.mousePosition;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (eventData.pointerEnter.CompareTag("CoinSlot"))
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
        GetComponent<Image>().raycastTarget = true;
        // transform.SetParent(CurrentTrs);
    }
}
