using System.Collections.Generic;
using UnityEngine;

namespace Card.Activators
{
    public abstract class CardActivator : MonoBehaviour
    {
        private CardHolder _cardHolder;

        public void Init(CardHolder cardHolder)
        {
            _cardHolder = cardHolder;
        }
    
        protected void Activate(List<Entity> targets)
        {
            _cardHolder.Use(targets);
        }
    }
}