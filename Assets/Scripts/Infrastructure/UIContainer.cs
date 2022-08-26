using Deck;
using UnityEngine;

namespace Infrastructure
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeckHolder _playerDeck;

        public Canvas Canvas => _canvas;
        public DeckHolder PlayerDeck => _playerDeck;
    }
}