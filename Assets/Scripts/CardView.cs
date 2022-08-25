using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void Init(int cost, string name, string description)
    {
        _costText.text = cost.ToString();
        _nameText.text = name;
        _descriptionText.text = description;
    }
}