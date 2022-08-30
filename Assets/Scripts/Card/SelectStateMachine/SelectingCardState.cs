﻿using Card.TargetSelectors;
using Deck;
using Infrastructure;
using UnityEngine;
using Utilities;

namespace Card.SelectStateMachine
{
    internal class SelectingCardState : CardState
    {
        private readonly CardSelectStateMachine _stateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly DeckView _deckView;
        private readonly CardTargetSelectorsPool _cardTargetSelectors;
        private readonly CardHolder _selectedCard;

        public SelectingCardState(CardSelectStateMachine stateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, CardTargetSelectorsPool cardTargetSelectors, CardHolder selectedCard)
        {
            _stateMachine = stateMachine;
            _finderUnderCursor = finderUnderCursor;
            _deckView = deckView;
            _cardTargetSelectors = cardTargetSelectors;
            _selectedCard = selectedCard;

            var selector = cardTargetSelectors.Get(selectedCard.CardStaticData.CardTargetSelectorType);
            selector.StartSelecting(selectedCard);
            selector.Selected += TransitionToNone;
            selectedCard.Destroyed += OnCardDestroyed;
        }

        private void OnCardDestroyed(CardHolder cardHolder)
        {
            TransitionToNone();
        }

        private void TransitionToNone()
        {
            _deckView.DeselectCard();
            var selector = _cardTargetSelectors.Get(_selectedCard.CardStaticData.CardTargetSelectorType);
            selector.Selected -= TransitionToNone;
            _selectedCard.Destroyed -= OnCardDestroyed;
            selector.FinishSelecting();
            _stateMachine.Transit(new NoneCardState(_stateMachine, _finderUnderCursor, _deckView, _cardTargetSelectors));
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