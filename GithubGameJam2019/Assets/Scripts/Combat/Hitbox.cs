using UnityEngine;
using Drw.Core;

namespace Drw.Combat
{
    public abstract class Hitbox : MonoBehaviour
    {
        [SerializeField] protected CombatConfig combatConfig = null;

        public abstract void ActivateHitbox(bool status);
    }
}