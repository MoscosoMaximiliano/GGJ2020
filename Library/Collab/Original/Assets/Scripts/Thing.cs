using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thing : MonoBehaviour
{
    Rigidbody m_rb;

    [SerializeField] int m_neededToRepair = 3;

    [SerializeField] int m_actualRepaired;
    [SerializeField] float m_maxTimePoints;
    [SerializeField] float m_pointsGives;

    float m_timePassed;

    [SerializeField]Image fillBar;

    ParticleSystem punchParticle;

   

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        fillBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        punchParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        m_timePassed += Time.deltaTime;

        if (fillBar.fillAmount > 0)
            fillBar.fillAmount -= Time.deltaTime / 100f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!IsGrabbed())
            {
                punchParticle.Play();

                Vector3 mag = OVRInput.GetLocalControllerVelocity(other.gameObject.GetComponent<HandControllerAction>().GetController());

                float valueMagniture = Mathf.Clamp01(mag.magnitude);

                Debug.Log("Punch force: " + mag.magnitude);

                if (mag.magnitude > 1.2f)
                {
                    GiveForce(other.gameObject.transform, mag.magnitude);
                    RepairConstruct();
                }
            }
            
        }
    }

    bool IsGrabbed()
    {
        bool grab = transform.GetComponent<OVRGrabbable>().isGrabbed;

        return grab;
    }

    void GiveForce(Transform handPos, float magnitudeForce)
    {
        Vector3 forceImpulse = transform.position - handPos.position;

        GetComponent<Rigidbody>().AddForce(forceImpulse * magnitudeForce * 10f, ForceMode.Impulse);
    }

    public void RepairConstruct()
    {
        m_actualRepaired++;

        StartCoroutine(FillBarProgress());

        if (m_actualRepaired == m_neededToRepair)
            CompleteReparation();
    }

    void CompleteReparation()
    {

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetColor("_BaseColor", Color.green);

        GetComponent<Renderer>().SetPropertyBlock(block);

        
    }

    IEnumerator FillBarProgress()
    {
        float newValue = 1 / m_neededToRepair;
        float speed = 100f;
        fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, newValue, Time.deltaTime * speed);

        yield return new WaitForSeconds(0.1f);
    }

    
}
