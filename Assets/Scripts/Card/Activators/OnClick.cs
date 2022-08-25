using System.Linq;
using UnityEngine.EventSystems;

namespace Card.Activators
{
    public class OnClick : CardActivator, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Activate(FindObjectsOfType<Entity>().ToList());
        }
    }
}