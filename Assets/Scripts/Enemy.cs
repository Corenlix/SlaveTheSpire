using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private int _damage;
    
    private void Attack(Entity entity)
    {
        entity.TakeDamage(_damage);   
    }
}
