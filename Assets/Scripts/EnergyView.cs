using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Player _player;
    
    private void Start()
    {
        _player.Energy.OnUpdateEnergy += UpdateEnergy;
    }

    private void OnDisable()
    {
        _player.Energy.OnUpdateEnergy += UpdateEnergy;
    }

    private void UpdateEnergy()
    {
        _text.text = $"{_player.Energy.CurrentEnergy}/{_player.Energy.MaxEnergy}";
    }
}
