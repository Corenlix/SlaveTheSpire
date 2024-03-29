﻿using System;

namespace Entities
{
    public class PlayerEnergy
    {
        public event Action<PlayerEnergy> Changed;
        
        public int MaxEnergy { get; }
        public int Energy { get; private set; }

        public PlayerEnergy(int energy, int maxEnergy)
        {
            MaxEnergy = maxEnergy;
            Energy = energy;
            
            if (energy > maxEnergy)
                throw new ArgumentOutOfRangeException();

            if (energy < 0)
                throw new ArgumentOutOfRangeException();
        }

        public void Subtract(int amount)
        {
            if (amount > Energy)
                throw new ArgumentOutOfRangeException();

            Energy -= amount;
            Changed?.Invoke(this);
        }

        public void Refresh()
        {
            Energy = MaxEnergy;
            Changed?.Invoke(this);
        }
    }
}