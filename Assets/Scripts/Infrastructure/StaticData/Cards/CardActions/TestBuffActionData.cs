using Card.Actions;
using Infrastructure.StaticData.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.CardActions
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