using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowManager : MonoBehaviour
{
    public static ArrowManager Instance;

    private GameObject currentArrow;
    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject arrowPrefab;
    public GameObject stringStartPoint;

    public SteamVR_TrackedObject trackedObj;

    private bool isAttached = false;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    void Start()
    {

    }

    void Update()
    {
        AttachArrow();
        PullString();

    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            // default arrow start position: (-.08, -2.32, 0.45)
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = trackedObj.transform;
            //currentArrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
            //currentArrow.transform.localPosition = new Vector3(0f, 0f, .242f);
            currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
            currentArrow.transform.localRotation = Quaternion.identity;

        }
    }

    private void PullString()
    {
       if (isAttached) {
			float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;
			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition  + new Vector3 (0f, -5f * dist, 0f);

			var device = SteamVR_Controller.Input((int)trackedObj.index);
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Fire();
			}
		}
    }

    private void Fire()
    {
        float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;
        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().Fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 25f * dist;
        r.useGravity = true;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;
        currentArrow = null;
        isAttached = false;
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStartPoint.transform.position;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;

    }
}

