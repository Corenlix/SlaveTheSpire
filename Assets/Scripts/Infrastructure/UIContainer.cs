using Deck;
using UnityEngine;

namespace Infrastructure
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeckView _playerDeck;

        public Canvas Canvas => _canvas;
        public DeckView PlayerDeck => _playerDeck;
    }
}