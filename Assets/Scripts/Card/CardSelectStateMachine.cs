using Card.TargetSelectors;
using Deck;
using Infrastructure;
using UnityEngine;
using Utilities;

namespace Card
{
    public class CardSelectStateMachine : MonoBehaviour
    {
        private CardState _currentState;
        
        public void Init(CardTargetSelectorsPool cardTargetSelectors, DeckView deckView, FinderUnderCursor finderUnderCursor, IPlayerHolder playerHolder)
        {
            _currentState = new NoneCardState(this, finderUnderCursor, deckView, cardTargetSelectors, playerHolder);
        }

        public void Transition(CardState cardState)
        {
            _currentState = cardState;
        }

        private void Update()
        {
            _currentState?.Update();
        }
    }
}