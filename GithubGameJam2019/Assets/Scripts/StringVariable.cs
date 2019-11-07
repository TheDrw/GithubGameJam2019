// ----------------------------------------------------------------------------
// Taken from: https://github.com/roboryantron/Unite2017/
// Video : https://www.youtube.com/watch?v=raQ3iHhE_Kk
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------
using UnityEngine;

namespace RoboRyanTron.Variables
{
    [CreateAssetMenu]
    public class StringVariable : ScriptableObject
    {
        [SerializeField]
        private string value = "";

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}