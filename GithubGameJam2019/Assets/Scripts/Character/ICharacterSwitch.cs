namespace Drw.CharacterSystems
{
    public interface ICharacterSwitch
    {
        event System.Action OnCharacterSwitch;
        void Switch(UnityEngine.Vector3 position, UnityEngine.Quaternion setRotation);
        float BaseSwitchCooldownTime { get; }
    }
}