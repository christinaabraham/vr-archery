using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]


public class Arrow : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    private bool isAttached = false;
    private bool isFired = false;


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void OnTriggerStay()
    {
        AttachArrow();
    }
    void OnTriggerEnter()
    {
        AttachArrow();
    }

    void Update()
    {
        if(isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }  
    }

    public void Fired()
    {
        isFired = true;
    }

    private void AttachArrow()
    {
        var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ArrowManager.Instance.AttachBowToArrow();
            isAttached = true;

        }

    }

}


