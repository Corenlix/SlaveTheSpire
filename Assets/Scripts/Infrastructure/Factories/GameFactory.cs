using Cards;
using Cards.SelectStateMachine;
using Cards.TargetSelectors;
using Entities;
using Entities.Buffs;
using Entities.Enemies;
using Infrastructure.Assets;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Cards;
using Infrastructure.StaticData.Enemies;
using Map;
using UIElements;
using UnityEngine;
using Utilities;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPrefabFactory _prefabFactory;
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly LocationHolder _locationHolder;
        private readonly UIHolder _uiHolder;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticDataService,
                IPrefabFactory prefabFactory,
            IEnemiesHolder enemiesHolder, FinderUnderCursor finderUnderCursor, LocationHolder locationHolder, UIHolder uiHolder)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _prefabFactory = prefabFactory;
            _enemiesHolder = enemiesHolder;
            _finderUnderCursor = finderUnderCursor;
            _locationHolder = locationHolder;
            _uiHolder = uiHolder;
        }
        
        public Card SpawnCard(Player owner)
        {
            CardId cardId = owner.DeckHolder.GetCard();
            var card = _assetProvider.Instantiate<Card>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            ICardActivator cardActivator = new CardActivator(_diContainer, owner, cardStaticData);
            card.Destroyed += OnCardDestroyed;
            card.Init(cardStaticData, cardActivator);
            _uiHolder.UI.PlayerDeck.AddCard(card);
            return card;

            void OnCardDestroyed(Card destroyedCard)
            {
                card.Destroyed -= OnCardDestroyed;
                owner.DeckHolder.PushCard(destroyedCard.CardId);
            }
        }

        public CardTargetSelectorsPool SpawnCardTargetSelectorsPool()
        {
            var poolGameObject = new GameObject("CardTargetsSelectorsPool");
            poolGameObject.transform.SetParent(_uiHolder.UI.Canvas.transform);
            var pool = poolGameObject.AddComponent<CardTargetSelectorsPool>();
            pool.Init(_prefabFactory);
            return pool;
        }
        
        public CardSelectStateMachine SpawnCardMover()
        {
            var cardSelectStateMachine = _assetProvider.Instantiate<CardSelectStateMachine>(AssetPath.CardMoverPath);
            cardSelectStateMachine.Init(SpawnCardTargetSelectorsPool(), _uiHolder.UI.PlayerDeck, _finderUnderCursor);
            return cardSelectStateMachine;
        }

        public UI SpawnUIContainer()
        {
            var ui = _assetProvider.Instantiate<UI>(AssetPath.UIContainerPath);
            _uiHolder.SetUI(ui);
            return ui;
        }

        public Enemy SpawnEnemy(EnemyId id)
        {
            EnemyStaticData staticData = _staticDataService.ForEnemy(id);
            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(staticData.EnemyPrefab);
            enemy.Init(staticData);
            _enemiesHolder.Add(enemy);
            return enemy;
        }
        
        public Player SpawnPlayer(PlayerData playerData)
        {
            var player = _assetProvider.Instantiate<Player>(AssetPath.PlayerPath);
            player.Init(playerData);
            return player;
        }

        public Location SpawnLocation()
        {
            var location = _assetProvider.Instantiate<Location>(AssetPath.LocationPath);
            _locationHolder.SetLocation(location);
            return location;
        }

        public Buff SpawnBuff(BuffId id, int steps, Transform parent, Entity buffTarget)
        {
            BuffStaticData buffData = _staticDataService.ForBuff(id);
            IBuffAction buffAction = buffData.GetBuffAction(_diContainer, buffTarget);
            var buff = _assetProvider.Instantiate<Buff>(AssetPath.BuffHolderPath);
            buff.transform.SetParent(parent);
            buff.transform.localScale = Vector3.one;
            buff.Init(buffData, buffAction, steps);
            return buff;
        }

        public MapManager SpawnMap()
        {
            var map = _assetProvider.Instantiate<MapManager>(AssetPath.MapPath);
            return map;
        }
    }
}