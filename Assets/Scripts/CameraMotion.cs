using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    private static Script target = null;

    public static Script Target
    {
        get { return target; }
        set
        {
            if (target != null)
                target.ToogleSelected();
            if (target != value)
            {
                target = value;
                target.ToogleSelected();
            }                
            else
                target = null;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }
    }
}
