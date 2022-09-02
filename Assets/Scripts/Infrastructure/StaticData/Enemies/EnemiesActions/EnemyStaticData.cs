using System.Collections.Generic;
using System.Linq;
using Entities.Enemies;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyId _id;
        [SerializeField] private int _maxHealth;
        [SerializeField] private string _name;
        [SerializeField] private List<EnemyActionData> _enemyActionData;
        [SerializeField] private int _shield;
        [SerializeField] private int _initiative;
        [SerializeField] private int _attackPower;
        
        public List<IEnemyAction> GetEnemyActions(DiContainer diContainer, Enemy enemy) => _enemyActionData.Select(x=>x.GetEnemyAction(diContainer, enemy)).ToList();
        public EnemyId Id => _id;
        public abstract Enemy EnemyPrefab { get; }
        public int MaxHealth => _maxHealth;
        public string Name => _name;
        public int Shield => _shield;
        public int Initiative => _initiative;
        public int AttackPower => _attackPower;
    }
}