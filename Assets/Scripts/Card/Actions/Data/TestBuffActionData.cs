using Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Test Buff")]
    public class TestBuffActionData : ActionData
    {
        [SerializeField] private BuffId _id;
        [SerializeField] private int _steps;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new TestBuffAction(_id, _steps);
        }
    }
}