using Card;
using Card.SelectStateMachine;
using Card.TargetSelectors;
using Deck;
using Entities;
using Entities.Buffs;
using Entities.Enemies;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Cards;
using Infrastructure.StaticData.Enemies.EnemiesActions;
using UIElements;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        CardHolder SpawnCard(CardId cardStaticData);
        CardTargetSelectorsPool SpawnCardTargetSelectorsPool();
        CardSelectStateMachine SpawnCardMover();
        UI SpawnUIContainer();
        Enemy SpawnEnemy(EnemyId id);
        Location SpawnLocation();
        Player SpawnPlayer();
        BuffHolder SpawnBuffHolder(BuffId id, int steps, Transform parent);
        DamageEffect SpawnDamageEffect(int damage, Vector3 position);
        PopUp SpawnPopUp(PopUpType type, Vector3 position);
    }
}