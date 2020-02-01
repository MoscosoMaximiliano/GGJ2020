using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float timePlay = 20f;

    void Update()
    {
        timePlay -= Time.deltaTime;

        if (timePlay < 0f)
            Debug.Log("FINISH GAME");
    }
}
