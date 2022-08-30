using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        Player Player { get; }
        
        void SetPlayer(Player player);
    }
}