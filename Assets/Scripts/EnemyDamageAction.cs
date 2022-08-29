using System;
using Entities;
using Infrastructure;
using Infrastructure.Factories;
using UnityEngine;

public class EnemyDamageAction : IEnemyAction
{
    public event Action<IEnemyAction> ActionEnded;
    
    private readonly IPlayerHolder _playerHolder;
    private readonly IGameFactory _gameFactory;
    private readonly Enemy _enemy;
    private readonly int _damage;


    public EnemyDamageAction(IGameFactory gameFactory, IPlayerHolder playerHolder, Enemy enemy, int damage)
    {
        _gameFactory = gameFactory;
        _playerHolder = playerHolder;
        _enemy = enemy;
        _damage = damage;
    }

    public void Use()
    {
        _enemy.Animator.SetTrigger(AnimationNames.AttackTrigger);
        _enemy.StateExited += OnEnemyAnimationEnded;
    }

    private void OnEnemyAnimationEnded(AnimatorStateInfo state)
    {
        if (state.shortNameHash == AnimationNames.FirstPhaseAttack)
            OnAttack();
        else if (state.shortNameHash == AnimationNames.SecondPhaseAttack)
            OnEndAttack();
    }

    private void OnAttack()
    {
        _playerHolder.Health.Subtract(_damage);
        _gameFactory.SpawnDamageEffect(_damage, _playerHolder.Position);
    }

    private void OnEndAttack()
    {
        _enemy.StateExited -= OnEnemyAnimationEnded;
        ActionEnded?.Invoke(this);
    }
}