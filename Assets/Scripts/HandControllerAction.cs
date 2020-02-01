using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerAction : MonoBehaviour
{
    public OVRInput.Controller GetController()
    {
        return (gameObject.name == "LeftHandAnchor") ? OVRInput.Controller.LHand : OVRInput.Controller.RHand;
    }
}
