using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData.Buffs;

namespace Cards.CardActions
{
    public class EatCardAction : ICardAction
    {
        private readonly int _instantHeal;
        private readonly int _healBuffSteps;

        public EatCardAction(int instantHeal, int healBuffSteps)
        {
            _instantHeal = instantHeal;
            _healBuffSteps = healBuffSteps;
        }

        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.EntityHealth.ApplyHeal(_instantHeal);
            cardOwner.BuffsHolderFacade.AddBuff(BuffId.WarriorEat, _healBuffSteps);
        }
    }
}