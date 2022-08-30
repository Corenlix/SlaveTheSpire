using System.Collections.Generic;
using Entities.Enemies;
using Infrastructure.StaticData.Enemies.EnemiesActions;

namespace Infrastructure.Factories
{
    public interface IEnemyActionsFactory
    {
        List<IEnemyAction> GetActions(EnemyStaticData staticData, Enemy enemy);
    }
}