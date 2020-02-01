using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    Rigidbody m_rb;

    [SerializeField] int m_neededToRepair = 3;

    [SerializeField] int m_actualRepaired;
    [SerializeField] float m_maxTimePoints;
    [SerializeField] float m_pointsGives;

    float m_timePassed;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_timePassed += Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 mag = OVRInput.GetLocalControllerVelocity(other.gameObject.GetComponent<HandControllerAction>().GetController());

            float valueMagniture = Mathf.Clamp01(mag.magnitude);

            Debug.Log("Punch force: " + mag.magnitude);

            if (mag.magnitude > .15f)
            {
                RepairConstruct();
            }
        }
    }

    public void RepairConstruct()
    {
        m_actualRepaired++;

        if (m_actualRepaired == m_neededToRepair)
            CompleteReparation();
    }

    void CompleteReparation()
    {

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetColor("_BaseColor", Color.green);

        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}
