using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
            return;

        GameObject dropped = eventData.pointerDrag;
        DragItem dragItem = dropped.GetComponent<DragItem>();
        dragItem.parentTransform = transform;
    }

}
