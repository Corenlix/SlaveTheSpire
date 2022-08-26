﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure
{
    public class StaticDataService : IStaticDataService
    {
        private const string CardsDataPath = "Static Data/Cards";
        private Dictionary<CardId, CardStaticData> _cards;
        
        public StaticDataService()
        {
            Load();
        }

        private void Load()
        {
            _cards = Resources
                .LoadAll<CardStaticData>(CardsDataPath)
                .ToDictionary(x => x.Id, x => x);
        }

        public CardStaticData ForCard(CardId id) => _cards[id];
    }
}