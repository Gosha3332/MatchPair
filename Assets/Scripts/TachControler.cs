using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TachControler : MonoBehaviour, IPointerClickHandler
{
    public bool isTach { get; set; }
    public void OnPointerClick(PointerEventData eventData)
    {
        isTach = true;
    }
}
