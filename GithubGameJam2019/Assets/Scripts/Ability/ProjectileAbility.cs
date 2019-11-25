using Drw.Combat;
using UnityEngine;

namespace Drw.CharacterSystems.Abilities
{
    /// <summary>
    /// Could optimize by using a pooling system, but there aren't gonna a large number of these
    /// so i THINK it is okay just create and del them on the fly.
    /// </summary>
    [CreateAssetMenu(menuName = "Abilities/Projectile Ability")]
    public class ProjectileAbility : Ability
    {
        [SerializeField] Projectile projectile;

        public override void Initialize(GameObject obj)
        {
            
        }

        public override void TriggerAbility(Transform setTransform, Quaternion setQuaternion, CharacterMovement characterMovement = null)
        {
            var go = Instantiate(projectile, setTransform.position, setQuaternion);
            go.gameObject.SetActive(true);
        }
    }
}