// ----------------------------------------------------------------------------
// Instead of Float this is an Integer
// Taken from: https://github.com/roboryantron/Unite2017/
// Video: https://www.youtube.com/watch?v=raQ3iHhE_Kk
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

namespace RoboRyanTron.Variables
{
    [CreateAssetMenu]
    public class IntegerVariable : ScriptableObject
    {
        public int Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntegerVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(IntegerVariable amount)
        {
            Value += amount.Value;
        }
    }
}