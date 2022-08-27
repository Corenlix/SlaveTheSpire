using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public class FinderUnderCursor
    {
        private EventSystem _eventSystem;
        
        public T FindObjectUnderCursor<T>() where T : MonoBehaviour
        {
            if(!_eventSystem)
                _eventSystem = EventSystem.current;
            
            var eventData = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };
            var raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, raycastResults);
            foreach (var raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.TryGetComponent(out T result))
                    return result;
            }
        
            return null;
        }
    }
}