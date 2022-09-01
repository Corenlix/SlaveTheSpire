using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Salo")]
    public class SaloCardStaticData : CardStaticData
    {
        [SerializeField] private int _bonusShield;
        [SerializeField] private int _damageToOwner;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new SaloCardAction(_bonusShield, _damageToOwner);
        }
    }
}