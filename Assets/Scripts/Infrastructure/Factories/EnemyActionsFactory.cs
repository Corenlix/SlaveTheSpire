using System.Collections.Generic;
using Entities.Enemies;
using Infrastructure.StaticData.Enemies;
using Infrastructure.StaticData.Enemies.EnemiesActions;
using Zenject;

namespace Infrastructure.Factories
{
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
}