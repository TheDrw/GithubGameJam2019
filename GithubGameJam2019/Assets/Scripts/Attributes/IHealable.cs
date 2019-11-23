using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Attributes
{
    public interface IHealable
    {
        void Heal(int healAmount);
    }
}