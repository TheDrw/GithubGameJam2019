using Drw.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    [CreateAssetMenu(menuName = "Abilities/Projectile Ability")]
    public class ProjectileAbility : Ability
    {
        public float baseDamage;
        public GameObject prefab;
        public Projectile projectile;

        public override void Initialize(GameObject obj)
        {
            
        }

        public override void TriggerAbility()
        {
            
        }

        public void LaunchProjectile(Transform launchTransform, Quaternion launchRotation)
        {
            var go = Instantiate(projectile, launchTransform.position, launchRotation);
            go.gameObject.SetActive(true);
        }
    }
}