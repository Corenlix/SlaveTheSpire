using Card.TargetSelectors;
using Deck;
using Infrastructure;
using UnityEngine;
using Utilities;

namespace Card
{
    public class SelectingState : CardState
    {
        private readonly CardSelectStateMachine _stateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly DeckView _deckView;
        private readonly CardTargetSelectorsPool _cardTargetSelectors;
        private readonly CardHolder _selectedCard;
        private readonly IPlayerHolder _playerHolder;

        public SelectingState(CardSelectStateMachine stateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, CardTargetSelectorsPool cardTargetSelectors, CardHolder selectedCard, IPlayerHolder playerHolder)
        {
            _stateMachine = stateMachine;
            _finderUnderCursor = finderUnderCursor;
            _deckView = deckView;
            _cardTargetSelectors = cardTargetSelectors;
            _selectedCard = selectedCard;
            _playerHolder = playerHolder;

            var selector = cardTargetSelectors.Get(selectedCard.CardStaticData.CardTargetSelectorType);
            selector.StartSelecting(selectedCard);
            selector.Selected += OnCardUsed;
        }

        private void OnCardUsed()
        {
            _playerHolder.Energy.Subtract(_selectedCard.CardStaticData.Cost);
            TransitionToNone();
        }

        private void TransitionToNone()
        {
            _deckView.DeselectCard();
            var selector = _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType);
            selector.Selected -= OnCardUsed;
            selector.FinishSelecting();
            _stateMachine.Transition(new NoneCardState(_stateMachine, _finderUnderCursor, _deckView, _cardTargetSelectors, _playerHolder));
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                TransitionToNone();
            }
        }
    }
}