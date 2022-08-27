using System.Collections.Generic;
using System.Linq;
using Card.TargetSelectors;
using UnityEngine;

namespace Infrastructure
{
    public class CardTargetSelectorFactory : ICardTargetSelectorFactory
    {
        private const string SelectorsPath = "CardTargetSelectors";
        private Dictionary<CardTargetSelectorType, CardTargetSelector> _cardTargetSelectorsPrefabs;

        public CardTargetSelectorFactory()
        {
            Load();
        }
        
        private void Load()
        {
            _cardTargetSelectorsPrefabs = Resources.LoadAll<CardTargetSelector>(SelectorsPath)
                .ToDictionary(x => x.SelectorType, x => x);
        }

        public CardTargetSelector ForType(CardTargetSelectorType type)
        {
            return Object.Instantiate(_cardTargetSelectorsPrefabs[type]);
        }
    }
}