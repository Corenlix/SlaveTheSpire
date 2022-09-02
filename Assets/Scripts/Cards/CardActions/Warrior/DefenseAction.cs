using System.Collections.Generic;
using Entities;

namespace Cards.CardActions.Warrior
{
    public class DefenseAction : ICardAction
    {
        private readonly int _armorPoint;
    

        public DefenseAction(int armorPoint)
        {
            _armorPoint = armorPoint;
        }
    
        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.EntityHealth.AddArmor(_armorPoint);   
        }
    }
}
