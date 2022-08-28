using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string CardsDataPath = "Static Data/Cards";
        private const string EnemiesDataPath = "Static Data/Enemies";
        private const string BuffIconDataPath = "Static Data";
        private Dictionary<CardId, CardStaticData> _cards;
        private Dictionary<EnemyId, EnemyStaticData> _enemies;
        private BuffIconStaticData _buffIcon;
        
        public StaticDataService()
        {
            Load();
        }

        private void Load()
        {
            _cards = Resources
                .LoadAll<CardStaticData>(CardsDataPath)
                .ToDictionary(x => x.Id, x => x);

            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesDataPath)
                .ToDictionary(x => x.Id, x => x);

            _buffIcon = Resources.LoadAll<BuffIconStaticData>(BuffIconDataPath).First();
        }

        public CardStaticData ForCard(CardId id) => _cards[id];
        public EnemyStaticData ForEnemy(EnemyId id) => _enemies[id];
        public BuffIconStaticData BuffIconData => _buffIcon;
    }
}