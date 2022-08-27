using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using Card;
using Card.Actions;
using Card.Actions.Data;
using Card.TargetSelectors;
using Deck;
using Entities;
using Infrastructure.Assets;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Mono.Collections.Generic;
using Zenject;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICardTargetSelectorFactory _cardTargetSelectorFactory;
        private CardActivator _cardActivator;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticDataService, ICardTargetSelectorFactory cardTargetSelectorFactory, CardActivator cardActivator)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _cardTargetSelectorFactory = cardTargetSelectorFactory;
            _cardActivator = cardActivator;
        }
        
        public CardHolder SpawnCard(DeckView deck, CardId cardId)
        {
            CardHolder cardHolder = _assetProvider.Instantiate<CardHolder>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            cardHolder.Init(cardStaticData, _cardActivator);
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

        public CardSelectStateMachine SpawnCardMover()
        {
            CardSelectStateMachine cardSelectStateMachine = _assetProvider.Instantiate<CardSelectStateMachine>(AssetPath.CardMoverPath);
            return cardSelectStateMachine;
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