using Cards;
using Cards.SelectStateMachine;
using Cards.TargetSelectors;
using Entities;
using Entities.Buffs;
using Entities.Enemies;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Cards;
using Infrastructure.StaticData.Enemies;
using UIElements;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        Card SpawnCard(CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardSelectStateMachine SpawnCardMover();
        UI SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
        Buff SpawnBuff(BuffId id, int steps, Transform parent);
        DamageEffect SpawnDamageEffect(int damage, Vector3 position);
    }
}