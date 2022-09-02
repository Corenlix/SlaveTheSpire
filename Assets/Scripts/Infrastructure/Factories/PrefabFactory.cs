using System.Collections.Generic;
using System.Linq;
using Cards.TargetSelectors;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class PrefabFactory : IPrefabFactory
    {
        private const string SelectorsPath = "CardTargetSelectors";
        private Dictionary<CardTargetSelectorType, CardTargetSelector> _cardTargetSelectorsPrefabs;
        
        private const string PopUpPrefabs = "PopUpPrefabs";
        private Dictionary<PopUpType, PopUp> _popUpPrefabs;

        public PrefabFactory()
        {
            Load();
        }
        
        private void Load()
        {
            _cardTargetSelectorsPrefabs = Resources.LoadAll<CardTargetSelector>(SelectorsPath)
                .ToDictionary(x => x.SelectorType, x => x);
            
            _popUpPrefabs = Resources.LoadAll<PopUp>(PopUpPrefabs)
                .ToDictionary(x => x.PopUpType, x => x);
        }

        public CardTargetSelector ForType(CardTargetSelectorType type)
        {
            return Object.Instantiate(_cardTargetSelectorsPrefabs[type]);
        }
        
        public PopUp ForType(PopUpType popUpType)
        {
            return Object.Instantiate(_popUpPrefabs[popUpType]);
        }
    }
}