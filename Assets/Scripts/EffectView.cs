using Entities;
using TMPro;
using UnityEngine;

public class EffectView : MonoBehaviour, IAnimatorStateListener
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(int value)
    {
        _text.text = value.ToString();
    }
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(gameObject);
    }
}
