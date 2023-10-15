using course;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public enum HighlightingCondition
    {
        OnActivation,
        OnTrigger
    }

    public HighlightingCondition highlightingContext;

    [SerializeField] ITriggerZone trigger;

    [SerializeField] private GameObject[] toHighlight;
    Task task;
    // Start is called before the first frame update
    void Start()
    {
        task = GetComponent<Task>();
        if (task == null) { Debug.LogError("no task is attached to highlighter"); return; }

        if (highlightingContext == HighlightingCondition.OnActivation) 
        {
            task._OnStepActivated.AddListener(Highlight);
            task._OnStepDeactivated.RemoveListener(Highlight);
        } 
        else
        {
            if (trigger == null) return;           
            trigger.onEnter += Highlight;
            trigger.onExit -= Highlight;
        }
    }

    public void Highlight()
    {
        HighlighterManager.Highlight(toHighlight);
    }
}
