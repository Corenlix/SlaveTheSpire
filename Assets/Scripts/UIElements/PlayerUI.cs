using System;
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
            Refresh();
        }

        private void OnEnergyChanged(PlayerEnergy energy)
        {
            _energyText.text = $"{energy.Energy}/{energy.MaxEnergy}";
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            if (_observingPlayer)
                _observingPlayer.Energy.Changed += OnEnergyChanged;
        }

        private void UnSubscribe()
        {
            if (_observingPlayer)
                _observingPlayer.Energy.Changed -= OnEnergyChanged;
        }

        private void Refresh()
        {
            OnEnergyChanged(_observingPlayer.Energy);
        }
    }
}