using Cards;
using Cards.CardActions.Warrior;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Defense")]
    public class DefenseCardStaticData : CardStaticData
    {
        [SerializeField] private int _armorPoint;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new DefenseAction(_armorPoint);
        }
    }
}