using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Valor")]
    public class ValorCardStaticData : CardStaticData
    {
        [SerializeField] private int _healthForDamage;

        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new ValorCardAction(diContainer.Resolve<IEnemiesHolder>(), _healthForDamage);
        }
    }
}