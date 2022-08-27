using Infrastructure.StaticData;
using UnityEngine;

namespace Entities
{
    public class TestEnemy : Enemy
    {
        private string _name;
        
        protected override void OnInit(EnemyStaticData staticData)
        {
            var testEnemyStaticData = (TestEnemyStaticData) staticData;
            _name = testEnemyStaticData.Name;
        }

        protected override void OnStep()
        {
            Debug.Log($"{_name}'s step is over");
        }
    }
}