using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Entities.Animations;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

public  class PopUp : MonoBehaviour, IAnimatorStateListener
{
    public PopUpType PopUpType => _popUpType;
    
    [SerializeField] private PopUpType _popUpType;
    
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(gameObject);
    }
}