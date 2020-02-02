using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionScene : MonoBehaviour
{
    [SerializeField] GameObject punchParticle;
    [SerializeField] AudioClip[] punchSounds;
    // Start is called before the first frame update
    void Start()
    {
        punchParticle = Resources.Load<GameObject>("CFX_MagicPoof");

        //punchSounds[0] = Resources.Load<AudioClip>("Sounds/punch_1");
        //punchSounds[1] = Resources.Load<AudioClip>("Sounds/punch_2");
        //punchSounds[2] = Resources.Load<AudioClip>("Sounds/punch_3");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Instantiate(punchParticle, transform.position, Quaternion.identity);

            int randomValue = Random.Range(0, punchSounds.Length - 1);

            //GetComponent<AudioSource>().PlayOneShot(punchSounds[randomValue]);

            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<SceneMethodsVariables>().ActionSceneChanger();
    }
}
