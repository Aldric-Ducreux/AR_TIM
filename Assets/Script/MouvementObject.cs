using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouvementObject : MonoBehaviour
{
    [SerializeField] private GameObject visualTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        visualTarget = transform.GetChild(0).gameObject;
        visualTarget.AddComponent<LeanPinchScale>();
        LeanTwistRotateAxis script = visualTarget.AddComponent<LeanTwistRotateAxis>();

        script.Axis.x = -1;
        script.Axis.y = -1;

    }
}
