using Card;
using Deck;
using UnityEngine;

namespace Infrastructure
{
    public interface IGameFactory
    {
        DeckHolder SpawnDeck(Vector3 position);
        CardHolder SpawnCard(DeckHolder deck, CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardMover SpawnCardMover(DeckHolder deck);
        UIContainer SpawnUIContainer();
    }
}