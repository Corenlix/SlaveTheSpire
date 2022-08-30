using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyId _id;
        [SerializeField] private int _maxHealth;
        [SerializeField] private string _name;
        [SerializeField] private List<EnemyActionData> _enemyActionData;
        
        public List<IEnemyAction> GetEnemyActions(DiContainer diContainer, Enemy enemy) => _enemyActionData.Select(x=>x.GetEnemyAction(diContainer, enemy)).ToList();
        public EnemyId Id => _id;
        public abstract Enemy EnemyPrefab { get; }
        public int MaxHealth => _maxHealth;
        public string Name => _name;

    }
}