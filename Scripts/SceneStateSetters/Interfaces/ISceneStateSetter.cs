using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneStateSetter
{
    // Start is called before the first frame update
    public void SetSceneState();

    public void LinkWithCourse();
}
