using Entities.Enemies;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
    [CreateAssetMenu(menuName = "EnemyActionData/Damage")]
    public class EnemyDamageActionData : EnemyActionData
    {
        [SerializeField] private int _damage;
        public override IEnemyAction GetEnemyAction(DiContainer diContainer, Enemy enemy)
        {
            return new EnemyDamageAction(diContainer.Resolve<IGameFactory>(), diContainer.Resolve<IPlayerHolder>(), enemy, _damage);
        }
    }
}