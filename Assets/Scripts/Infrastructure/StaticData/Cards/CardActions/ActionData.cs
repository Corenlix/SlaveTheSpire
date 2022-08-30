using Card.Actions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards.CardActions
{
    public abstract class ActionData : ScriptableObject
    {
        public abstract ICardAction GetCardAction(DiContainer diContainer);
    }
}