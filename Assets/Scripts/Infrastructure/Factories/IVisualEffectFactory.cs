using UnityEngine;

public interface IVisualEffectFactory
{
    DamageEffect SpawnDamageEffect(int damage, Vector3 position);
    PopUp SpawnPopUp(PopUpType type, Vector3 position);
}