using System.Collections.Generic;
using Card.TargetSelectors;
using UnityEngine;

namespace Infrastructure
{
    public class CardTargetSelectorsPool : MonoBehaviour
    {
        private ICardTargetSelectorFactory _targetSelectorFactory;
        private readonly Dictionary<CardTargetSelectorType, CardTargetSelector> _selectors = new ();
        
        public void Init(ICardTargetSelectorFactory targetSelectorFactory)
        {
            _targetSelectorFactory = targetSelectorFactory;
        }

        public CardTargetSelector Get(CardTargetSelectorType type)
        {
            if(_selectors.TryGetValue(type, out var selector))
                return selector;

            selector = _targetSelectorFactory.ForType(type);
            selector.transform.SetParent(transform);
            _selectors.Add(type, selector);
            return selector;
        }
    }
}