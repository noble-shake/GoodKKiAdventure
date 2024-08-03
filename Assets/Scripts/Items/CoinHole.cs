using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinHole : MonoBehaviour, IDropHandler
{
    [SerializeField] CoinGatcha coin;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CoinGatcha") && coin == null)
        {
            coin = eventData.pointerDrag.GetComponent<CoinGatcha>();
            Transform goTrs = coin.setMagnetic();
            // goTrs.SetParent(transform);
            // goTrs Trigger
            coin.OpenItem();
        }
    }
}
