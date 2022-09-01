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
        private readonly ICardTargetSelectorFactory _cardTargetSelectorFactory;
        private readonly IPlayerHolder _playerHolder;
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly LocationHolder _locationHolder;
        private readonly UIHolder _uiHolder;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticDataService,
            ICardTargetSelectorFactory cardTargetSelectorFactory, IPlayerHolder playerHolder,
            IEnemiesHolder enemiesHolder, FinderUnderCursor finderUnderCursor, LocationHolder locationHolder, UIHolder uiHolder)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _cardTargetSelectorFactory = cardTargetSelectorFactory;
            _playerHolder = playerHolder;
            _enemiesHolder = enemiesHolder;
            _finderUnderCursor = finderUnderCursor;
            _locationHolder = locationHolder;
            _uiHolder = uiHolder;
        }
        
        public Card SpawnCard(CardId cardId, Player owner)
        {
            Card card = _assetProvider.Instantiate<Card>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            ICardActivator cardActivator = new CardActivator(_diContainer, owner, cardStaticData);
            card.Init(cardStaticData, cardActivator);
            _uiHolder.UI.PlayerDeck.AddCard(card);
            return card;
        }

        public CardTargetSelectorsPool SpawnCardTargetSelectorsPool()
        {
            var poolGameObject = new GameObject("CardTargetsSelectorsPool");
            poolGameObject.transform.SetParent(_uiHolder.UI.Canvas.transform);
            var pool = poolGameObject.AddComponent<CardTargetSelectorsPool>();
            pool.Init(_cardTargetSelectorFactory);
            return pool;
        }

        public CardSelectStateMachine SpawnCardMover()
        {
            CardSelectStateMachine cardSelectStateMachine = _assetProvider.Instantiate<CardSelectStateMachine>(AssetPath.CardMoverPath);
            cardSelectStateMachine.Init(SpawnCardTargetSelectorsPool(), _uiHolder.UI.PlayerDeck, _finderUnderCursor);
            return cardSelectStateMachine;
        }

        public UI SpawnUIContainer()
        {
            var ui = _assetProvider.Instantiate<UI>(AssetPath.UIContainerPath);
            _uiHolder.SetUI(ui);
            ui.PlayerUI.ObservePlayer(_playerHolder.Player);
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
        
        public Player SpawnPlayer()
        {
            var player = _assetProvider.Instantiate<Player>(AssetPath.PlayerPath);
            player.transform.SetParent(_locationHolder.Location.PlayerSpawnPoint);
            player.transform.position = _locationHolder.Location.PlayerSpawnPoint.position;
            player.Init(3, 3, 30, 30, "Player", 10);
            _playerHolder.SetPlayer(player);
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

        public DamageEffect SpawnDamageEffect(int damage, Vector3 position)
        {
            var damageEffect = _assetProvider.Instantiate<DamageEffect>(AssetPath.DamageEffectPath, position);
            damageEffect.Init(damage);
            return damageEffect;
        }
    }
}