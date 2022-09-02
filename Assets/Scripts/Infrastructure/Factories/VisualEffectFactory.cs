using Infrastructure.Assets;
using Infrastructure.Factories;
using UnityEngine;

public class VisualEffectFactory : IVisualEffectFactory
{
    private readonly IAssetProvider _assetProvider;
    private readonly UIHolder _uiHolder;
    private readonly IPrefabFactory _prefabFactory;

    public VisualEffectFactory(IAssetProvider assetProvider, UIHolder uiHolder, IPrefabFactory prefabFactory )
    {
        _assetProvider = assetProvider;
        _uiHolder = uiHolder;
        _prefabFactory = prefabFactory;
    }
    public DamageEffect SpawnDamageEffect(int damage, Vector3 position)
    {
        var damageEffect = _assetProvider.Instantiate<DamageEffect>(AssetPath.DamageEffectPath, position);
        damageEffect.Init(damage);
        return damageEffect;
    }

    public PopUp SpawnPopUp(PopUpType type, Vector3 position)
    {
        var popUp = _prefabFactory.ForType(type);
        popUp.transform.SetParent(_uiHolder.UI.Canvas.transform);
        return popUp;
    }
}