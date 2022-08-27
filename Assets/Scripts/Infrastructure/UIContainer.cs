using Deck;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeckView _playerDeck;
        [SerializeField] private Button _EndTurnButton;

        public Canvas Canvas => _canvas;
        public DeckView PlayerDeck => _playerDeck;
        public Button EndTurnButton => _EndTurnButton;
    }
}