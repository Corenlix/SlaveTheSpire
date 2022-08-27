using UnityEngine;
using Card;
using Deck;
using Zenject;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICardTargetSelectorFactory _cardTargetSelectorFactory;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, ICardTargetSelectorFactory cardTargetSelectorFactory)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _cardTargetSelectorFactory = cardTargetSelectorFactory;
        }
        
        public DeckView SpawnDeck(Vector3 position)
        {
            var deck = _assetProvider.Instantiate<DeckView>(AssetPath.DeckPath, position);
            return deck;
        }

        public CardHolder SpawnCard(DeckView deck, CardId cardId)
        {
            CardHolder cardHolder = _assetProvider.Instantiate<CardHolder>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            cardHolder.Init(cardStaticData, deck);
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
    }
}