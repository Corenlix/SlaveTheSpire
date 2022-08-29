using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData;
using Zenject;

public class EnemyActionsFactory : IEnemyActionsFactory
{
    private readonly DiContainer _diContainer;

    public EnemyActionsFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public List<IEnemyAction> GetActions(EnemyStaticData staticData, Enemy enemy) =>
        staticData.GetEnemyActions(_diContainer, enemy);
}