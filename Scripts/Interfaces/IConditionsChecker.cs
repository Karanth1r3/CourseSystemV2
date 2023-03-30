using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditionsChecker 
{
    // Start is called before the first frame update
    public void HandleCondition(bool state);

    public void HandleUnfailableCondition();

    public bool ReturnCheckResult();
}
