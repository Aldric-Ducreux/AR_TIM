using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFollow : MonoBehaviour
{
    public GameObject ObjButtonLeft;
    public GameObject ObjButtonRight;
    public GameObject list;
    public Transform cam;
    public List<GameObject> furnitures;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        furnitures = GetAllChilds(list);
        for(int i = 1; i < furnitures.Count; i++)
        {
            furnitures[i].SetActive(false);
        }
        ObjButtonRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            Quaternion cr = cam.rotation;
            ObjButtonLeft.transform.rotation = cr;
            ObjButtonRight.transform.rotation = cr;
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            switch (e.keyCode)
            {
                case KeyCode.Q: swipeFurnitures(true); break;
                case KeyCode.D: swipeFurnitures(false); break;
            }
        }
    }

    public void swipeFurnitures(bool goRight = true)
    {
        if (goRight && index > 0)
        {
            list.transform.Translate(.1f, 0, 0);
            furnitures[index].SetActive(false);
            dettachLeanTouch(furnitures[index]);
            index--;
            furnitures[index].SetActive(true);
            attachLeanTouch(furnitures[index]);
            if (index <= 0)
            {
                ObjButtonRight.SetActive(false);
            }
            ObjButtonLeft.SetActive(true);
        } else if(!goRight &&  index < furnitures.Count-1)
        {
            list.transform.Translate(-.1f, 0, 0);
            furnitures[index].SetActive(false);
            dettachLeanTouch(furnitures[index]);
            index++;
            furnitures[index].SetActive(true);
            attachLeanTouch(furnitures[index]);
            if (index >= furnitures.Count - 1)
            {
                ObjButtonLeft.SetActive(false);
            }
            ObjButtonRight.SetActive(true);
        }
    }

    public List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    public void attachLeanTouch(GameObject go)
    {
        go.AddComponent<LeanPinchScale>();
        LeanTwistRotateAxis script = go.AddComponent<LeanTwistRotateAxis>();

        script.Axis.x = -1;
        script.Axis.y = -1;
    }

    public void dettachLeanTouch(GameObject go)
    {
        Destroy(go.GetComponent<LeanPinchScale>());
        Destroy(go.GetComponent<LeanTwistRotateAxis>());
        go.transform.rotation = Quaternion.Euler(0, -180, 0);
        go.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
