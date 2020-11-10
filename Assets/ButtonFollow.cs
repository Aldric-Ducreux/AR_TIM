﻿using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFollow : MonoBehaviour
{
    public GameObject goButtonLeft;
    public GameObject goButtonRight;
    public GameObject list;
    public GameObject goAudioAction; 
    public GameObject goAudioGesture;
    public Transform cam;

    private List<GameObject> furnitures;
    private AudioSource audioAction;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        furnitures = GetAllChilds(list);
        for(int i = 1; i < furnitures.Count; i++)
        {
            furnitures[i].SetActive(false);
            attachLeanTouch(furnitures[i]);

        }
        attachLeanTouch(furnitures[0]);
        goButtonRight.SetActive(false);
        audioAction = goAudioAction.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            Quaternion cr = cam.rotation;
            goButtonLeft.transform.rotation = cr;
            goButtonRight.transform.rotation = cr;
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
            audioAction.Play();
            list.transform.Translate(.1f, 0, 0);
            resetLeanTouch(furnitures[index]);
            furnitures[index].SetActive(false);
            index--;
            furnitures[index].SetActive(true);
            if (index <= 0)
            {
                goButtonRight.SetActive(false);
            }
            goButtonLeft.SetActive(true);
        } else if(!goRight &&  index < furnitures.Count-1)
        {
            audioAction.Play();
            list.transform.Translate(-.1f, 0, 0);
            resetLeanTouch(furnitures[index]);
            furnitures[index].SetActive(false);
            index++;
            furnitures[index].SetActive(true);
            if (index >= furnitures.Count - 1)
            {
                goButtonLeft.SetActive(false);
            }
            goButtonRight.SetActive(true);
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

        LeanSelectable selectable = go.AddComponent<LeanSelectable>();
        //selectable.DeselectOnUp = true;
        LeanSelectableRendererColor selectableRender = go.AddComponent<LeanSelectableRendererColor>();
        selectableRender.AutoGetDefaultColor = true;

        LeanPinchScale scale = go.AddComponent<LeanPinchScale>();
        scale.Use.RequiredSelectable = selectable;
        LeanTwistRotateAxis rotateAxis = go.AddComponent<LeanTwistRotateAxis>();
        rotateAxis.Use.RequiredSelectable = selectable;
        rotateAxis.Axis.x = -1;
        rotateAxis.Axis.y = -1;






    }

    public void dettachLeanTouch(GameObject go)
    {
        Destroy(go.GetComponent<LeanPinchScale>());
        Destroy(go.GetComponent<LeanTwistRotateAxis>());
        go.transform.rotation = Quaternion.Euler(0, -180, 0);
        go.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void resetLeanTouch(GameObject go)
    {
        go.transform.rotation = Quaternion.Euler(0, -180, 0);
        go.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
