using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransformsResetter : MonoBehaviour
{
    // if interactable can switch states, it may be put in here
    [SerializeField] private ISwitchableInteractable[] switchableInteractables;
    // transforms of these will be reset on Reset() call depending on their individual checkboxes
    [SerializeField] private TransformResetter[] transformResettables;
    private Dictionary<TransformResetter, Vector3> positionMap;
    private Dictionary<TransformResetter, Quaternion> rotationMap;
    private Dictionary<TransformResetter, Vector3> scaleMap;
    
    public void Initialize()
    {
        foreach(TransformResetter resetter in transformResettables)
        {
            if(!resetter.NoPos) positionMap.Add(resetter, resetter.transform.position);

            if (!resetter.NoRot) rotationMap.Add(resetter, resetter.transform.rotation);

            if (!resetter.NoScal) scaleMap.Add(resetter, resetter.transform.localScale);
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void ResetStates()
    {
        foreach(TransformResetter g in transformResettables)
        {
            g.transform.position = positionMap[g];
            g.transform.rotation = rotationMap[g];
            g.transform.localScale = scaleMap[g];
        }
        foreach(ISwitchableInteractable SI in switchableInteractables)
        {
            SI.ResetToDefaultState();
        }
    }   
}
