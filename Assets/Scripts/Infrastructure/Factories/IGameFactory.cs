using Card;
using Card.SelectStateMachine;
using Card.TargetSelectors;
using Deck;
using Entities;
using Infrastructure.StaticData;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        CardHolder SpawnCard(DeckView deck, CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardSelectStateMachine SpawnCardMover();
        UIContainer SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
    }
}