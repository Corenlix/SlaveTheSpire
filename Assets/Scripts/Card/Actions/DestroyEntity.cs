﻿using System.Collections.Generic;
using UnityEngine;

namespace Card.Actions
{
    public class TestAction : ICardAction
    {
        private readonly int _testNumber;

        public TestAction(int testNumber)
        {
            _testNumber = testNumber;
        }
        
        public void Activate(List<Entity> targets)
        {
            Debug.Log($"Test action {_testNumber}");
        }
    }
}