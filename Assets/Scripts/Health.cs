using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public event Action<int> OnHealthChanged;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _health;
    
    [SerializeField] private int _maxHealth;
    private int _health;
    
    public void DealDamage(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);

        if (_health >= 0) return;
        Die();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _health = _maxHealth;
        OnHealthChanged?.Invoke(_health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
