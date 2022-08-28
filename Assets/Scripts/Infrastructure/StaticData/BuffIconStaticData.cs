using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Buff Icons")]
    public class BuffIconStaticData : ScriptableObject
    {
        [SerializeField] private List<BuffIconData> _buffIcons;

        public Sprite IconFor(BuffId id) => _buffIcons.First(x => x.Id == id).Icon;
    }

    [Serializable]
    public class BuffIconData
    {
        public BuffId Id;
        public Sprite Icon;
    }
}