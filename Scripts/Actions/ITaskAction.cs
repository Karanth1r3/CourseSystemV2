using course;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  interface ITaskAction
{
    // Start is called before the first frame update
    public void Complete();
    public void Fail();
    public void LinkTask(Task task);

}
