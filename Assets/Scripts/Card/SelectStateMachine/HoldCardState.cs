using Card.TargetSelectors;
using Deck;
using Infrastructure;
using UnityEngine;
using Utilities;

namespace Card.SelectStateMachine
{
    internal class HoldCardState : CardState {
        private readonly CardSelectStateMachine _cardSelectStateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly DeckView _deckView;
        private readonly CardGameObject _selectedCard;
        private readonly CardTargetSelectorsPool _cardTargetSelectorsPool;

        public HoldCardState(CardSelectStateMachine cardSelectStateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, CardGameObject selectedCard, CardTargetSelectorsPool cardTargetSelectorsPool)
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
            CardGameObject cardGameObjectUnderCursor = _finderUnderCursor.FindObjectUnderCursor<CardGameObject>();
            if (_selectedCard != cardGameObjectUnderCursor)
            {
                _deckView.DeselectCard();
                _cardSelectStateMachine.Transit(new NoneCardState(_cardSelectStateMachine, _finderUnderCursor, _deckView, _cardTargetSelectorsPool));
                return;
            } 
            
            if(Input.GetMouseButtonDown(0))
            {
                if (_selectedCard.IsAvailableToUse())
                    _cardSelectStateMachine.Transit(new SelectingCardState(_cardSelectStateMachine, _finderUnderCursor,
                        _deckView, _cardTargetSelectorsPool, _selectedCard));
                else Debug.Log("Can't use");
            }
        }
    }
}