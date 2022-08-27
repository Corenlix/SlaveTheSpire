using Card.TargetSelectors;
using Deck;
using Infrastructure;
using Utilities;

namespace Card
{
    public class NoneCardState : CardState
    {
        private readonly CardSelectStateMachine _stateMachine;
        private readonly FinderUnderCursor _finderUnderCursor;
        private DeckView _deckView;
        private readonly CardTargetSelectorsPool _cardTargetSelectorsPool;
        private readonly IPlayerHolder _playerHolder;

        public NoneCardState(CardSelectStateMachine stateMachine, FinderUnderCursor finderUnderCursor, DeckView deckView, CardTargetSelectorsPool cardTargetSelectorsPool, IPlayerHolder playerHolder)
        {
            _stateMachine = stateMachine;
            _finderUnderCursor = finderUnderCursor;
            _deckView = deckView;
            _cardTargetSelectorsPool = cardTargetSelectorsPool;
            _playerHolder = playerHolder;
        }
        
        public override void Update()
        {
            CardHolder cardUnderCursor = _finderUnderCursor.FindObjectUnderCursor<CardHolder>();
            if(cardUnderCursor != null)
                _stateMachine.Transition(new HoldCardState(_stateMachine, _finderUnderCursor, _deckView, cardUnderCursor, _cardTargetSelectorsPool, _playerHolder));
        }
    }
}