using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomImageTarget : DefaultTrackableEventHandler
{
    public UnityEvent onFound;
    public UnityEvent onLost;

    protected override void OnTrackingFound()
    {
        onFound.Invoke();
    }

    protected override void OnTrackingLost()
    {
        onLost.Invoke();
    }
}
