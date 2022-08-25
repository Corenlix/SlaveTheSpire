using Card;

namespace Deck
{
    public class DeckHolder
    {
        private DeckView _deckView;

        public DeckHolder(DeckView deckView)
        {
            _deckView = deckView;
        }
        
        public void AddCard(CardHolder cardHolder)
        {
            _deckView.AddCard(cardHolder.CardView);
            cardHolder.Used += RemoveCard;
        }

        public void RemoveCard(CardHolder cardHolder)
        {
            cardHolder.Used -= RemoveCard;
            _deckView.RemoveCard(cardHolder.CardView);
        }
    }
}