using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Card.TargetSelectors
{
    public abstract class CardTargetSelector : MonoBehaviour
    {
        public event Action<List<Entity>> Selected;
 
        public abstract void StartSelecting();

        public abstract void FinishSelecting();
        
        protected void SelectTargets(List<Entity> targets)
        {
            FinishSelecting();
            Selected?.Invoke(targets);
        }
    }
}