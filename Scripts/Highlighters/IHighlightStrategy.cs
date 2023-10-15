using UnityEngine;

public interface IHighlightStrategy
{
    public void HighlightObjects(GameObject[] objects);

    public void RemoveHighlight(GameObject[] objects);
}
