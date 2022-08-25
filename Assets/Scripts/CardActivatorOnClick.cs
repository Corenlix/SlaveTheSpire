using System.Linq;
using UnityEngine.EventSystems;

public class CardActivatorOnClick : CardActivator, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Activate(FindObjectsOfType<Entity>().ToList());
    }
}