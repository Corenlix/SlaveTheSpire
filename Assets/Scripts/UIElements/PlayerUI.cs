using Entities;
using TMPro;
using UnityEngine;

namespace UIElements
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyText;
        private Player _observingPlayer;

        public void ObservePlayer(Player player)
        {
            UnSubscribe();
            _observingPlayer = player;
            Subscribe();
        }

        private void OnPlayerUpdate(Player player)
        {
            _energyText.text = $"{player.Energy}/{player.MaxEnergy}";
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _observingPlayer.PlayerUpdated += OnPlayerUpdate;
        }

        private void UnSubscribe()
        {
            if (_observingPlayer)
                _observingPlayer.PlayerUpdated -= OnPlayerUpdate;
        }
    }
}