using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Thing : MonoBehaviour
{
    Rigidbody m_rb;

    [SerializeField] int m_neededToRepair = 3;

    [SerializeField] int m_actualRepaired;

    float valueChargeBar;

    float m_timePassed;

    [SerializeField] Image fillBar;
    [SerializeField] float fillDecreaseSpeed = 100f;
    [SerializeField] float forceIncrease;

    [SerializeField] GameObject punchParticle;
    [SerializeField] GameObject stateChangeParticle;

    [SerializeField]AudioClip[] punchSounds;
    AudioClip repairedSection;

    [SerializeField] Transform particlePosition;

    bool particleFirstTime;

    Animator anim;

    Vector3 originalSize;


    // Start is called before the first frame update
    void Start()
    {
        particleFirstTime = false;

        valueChargeBar = 1 * 1 / (float)m_neededToRepair;

        Debug.Log("ASDFJOASFJAJSOFJOASFJOAS: " + valueChargeBar);

        anim = GetComponent<Animator>();

        originalSize = transform.localScale;

        punchParticle = Resources.Load<GameObject>("CFX_MagicPoof");
        stateChangeParticle = Resources.Load<GameObject>("CFX3_Hit_SmokePuff");

        particlePosition = transform.GetChild(transform.childCount - 1);

        punchSounds = new AudioClip[3];

        punchSounds[0] = Resources.Load<AudioClip>("Sounds/punch_1");
        punchSounds[1] = Resources.Load<AudioClip>("Sounds/punch_2");
        punchSounds[2] = Resources.Load<AudioClip>("Sounds/punch_3");

        repairedSection = Resources.Load<AudioClip>("Sounds/repair");

        m_rb = GetComponent<Rigidbody>();
        fillBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_timePassed += Time.deltaTime;

        FillAmountControl();
    }

    void FillAmountControl()
    {
        bool changed = false;
        fillBar.fillAmount -= Time.deltaTime / fillDecreaseSpeed;

        if (fillBar.fillAmount <= 1f && fillBar.fillAmount > .5f && anim.GetInteger("state") != 3)
        {
            fillBar.color = Color.green;
            anim.SetInteger("state", 3);
            if (!particleFirstTime)
                particleFirstTime = true;
            else
                changed = true;
        }

        if (fillBar.fillAmount < .50f && fillBar.fillAmount > .25f && anim.GetInteger("state") != 2)
        {
            fillBar.color = Color.yellow;
            anim.SetInteger("state", 2);
            changed = true;
        }
            
        if (fillBar.fillAmount < .25f && anim.GetInteger("state") != 1)
        {
            fillBar.color = Color.red;
            anim.SetInteger("state", 1);
            changed = true;
        }

        if (changed)
        {
            Instantiate(stateChangeParticle, particlePosition.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(repairedSection);
        }
            
            
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            float magRequeried;

            if (!IsGrabbed())
                magRequeried = 1f;
            else
                magRequeried = 1.8f;

            Debug.Log(magRequeried);

                Vector3 mag = OVRInput.GetLocalControllerVelocity(other.gameObject.GetComponent<HandControllerAction>().GetController());

                float valueMagniture = Mathf.Clamp01(mag.magnitude);


                if (mag.magnitude > magRequeried)
                {
                    Instantiate(punchParticle, particlePosition.position, Quaternion.identity);

                    //transform.DOShakeScale(.1f, 1f, 10, 90, false);

                    //transform.DOScale(originalSize, .1f);

                    int randomValue = Random.Range(0, punchSounds.Length - 1);

                    GetComponent<AudioSource>().PlayOneShot(punchSounds[randomValue]);

                    GiveForce(other.gameObject.transform);
                    RepairConstruct();
                }
            
            
        }
    }

    bool IsGrabbed()
    {
        if(gameObject.TryGetComponent<OVRGrabbable>(out OVRGrabbable grab))
        {
            return grab.isGrabbed;
        }

        return false;
 
    }

    void GiveForce(Transform handPos)
    {
        Vector3 forceImpulse = transform.position - handPos.position;

        GetComponent<Rigidbody>().AddForce(forceImpulse * forceIncrease, ForceMode.Impulse);
    }

    public void RepairConstruct()
    {
        m_actualRepaired++;

        FillBarProgress();

        if (m_actualRepaired == m_neededToRepair)
            CompleteReparation();
    }

    void CompleteReparation()
    {
        m_actualRepaired = 0;  

        if (GetActualFill() < .85f)
            GameManager._instanceManager.UpdateScore(CalculateScore());
    }

    int CalculateScore()
    {
        float score = m_neededToRepair * 2f * GetActualFill() / fillDecreaseSpeed;
        return Mathf.FloorToInt(score);
    }

    void FillBarProgress()
    {
        Debug.Log(valueChargeBar);

        fillBar.fillAmount += valueChargeBar;
    }

    float GetActualFill()
    {
        return fillBar.fillAmount;
    }
    
}
