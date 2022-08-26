using System.Linq;
using UnityEngine.EventSystems;

namespace Card.Activators
{
    public class Attack : CardTargetSelector, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Use(FindObjectsOfType<Entity>().ToList());
        }
    }
}