using UnityEngine;
using UnityEngine.EventSystems;

public class TachControler : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public bool isTach { get; set; }
    [field: SerializeField] public bool oneTach { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        isTach = true;
    }
}
