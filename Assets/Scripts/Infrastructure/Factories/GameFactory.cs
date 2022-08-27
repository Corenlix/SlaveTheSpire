using UnityEngine;
using Card;
using Deck;
using Entities;
using Infrastructure.Assets;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Zenject;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICardTargetSelectorFactory _cardTargetSelectorFactory;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticDataService, ICardTargetSelectorFactory cardTargetSelectorFactory)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _cardTargetSelectorFactory = cardTargetSelectorFactory;
        }
        
        public CardHolder SpawnCard(DeckView deck, CardId cardId)
        {
            CardHolder cardHolder = _assetProvider.Instantiate<CardHolder>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            cardHolder.Init(cardStaticData);
            deck.AddCard(cardHolder);
            return cardHolder;
        }

        public CardTargetSelectorsPool SpawnCardTargetSelectorsPool()
        {
            var poolGameObject = new GameObject("CardTargetsSelectorsPool");
            var pool = poolGameObject.AddComponent<CardTargetSelectorsPool>();
            pool.Init(_cardTargetSelectorFactory);
            return pool;
        }

        public CardMover SpawnCardMover(DeckView deck)
        {
            CardMover cardMover = _assetProvider.Instantiate<CardMover>(AssetPath.CardMoverPath);
            cardMover.UseDeck(deck);
            return cardMover;
        }

        public UIContainer SpawnUIContainer()
        {
            return _assetProvider.Instantiate<UIContainer>(AssetPath.UIContainerPath);
        }

        public Enemy SpawnEnemy(EnemyId id)
        {
            EnemyStaticData staticData = _staticDataService.ForEnemy(id);
            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(staticData.EnemyPrefab);
            enemy.Init(staticData);
            return enemy;
        }
        
        public Player SpawnPlayer()
        {
            return _assetProvider.Instantiate<Player>(AssetPath.PlayerPath);
        }

        public Location SpawnLocation()
        {
            return _assetProvider.Instantiate<Location>(AssetPath.LocationPath);
        }
    }
}