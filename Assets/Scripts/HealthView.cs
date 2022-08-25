using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _EntityHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private TextMeshProUGUI _hpLabelInfo;

    private void Start()
    {
        _healthBar.InitHealth(_EntityHealth.MaxHealth);
    }

    private void OnEnable()
    {
        _EntityHealth.OnHealthChanged += UpdateView;
    }

    private void OnDisable()
    {
        _EntityHealth.OnHealthChanged -= UpdateView;
    }

    private void UpdateView(int value)
    {
        _hpLabelInfo.text = value + $"/{_EntityHealth.MaxHealth}";
        _healthBar.SetCurrentHealth(value);
    }
}
