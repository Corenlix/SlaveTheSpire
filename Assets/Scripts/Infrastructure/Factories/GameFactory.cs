using Card;
using Card.SelectStateMachine;
using Card.TargetSelectors;
using Entities;
using Entities.Buffs;
using Infrastructure.Assets;
using Infrastructure.StaticData;
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
        private readonly ICardActivator _cardActivator;
        private readonly IPlayerHolder _playerHolder;
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly LocationHolder _locationHolder;
        private readonly UIHolder _uiHolder;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticDataService,
            ICardTargetSelectorFactory cardTargetSelectorFactory, ICardActivator cardActivator, IPlayerHolder playerHolder,
            IEnemiesHolder enemiesHolder, FinderUnderCursor finderUnderCursor, LocationHolder locationHolder, UIHolder uiHolder)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _cardTargetSelectorFactory = cardTargetSelectorFactory;
            _cardActivator = cardActivator;
            _playerHolder = playerHolder;
            _enemiesHolder = enemiesHolder;
            _finderUnderCursor = finderUnderCursor;
            _locationHolder = locationHolder;
            _uiHolder = uiHolder;
        }
        
        public CardHolder SpawnCard(CardId cardId)
        {
            CardHolder cardHolder = _assetProvider.Instantiate<CardHolder>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            cardHolder.Init(cardStaticData, _cardActivator);
            _uiHolder.UI.PlayerDeck.AddCard(cardHolder);
            return cardHolder;
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
            ui.EnergyView.Init(_playerHolder.Player.Energy);
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
            player.Init(3, 50, "Player");
            _playerHolder.SetPlayer(player);
            return player;
        }

        public Location SpawnLocation()
        {
            var location = _assetProvider.Instantiate<Location>(AssetPath.LocationPath);
            _locationHolder.SetLocation(location);
            return location;
        }

        public BuffHolder SpawnBuffHolder(Buff buff, Transform parent)
        {
            var buffHolder = _assetProvider.Instantiate<BuffHolder>(AssetPath.BuffHolderPath);
            buffHolder.transform.SetParent(parent);
            buffHolder.transform.localScale = Vector3.one;
            buffHolder.Init(buff, _staticDataService.BuffIconData.IconFor(buff.GetBuffId()));
            return buffHolder;
        }

        public DamageEffect SpawnDamageEffect(int damage, Vector3 position)
        {
            var damageEffect = _assetProvider.Instantiate<DamageEffect>(AssetPath.DamageEffectPath, position);
            damageEffect.Init(damage);
            return damageEffect;
        }
    }
}