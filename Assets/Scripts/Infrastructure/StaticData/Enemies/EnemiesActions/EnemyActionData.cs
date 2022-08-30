using Entities.Enemies;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
    public abstract class EnemyActionData : ScriptableObject
    {
        public abstract IEnemyAction GetEnemyAction(DiContainer diContainer, Enemy enemy);
    }
}