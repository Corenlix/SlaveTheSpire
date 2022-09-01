using System.Collections.Generic;
using Entities;

namespace Cards.CardActions
{
    public class SaloCardAction : ICardAction
    {
        private readonly int _bonusShield;
        private readonly int _damageToOwner;
        
        public SaloCardAction(int bonusShield, int damageToOwner)
        {
            _bonusShield = bonusShield;
            _damageToOwner = damageToOwner;
        }
        
        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.AddArmor(_bonusShield);
            cardOwner.ApplyDamageThroughArmor(_damageToOwner);
        }
    }
}