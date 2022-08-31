using Cards.TargetSelectors;
using Deck;
using UnityEngine;
using Utilities;

namespace Cards.SelectStateMachine
{
    public class CardSelectStateMachine : MonoBehaviour
    {
        private CardState _currentState;
        
        public void Init(CardTargetSelectorsPool cardTargetSelectors, DeckView deckView, FinderUnderCursor finderUnderCursor)
        {
            _currentState = new NoneCardState(this, finderUnderCursor, deckView, cardTargetSelectors);
        }

        internal void Transit(CardState cardState)
        {
            _currentState = cardState;
        }

        private void Update()
        {
            _currentState?.Update();
        }
    }
}