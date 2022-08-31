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
        private readonly IPlayerHolder _playerHolder;
        private readonly Card _card;
        private readonly CardStaticData _cardStaticData;

        public CardActivator(DiContainer diContainer, IPlayerHolder playerHolder, CardStaticData cardStaticData)
        {
            _diContainer = diContainer;
            _playerHolder = playerHolder;
            _cardStaticData = cardStaticData;
        }

        public bool IsAvailableToUse()
        {
            return _playerHolder.Player.Energy >= _cardStaticData.Cost;
        }

        public void Use(List<Entity> targets)
        {
            if (!IsAvailableToUse())
                throw new InvalidOperationException();
            
            _playerHolder.Player.SubtractEnergy(_cardStaticData.Cost);
            _cardStaticData.GetCardActions(_diContainer).ForEach(x=>x.Activate(targets));
        }
    }
}