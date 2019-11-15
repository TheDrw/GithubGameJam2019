using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Drw.CharacterSystems
{
    public class CharacterScheduler : MonoBehaviour
    {
        public bool IsBusy { get; private set; }
        public IScheduler CurrentSchedule { get; private set; }

        public void StartSchedule(IScheduler schedule, bool isBusy = false)
        {
            IsBusy = isBusy;
            if (CurrentSchedule == schedule) return;

            if(CurrentSchedule != null)
            {
                CurrentSchedule.Cancel();
            }

            CurrentSchedule = schedule;
        }

        public void CancelCurrentSchedule()
        {
            IsBusy = false;
            StartSchedule(null);
        }
    }
}