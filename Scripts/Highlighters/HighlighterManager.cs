using UnityEngine;

// todo - custom inspector for instant strategy change on enum dropdown selection change
public class HighlighterManager : MonoBehaviour
{
    [SerializeField] private IHighlightStrategy[] highlighters;
    public enum HighlightingOptions
    {
        outline
    }
    public HighlightingOptions highlightingOptions;
    // useless
    public HighlightingOptions options
    {
        get { return highlightingOptions; }
        set
        {
            highlightingOptions = value;
            SwitchStrategy();
        }
    }

    private static IHighlightStrategy currentHighlightingMethod;

    public T GetStrategy<T>()
    {
        foreach(IHighlightStrategy strategy1 in highlighters)
        {
            if (strategy1.GetType() is T)
            {
                return (T)strategy1;
            }
        }
        return default;
    }

    void SwitchStrategy()
    {
        switch (highlightingOptions)
        {
            case HighlightingOptions.outline:
                {
                    SetHighlightingMethod(GetStrategy<OutlineHighlighter>());
                    break;
                }
        }
    }
    private void Start()
    {
       SwitchStrategy();
    }
    public void SetHighlightingMethod(IHighlightStrategy strategy)
    {
        currentHighlightingMethod = strategy;
    }

    public static void Highlight(GameObject[] toHighlight)
    {
        currentHighlightingMethod.HighlightObjects(toHighlight);
    }

    public static void RemoveHighlight(GameObject[] toHighlight)
    {
        currentHighlightingMethod.RemoveHighlight(toHighlight);
    }
}
