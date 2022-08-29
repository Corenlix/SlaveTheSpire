using Infrastructure;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "EnemyActionData/Damage")]
public class EnemyDamageActionData : EnemyActionData
{
    [SerializeField] private int _damage;
    public override IEnemyAction GetEnemyAction(DiContainer diContainer)
    {
        return new EnemyDamageAction(diContainer.Resolve<IPlayerHolder>(), _damage);
    }
}