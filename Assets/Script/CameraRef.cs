using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRef : MonoBehaviour
{
    [SerializeField] private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ModelFollower>().cam = cam;
        }
    }
}
