using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Energy Energy => _energy;
    
    private Energy _energy;

    private void Start()
    {
        _energy = new Energy(3, 3);
    }
}
