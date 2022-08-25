using System;
using System.Collections.Generic;
using Card.Actions;

namespace Card
{
    public class CardHolder
    {
        public event Action<CardHolder> Used;
        private readonly int _cost;
        private readonly List<ICardAction> _cardActions;
        private readonly CardView _cardView;
        private readonly CardFactory _cardFactory;

        public CardView CardView => _cardView;
        public CardFactory CardFactory => _cardFactory;

        public CardHolder(int cost, List<ICardAction> cardActions, CardView cardView, CardFactory cardFactory)
        {
            _cost = cost;
            _cardActions = cardActions;
            _cardView = cardView;
            _cardFactory = cardFactory;
        }

        public void Use(List<Entity> targets)
        {
            _cardActions.ForEach(x=> x.Activate(targets));
            Used?.Invoke(this);
        }
    }
}