using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Drink beer")]
    public class DrinkBeerCardStaticData : CardStaticData
    {
        [SerializeField] private int _damageToOwner;
        [SerializeField] private int _bonusDamage;

        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new DrinkBeerCardAction(_damageToOwner, _bonusDamage);
        }
    }
}