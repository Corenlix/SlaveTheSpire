using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Cards.CardActions
{
    public class MegaUltraPunchCardAction : ICardAction
    {
        private readonly float _damageMultiplier;
        private readonly float _damageToOwnerMultiplier;
        private DamageProcessor _damageProcessor;
        private Player _cardOwner;

        public MegaUltraPunchCardAction(float damageMultiplier, float damageToOwnerMultiplier)
        {
            _damageMultiplier = damageMultiplier;
            _damageToOwnerMultiplier = damageToOwnerMultiplier;
        }
        
        public void Use(List<Entity> targets, Player cardOwner)
        {
            _cardOwner = cardOwner;
            _damageProcessor = new MultiplyDamageProcessor(_damageMultiplier);
            cardOwner.AttackProcessor.AddDamageProcessor(_damageProcessor);
            cardOwner.AttackProcessor.Attacked += OnAttack;
        }

        private void OnAttack(int damage)
        {
            _cardOwner.EntityHealth.ApplyDamage(Mathf.RoundToInt(damage * _damageToOwnerMultiplier));
            _cardOwner.AttackProcessor.RemoveDamageProcessor(_damageProcessor);
            _cardOwner.AttackProcessor.Attacked -= OnAttack;
        }
    }
}