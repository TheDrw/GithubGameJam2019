using UnityEngine;

namespace Drw.CharacterSystems
{
    public interface ICharacterSwitch
    {
        event System.Action OnCharacterSwitch;
        void SwitchOnCommand(Vector3 position, Quaternion setRotation);
        void ForceSwitchOnDeath(Vector3 position, Quaternion setRotation);
        float BaseSwitchCooldownTime { get; }
    }
}