using Infrastructure;
using Zenject;

public class EnemyDamageAction : IEnemyAction
{
    private readonly IPlayerHolder _playerHolder;
    private readonly int _damage;

    public EnemyDamageAction(IPlayerHolder playerHolder, int damage)
    {
        _playerHolder = playerHolder;
        _damage = damage;
    }
    
    public void Use()
    {
        _playerHolder.Health.Subtract(_damage);
    }
}