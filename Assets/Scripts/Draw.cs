//Refered unity documentation for htc vive controller
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Draw : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonTouchDown = false;
    public bool triggerButtonTouch = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public GameObject prefab;
    public GameObject VRPawn;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
       
        triggerButtonTouchDown = controller.GetTouchDown(triggerButton);
        triggerButtonTouch = controller.GetTouch(triggerButton);

    
        if (triggerButtonTouchDown)
        {
            VRPawn.GetComponent<BrushController>().proceed_start(controller.transform.pos);
        }
        else if (triggerButtonTouch)
        {
            VRPawn.GetComponent<BrushController>().proceed_create(controller.transform.pos);
        }
     
    }
}
