﻿using System.Collections.Generic;
using System.Linq;
using Cards.TargetSelectors;
using UnityEngine;

namespace Infrastructure.Factories
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