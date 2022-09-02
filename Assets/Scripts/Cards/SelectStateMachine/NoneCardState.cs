using Cards.TargetSelectors;
using Deck;
using Utilities;

namespace Cards.SelectStateMachine
{
    internal class NoneCardState : CardState
    {
        private readonly CardSelectStateMachine _stateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly DeckView _deckView;
        private readonly CardTargetSelectorsPool _cardTargetSelectorsPool;

        public NoneCardState(CardSelectStateMachine stateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, CardTargetSelectorsPool cardTargetSelectorsPool)
        {
            _stateMachine = stateMachine;
            _finderUnderCursor = finderUnderCursor;
            _deckView = deckView;
            _cardTargetSelectorsPool = cardTargetSelectorsPool;
        }
        
        public override void Update()
        {
            Card cardUnderCursor = _finderUnderCursor.FindObjectUnderCursor<Card>();
            if(cardUnderCursor != null)
                _stateMachine.Transit(new HoldCardState(_stateMachine, _finderUnderCursor, _deckView, cardUnderCursor, _cardTargetSelectorsPool));
        }
    }
}