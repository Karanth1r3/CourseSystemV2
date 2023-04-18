using UnityEngine;

namespace HandStates
{

    public interface IHandState
    {
        void EnterState();
        void ExitState();
        void UpdateState();
        void ActivateObjectArray(GameObject[] gameObjects);
        void DeactivateObjectArray(GameObject[] gameObjects);
        void ActivateLinkedObjects();
        void DeactivateLinkedObjects();

    }
}


