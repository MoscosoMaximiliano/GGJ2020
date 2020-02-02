using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]float timePlay = 120f;

    float actualScore;
    GameObject canvas3D;
    GameObject[] props;

    Text points;
    Text time;

    public static GameManager _instanceManager;

    private void Awake()
    {
        canvas3D = GameObject.Find("UI3D").gameObject;
        canvas3D.SetActive(false);

        props = GameObject.FindGameObjectsWithTag("prop");

        points = GameObject.Find("SCORE").GetComponent<Text>();
        time = GameObject.Find("TIME").GetComponent<Text>();
    }

    private void Start()
    {
        _instanceManager = this;
        points.text = actualScore.ToString();
    }


    void Update()
    {
        timePlay -= Time.deltaTime;

        if (timePlay >= 0)
            time.text = timePlay.ToString();

        if (timePlay < 0f)
            FinishGame();
    }

    void FinishGame()
    {
        canvas3D.SetActive(true);

        foreach (var item in props)
        {
            Destroy(item);
        }
    }

    public void UpdateScore(int scorePlus)
    {
        actualScore += scorePlus;

        points.text = actualScore.ToString();
        Debug.Log("PUNTAJE MAS: " + actualScore);
    }
}
