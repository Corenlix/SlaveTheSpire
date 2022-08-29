using Card;
using Card.SelectStateMachine;
using Card.TargetSelectors;
using Deck;
using Entities;
using Entities.Buffs;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        CardHolder SpawnCard(CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardSelectStateMachine SpawnCardMover();
        UIContainer SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
        BuffHolder SpawnBuffHolder(Buff buff, Transform parent);
        DamageEffect SpawnDamageEffect(int damage, Vector3 position);
    }
}