using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health
{
    public event Action<int> OnHealthChanged;
    public event Action Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _health;
    
    private int _maxHealth;
    private int _health;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
        if (_health < 0)
            Died?.Invoke();
    }
}
