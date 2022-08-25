using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _icon;

        public void Init(int cost, string name, string description, Sprite icon)
        {
            _costText.text = cost.ToString();
            _nameText.text = name;
            _descriptionText.text = description;
            _icon.sprite = icon;
        }
    }
}