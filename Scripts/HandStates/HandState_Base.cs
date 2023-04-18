using HandStates;
using UnityEngine;

namespace HandStates
{
    public abstract class HandState_Base : MonoBehaviour, IHandState
    {
        [SerializeField] GameObject[] linkedObjects;
        public virtual void ActivateLinkedObjects()
        {
            ActivateObjectArray(this.linkedObjects);
        }

        public virtual void ActivateObjectArray(GameObject[] gameObjects)
        {

            foreach (GameObject obj in linkedObjects)
            {
                obj.SetActive(true);
            }
        }

        public virtual void EnterState()
        {
            ActivateLinkedObjects();
        }

        public virtual void ExitState()
        {
            DeactivateLinkedObjects();
        }

        public virtual void DeactivateLinkedObjects()
        {
            DeactivateObjectArray(this.linkedObjects);
        }

        public virtual void DeactivateObjectArray(GameObject[] gameObjects)
        {
            foreach (GameObject obj in linkedObjects)
            {
                obj.SetActive(false);
            }
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }

}
