using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    public abstract class CardActionStaticData : ScriptableObject
    {
        public abstract ICardAction GetCardAction(DiContainer diContainer);
    }
}