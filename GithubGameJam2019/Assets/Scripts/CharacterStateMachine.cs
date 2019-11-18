﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.CharacterSystems
{
    /// <summary>
    /// This is about to get messy fast.
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterStateMachine")]
    public class CharacterStateMachine : ScriptableObject
    {
        /// <summary>
        /// If the state that was set was successfully set, this will return true.
        /// Should really only be called right after SetCharacterState(...), otherwise will return wrong results.
        /// Not sure if this is the best way.
        /// </summary>
        public bool WasSetStateSuccessful { get; private set; }

        public CharacterState CurrentState { get; private set; } = CharacterState.Idle;
        private CharacterState previousState = CharacterState.Idle;
        private IScheduler previousSchedule = null;

        /// <summary>
        /// Sets the character state. If the state is the same as the previous, it will exit.
        /// When switching to the next state, the previous state will cancel its action.
        /// If state switch was successful, you can call WasSetStateSuccessful right after
        /// to see if it was set.
        /// </summary>
        /// <param name="setState"></param>
        /// <param name="currentSchedule"></param>
        public void SetCharacterState(CharacterState setState, IScheduler currentSchedule = null)
        {
            // Reset to always false unless set was successful or if currState is setstate
            WasSetStateSuccessful = false; 

            if (CurrentState == setState)
            {
                return;
            }

            switch(setState)
            {
                case CharacterState.Moving:
                    SetToMovingState(setState);
                    break;
                case CharacterState.Airborne:
                    SetToAirborneState(setState);
                    break;
                case CharacterState.Grounded:
                    SetToGroundedState(setState);
                    break;
                case CharacterState.Casting:
                    SetToCastingState(setState);
                    break;
                case CharacterState.Attacking:
                    SetToAttackingState(setState);
                    break;
                case CharacterState.Switching:
                    SetToSwitchingState(setState);
                    break;
                default:
                    CurrentState = CharacterState.Idle;
                    WasSetStateSuccessful = true;
                    break;
            }

            if (WasSetStateSuccessful)
            {
                Debug.Log($"Switching from {previousState} to {setState} and canceling {previousSchedule}");

                if(previousSchedule != null)
                    previousSchedule.Cancel();

                previousSchedule = currentSchedule;
            }
        }

        private void SetToSwitchingState(CharacterState setState)
        {
            if(CurrentState == CharacterState.Grounded || 
                CurrentState == CharacterState.Moving)
            {
                WasSetStateSuccessful = true;
                previousState = CurrentState;
                CurrentState = setState;
            }
        }

        private void SetToAttackingState(CharacterState setState)
        {
            if(CurrentState != CharacterState.Casting && CurrentState != CharacterState.Airborne) 
            {
                WasSetStateSuccessful = true;
                previousState = CurrentState;
                CurrentState = setState;
            }
        }

        private void SetToCastingState(CharacterState setState)
        {
            if (CurrentState != CharacterState.Airborne)
            {
                WasSetStateSuccessful = true;
                previousState = CurrentState;
                CurrentState = setState;
            }
        }

        private void SetToGroundedState(CharacterState setState)
        {
            if (CurrentState != CharacterState.Casting && CurrentState != CharacterState.Attacking)
            {
                WasSetStateSuccessful = true;
                previousState = CurrentState;
                CurrentState = setState;
            }
        }

        private void SetToAirborneState(CharacterState setState)
        {
            WasSetStateSuccessful = true;
            previousState = CurrentState;
            CurrentState = setState;
        }

        private void SetToMovingState(CharacterState setState)
        {
            if (CurrentState != CharacterState.Casting && 
                CurrentState != CharacterState.Attacking)
            {
                WasSetStateSuccessful = true;
                previousState = CurrentState;
                CurrentState = setState;
            }
        }

        public bool IsInMoveableState()
        {
            return CurrentState == CharacterState.Grounded ||
                    CurrentState == CharacterState.Moving ||
                    CurrentState == CharacterState.Airborne ||
                    CurrentState == CharacterState.Idle;
        }
    }
}