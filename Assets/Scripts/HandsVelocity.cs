using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsVelocity : MonoBehaviour
{
    AudioClip soundMove;

    private void Start()
    {
        soundMove = Resources.Load<AudioClip>("Sounds/speedMove");    
    }

    void Update()
    {
        Vector3 mag = OVRInput.GetLocalControllerVelocity(GetComponent<HandControllerAction>().GetController());

        float valueMagniture = Mathf.Clamp01(mag.magnitude);

        if (valueMagniture > 1)
            GetComponent<AudioSource>().PlayOneShot(soundMove);


    }
}
