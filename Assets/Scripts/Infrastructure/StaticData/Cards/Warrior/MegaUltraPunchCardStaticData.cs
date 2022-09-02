using Cards;
using Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.CardDataMenuName+"Warrior/Mega ultra punch")]
    public class MegaUltraPunchCardStaticData : CardStaticData
    {
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private float _damageToOwnerMultiplier;
        
        public override ICardAction GetCardAction(DiContainer diContainer)
        {
            return new MegaUltraPunchCardAction(_damageMultiplier, _damageToOwnerMultiplier);
        }
    }
}