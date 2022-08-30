using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Buffs
{
    public class BuffView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _stepsText;
        
        public void Init(Sprite icon, int steps)
        {
            _icon.sprite = icon;
            UpdateSteps(steps);
        }

        public void UpdateSteps(int steps)
        {
            _stepsText.text = steps.ToString();
        }
    }
}