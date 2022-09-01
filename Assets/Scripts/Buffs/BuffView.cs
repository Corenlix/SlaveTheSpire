using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Buffs
{
    public class BuffView : MonoBehaviour
    {
        [SerializeField] private Buff buff;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _stepsText;

        private void OnEnable()
        {
            buff.Inited += OnBuffInit;
            buff.StepsRemainChanged += UpdateSteps;
        }

        private void OnBuffInit(Buff buff)
        {
            _icon.sprite = buff.Icon;
        }

        private void UpdateSteps(Buff buff)
        {
            _stepsText.text = buff.StepsRemain.ToString();
        }

        private void OnDisable()
        {
            buff.Inited -= OnBuffInit;
            buff.StepsRemainChanged -= UpdateSteps;
        }
    }
}