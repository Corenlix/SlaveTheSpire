using Card;
using Card.TargetSelectors;
using Deck;
using Entities;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        CardHolder SpawnCard(DeckView deck, CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardMover SpawnCardMover(DeckView deck);
        UIContainer SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
    }
}