using System;
using System.Collections.Generic;
using Entities;
using Infrastructure;
using Zenject;

namespace Card
{
    public class CardActivator : ICardActivator
    {
        private readonly DiContainer _diContainer;
        private readonly IPlayerHolder _playerHolder;

        public CardActivator(DiContainer diContainer, IPlayerHolder playerHolder)
        {
            _diContainer = diContainer;
            _playerHolder = playerHolder;
        }

        public bool IsAvailableToUse(CardHolder cardHolder)
        {
            return _playerHolder.Player.Energy.CurrentValue >= cardHolder.CardStaticData.Cost;
        }

        public void Use(CardHolder cardHolder, List<Entity> targets)
        {
            if (!IsAvailableToUse(cardHolder))
                throw new InvalidOperationException();
            
            _playerHolder.Player.Energy.Subtract(cardHolder.CardStaticData.Cost);
            cardHolder.CardStaticData.GetCardActions(_diContainer).ForEach(x=>x.Activate(targets));
        }
    }
}