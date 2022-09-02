using System;
using System.Collections.Generic;
using Entities;
using Infrastructure;
using Infrastructure.StaticData.Cards;
using Zenject;

namespace Cards
{
    public class CardActivator : ICardActivator
    {
        private readonly DiContainer _diContainer;
        private readonly Player _owner;
        private readonly Card _card;
        private readonly CardStaticData _cardStaticData;

        public CardActivator(DiContainer diContainer, Player owner, CardStaticData cardStaticData)
        {
            _diContainer = diContainer;
            _owner = owner;
            _cardStaticData = cardStaticData;
        }

        public bool IsAvailableToUse()
        {
            return _owner.Energy.Energy >= _cardStaticData.Cost;
        }

        public void Use(List<Entity> targets)
        {
            if (!IsAvailableToUse())
                throw new InvalidOperationException();
            
            _owner.Energy.Subtract(_cardStaticData.Cost);
            _cardStaticData.GetCardAction(_diContainer).Use(targets, _owner);
        }
    }
}