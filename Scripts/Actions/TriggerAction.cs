using UnityEngine;

namespace course
{//not sure if such class structure is required
    public class TriggerAction : TaskAction
    {
        [SerializeField] bool isCorrectTrigger;
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isCorrectTrigger)
                {
                    Complete();
                }
                else
                {
                    Fail();
                }
            }
        }
    }
}


