using System.Collections.Generic;
using UnityEngine;

namespace Card.Activators
{
    public abstract class CardTargetSelector : MonoBehaviour
    {
        private CardHolder _cardHolder;

        public void Init(CardHolder cardHolder)
        {
            _cardHolder = cardHolder;
        }
    
        protected void Use(List<Entity> targets)
        {
            _cardHolder.Use(targets);
        }
    }
}