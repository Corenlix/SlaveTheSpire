using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData;

public interface IEnemyActionsFactory
{
    List<IEnemyAction> GetActions(EnemyStaticData staticData, Enemy enemy);
}