using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Eat")]
    public class EatCardStaticData : CardStaticData
    {
        [SerializeField] private int _instantHeal;
        [SerializeField] private int _buffSteps;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new EatCardAction(_instantHeal, _buffSteps);
        }
    }
}