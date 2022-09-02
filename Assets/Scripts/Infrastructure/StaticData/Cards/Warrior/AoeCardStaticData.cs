using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Aoe")]
    public class AoeCardStaticData : CardStaticData
    {
        [SerializeField] private int _damage;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new AoeCardAction(diContainer.Resolve<IEnemiesHolder>(), _damage);
        }
    }
}