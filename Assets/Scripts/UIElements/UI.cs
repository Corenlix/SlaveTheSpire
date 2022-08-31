using Deck;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeckView _playerDeck;
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private PlayerUI _playerUI;

        public Canvas Canvas => _canvas;
        public DeckView PlayerDeck => _playerDeck;
        public Button EndTurnButton => _endTurnButton;
        public PlayerUI PlayerUI => _playerUI;
    }
}