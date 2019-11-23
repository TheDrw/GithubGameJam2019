using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Attributes
{
    public interface IDamageable
    {
        void Damage(int damageAmount);
    }
}