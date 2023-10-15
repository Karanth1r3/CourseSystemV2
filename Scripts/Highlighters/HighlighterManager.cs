using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class HighlighterManager : MonoBehaviour
{
    [SerializeField] private IHighlightStrategy[] highlighters;
    public enum HighlightingOptions
    {
        outline
    }
    public HighlightingOptions highlightingOptions;

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
        return default(T);
    }
    private void Start()
    {
        switch(highlightingOptions)
        {
            case HighlightingOptions.outline:
                {
                    SetHighlightingMethod(GetStrategy<OutlineHighlighter>());
                    break;
                }
        }
    }
    public void SetHighlightingMethod(IHighlightStrategy strategy)
    {
        currentHighlightingMethod = strategy;
    }

    public static void Highlight(GameObject[] toHighlight)
    {
        currentHighlightingMethod.HighlightObjects(toHighlight);
    }
}
