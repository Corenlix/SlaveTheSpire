using UnityEngine;
using Card;
using Deck;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        
        public DeckHolder SpawnDeck(Vector3 position)
        {
            var deck = _assetProvider.Instantiate<DeckHolder>(AssetPath.DeckPath, position);
            return deck;
        }

        public CardHolder SpawnCard(DeckHolder deck, CardId cardId)
        {
            CardHolder cardHolder = _assetProvider.Instantiate<CardHolder>(AssetPath.CardPath);
            CardStaticData cardStaticData = _staticDataService.ForCard(cardId);
            cardHolder.Init(cardStaticData, deck);
            return cardHolder;
        }

        public CardMover SpawnCardMover(DeckHolder deck)
        {
            CardMover cardMover = _assetProvider.Instantiate<CardMover>(AssetPath.CardMoverPath);
            cardMover.UseDeck(deck);
            return cardMover;
        }
    }
}