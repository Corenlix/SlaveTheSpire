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
        CardGameObject SpawnCard(CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardSelectStateMachine SpawnCardMover();
        UI SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
        BuffHolder SpawnBuffHolder(BuffId id, int steps, Transform parent);
        DamageEffect SpawnDamageEffect(int damage, Vector3 position);
    }
}