namespace Drw.CharacterSystems
{
    public interface ICharacterSwitch
    {
        void Switch(UnityEngine.Vector3 position, UnityEngine.Quaternion setRotation);
        float CharacterSwitchCooldownTime { get; }
    }
}