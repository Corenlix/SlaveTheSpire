using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Test")]
    public class TestActionData : ActionData
    {
        [SerializeField] private int _testNumber;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new TestAction(_testNumber);
        }
    }
}