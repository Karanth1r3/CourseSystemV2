using course;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    private bool canHighlight;
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

        if (highlightingContext == HighlightingCondition.OnTrigger) 
        {
            if (trigger == null)
            {
                canHighlight = true;
                task._OnStepActivated.AddListener(Highlight);
                task._OnStepDeactivated.AddListener(RemoveHightlight);
                return;
            }
            trigger.onEnter += Highlight;
            trigger.onExit -= Highlight;
        } 
        else
        {
            task._OnStepActivated.AddListener(AllowHighlight);
            task._OnStepDeactivated.AddListener(RestrictHighlight);
        }
    }

    private void OnDestroy()
    {
        RemoveHightlight();
        if (trigger == null)
        {
            task._OnStepActivated.RemoveListener(Highlight);
            task._OnStepDeactivated.RemoveListener(RemoveHightlight);
        }
        else
        {
            task._OnStepActivated.RemoveListener(AllowHighlight);
            task._OnStepDeactivated.RemoveListener(RestrictHighlight);
        }
    }

    public void AllowHighlight()
    {
        canHighlight = true;
    }

    public void RestrictHighlight()
    {
        canHighlight = false;
        RemoveHightlight();
    }

    public void Highlight()
    {
        if(!canHighlight) { RemoveHightlight(); return; }
        HighlighterManager.Highlight(toHighlight);
    }

    public void RemoveHightlight()
    {
        HighlighterManager.RemoveHighlight(toHighlight);
    }
}
