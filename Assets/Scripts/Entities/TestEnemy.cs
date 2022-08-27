using Infrastructure.StaticData;
using UnityEngine;

namespace Entities
{
    public class TestEnemy : Enemy
    {
        private string _name;
        
        public override void Init(EnemyStaticData staticData)
        {
            var testEnemyStaticData = (TestEnemyStaticData) staticData;
            _name = testEnemyStaticData.Name;
        }

        public override void Step()
        {
            Debug.Log($"{_name}'s step is over");
        }
    }
}