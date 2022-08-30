using UnityEngine;

[CreateAssetMenu(menuName = "EntityStaticData/Entity Data")]
public class EntityStaticData : ScriptableObject
{
    [SerializeField] private int _shield;

    public int Shield => _shield;   
}
