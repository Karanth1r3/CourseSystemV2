
using System;

public interface IActionLinker
{
    public delegate void IActionLinker();
    public event IActionLinker OnValidActionPerformed, OnWrongActionPerformed;

    public void LinkAction();
    public void ProcessValidSignal();
    public void ProcessInvalidSignal();
}
