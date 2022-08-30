using System;
using Entities.Animations;
using Entities.Enemies;
using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
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
            _enemy.Animator.PlayPhaseAnimation(AnimationNames.AttackAnimation, OnAttack, OnEndAttack);
        }

        private void OnAttack()
        {
            _playerHolder.Player.TakeDamage(_damage);
            _gameFactory.SpawnDamageEffect(_damage, _playerHolder.Player.transform.position);
        }

        private void OnEndAttack()
        {
            ActionEnded?.Invoke(this);
        }
    }
}