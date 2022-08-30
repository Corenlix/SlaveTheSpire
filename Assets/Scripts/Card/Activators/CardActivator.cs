using System;
using System.Collections.Generic;
using Card;
using Card.Actions;
using Entities;

namespace Infrastructure
{
    public class CardActivator : ICardActivator
    {
        private readonly List<ICardAction> _cardActions;
        private readonly IPlayerHolder _playerHolder;
        private readonly int _cardCost;

        public CardActivator(List<ICardAction> cardActions, IPlayerHolder playerHolder, int cardCost)
        {
            _cardActions = cardActions;
            _playerHolder = playerHolder;
            _cardCost = cardCost;
        }

        public bool IsAvailableToUse()
        {
            return _playerHolder.Player.Energy.CurrentValue >= _cardCost;
        }

        public void Use(List<Entity> targets)
        {
            if (!IsAvailableToUse())
                throw new InvalidOperationException();
            
            _playerHolder.Player.Energy.Subtract(_cardCost);
            _cardActions.ForEach(x=>x.Activate(targets));
        }
    }
}