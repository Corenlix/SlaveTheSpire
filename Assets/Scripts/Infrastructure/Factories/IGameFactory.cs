using Card;
using Deck;
using Entities;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        DeckView SpawnDeck(Vector3 position);
        Card.CardHolder SpawnCard(DeckView deck, CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardMover SpawnCardMover(DeckView deck);
        UIContainer SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
    }
}