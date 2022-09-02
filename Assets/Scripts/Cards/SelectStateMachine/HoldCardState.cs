using Cards.TargetSelectors;
using Deck;
using UnityEngine;
using Utilities;

namespace Cards.SelectStateMachine
{
    internal class HoldCardState : CardState {
        private readonly CardSelectStateMachine _cardSelectStateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly DeckView _deckView;
        private readonly Card _selectedCard;
        private readonly CardTargetSelectorsPool _cardTargetSelectorsPool;

        public HoldCardState(CardSelectStateMachine cardSelectStateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, Card selectedCard, CardTargetSelectorsPool cardTargetSelectorsPool)
        {
            _cardSelectStateMachine = cardSelectStateMachine;
            _finderUnderCursor = finderUnderCursor;
            _deckView = deckView;
            _selectedCard = selectedCard;
            _cardTargetSelectorsPool = cardTargetSelectorsPool;
            deckView.SelectCard(selectedCard);
        }
        
        public override void Update()
        {
            Card cardUnderCursor = _finderUnderCursor.FindObjectUnderCursor<Card>();
            if (_selectedCard != cardUnderCursor)
            {
                _deckView.DeselectCard();
                _cardSelectStateMachine.Transit(new NoneCardState(_cardSelectStateMachine, _finderUnderCursor, _deckView, _cardTargetSelectorsPool));
                return;
            } 
            
            if(Input.GetMouseButtonUp(0))
            {
                if (_selectedCard.IsAvailableToUse())
                    _cardSelectStateMachine.Transit(new SelectingCardState(_cardSelectStateMachine, _finderUnderCursor,
                        _deckView, _cardTargetSelectorsPool, _selectedCard));
                else Debug.Log("Can't use");
            }
        }
    }
}