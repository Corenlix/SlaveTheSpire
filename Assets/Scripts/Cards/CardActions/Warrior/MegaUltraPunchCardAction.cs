using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Cards.CardActions
{
    public class MegaUltraPunchCardAction : ICardAction
    {
        private readonly float _damageMultiplier;
        private readonly float _damageToOwnerMultiplier;

        public MegaUltraPunchCardAction(float damageMultiplier, float damageToOwnerMultiplier)
        {
            _damageMultiplier = damageMultiplier;
            _damageToOwnerMultiplier = damageToOwnerMultiplier;
        }
        
        public void Use(List<Entity> targets, Player cardOwner)
        {
            var damageProcessor = new MegaUltraPunchDamageProcessor(_damageMultiplier, _damageToOwnerMultiplier, cardOwner);
            cardOwner.AddDamageProcessor(damageProcessor);
        }

        class MegaUltraPunchDamageProcessor : DamageProcessor
        {
            private readonly float _multiplier;
            private readonly float _damageToOwnerMultiplier;
            private readonly Entity _owner;

            public MegaUltraPunchDamageProcessor(float multiplier, float damageToOwnerMultiplier,  Entity owner)
            {
                _multiplier = multiplier;
                _damageToOwnerMultiplier = damageToOwnerMultiplier;
                _owner = owner;
            }
            
            public int DamageProcess(int damage)
            {
                return Mathf.RoundToInt(damage * _multiplier);
            }

            public void PostDamageProcess(int damage)
            {
                _owner.ApplyDamage(Mathf.RoundToInt(damage*_damageToOwnerMultiplier));
                _owner.RemoveDamageProcessor(this);
            }
        }
    }
}