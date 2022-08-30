using Entities;
using Infrastructure;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "EnemyActionData/Damage")]
public class EnemyDamageActionStaticData : EnemyActionStaticData
{
    [SerializeField] private int _damage;
    public override IEnemyAction GetEnemyAction(DiContainer diContainer, Enemy actionOwner)
    {
        return new EnemyDamageAction(diContainer.Resolve<IGameFactory>(), diContainer.Resolve<IPlayerHolder>(), actionOwner, _damage);
    }
}