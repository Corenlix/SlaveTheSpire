using Deck;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private DeckView _playerDeck;
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private TextValueView _energyView;

        public Canvas Canvas => _canvas;
        public DeckView PlayerDeck => _playerDeck;
        public Button EndTurnButton => _endTurnButton;
        public TextValueView EnergyView => _energyView;
    }
}