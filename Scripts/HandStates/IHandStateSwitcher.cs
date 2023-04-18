using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandStates
{
    public interface IHandStateSwitcher
    {
        // Start is called before the first frame update
        void SwitchState<T>() where T : HandState_Base;
    }
}

