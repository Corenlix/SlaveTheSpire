using Card;
using Deck;
using UnityEngine;

namespace Infrastructure
{
    public interface IGameFactory
    {
        DeckView SpawnDeck(Vector3 position);
        CardHolder SpawnCard(DeckView deck, CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardMover SpawnCardMover(DeckView deck);
        UIContainer SpawnUIContainer();
    }
}