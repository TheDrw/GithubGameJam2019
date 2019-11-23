namespace Drw.CharacterSystems
{
    public enum CharacterState
    {
        Idle, // Flex state that can be flexed to any other state. the default state
        Grounded, Moving, Airborne, Climbing, Rolling, // movement
        GotHit, Stunned, Slowed, Injured, Dead, // Health Effects on Character
        InMenu, // In the menus
        CharacterSwitching, // switch character
        Attacking, Casting, Consuming, Interacting, Evading // actions
    }
}