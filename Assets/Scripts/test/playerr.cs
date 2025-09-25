using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerr : MonoBehaviour
{
    int score = 0;
    private void OnEnable()
    {
        EventManager.onclick += AddScore;



        
    }

    private void OnDisable()
    {
        EventManager.onclick -= AddScore;
    }

    private void AddScore(int score2)
    {
        Debug.Log("" + score + score2);
    }
}
