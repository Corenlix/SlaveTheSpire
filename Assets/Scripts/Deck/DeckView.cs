using Card;
using UnityEngine;

namespace Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Transform _cardsContainer;
        
        public void AddCard(CardView card)
        {
            card.transform.SetParent(_cardsContainer);
        }
        
    }
}