using UnityEngine;

namespace Drw.Core
{
    public interface IMoveable
    {
        void Jump(Vector3 direction, float amount);

        /// <summary>
        ///  really jsut calls the jump function but the word knockback is easier to understand
        ///  when getting hit than calling jump and then moving towards the knockback direction.
        /// </summary>
        /// <param name="knockbackDirection"></param>
        /// <param name="knockbackForce"></param>
        void Knockback(Vector3 knockbackDirection, float knockbackForce);
    }
}