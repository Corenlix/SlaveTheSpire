using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    [CreateAssetMenu(menuName = "Card/Card Actions/Test Buff")]
    public class TestBuffActionData : ActionData
    {
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new TestBuffAction(diContainer);
        }
    }
}