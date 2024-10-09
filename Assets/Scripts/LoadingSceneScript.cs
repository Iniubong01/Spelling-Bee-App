using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{

    public GameObject difficultyLevel;
    public Text loadingText;
    private float timer;
    private int dotCount;
   
    
    // Start is called before the first frame update
    void Start()
    {
        float invokeDelay = Random.Range(6, 14);
        Invoke("SetDifficultyLevelActive", invokeDelay);
        dotCount = 0;
        UpdateLoadingText();
    }

    // Update is called once per frame
    void Update()
    {
         timer += Time.deltaTime;

        if (timer >= 1f) // Every second
        {
            dotCount = (dotCount + 1) % 4; // Cycle from 0 to 3
            UpdateLoadingText();
            timer = 0; // Reset timer
        }
    }

    void SetDifficultyLevelActive()
    {
        difficultyLevel.SetActive(true);
    }

    void UpdateLoadingText()
    {
        string dots = new string('.', dotCount);
        loadingText.text = "LOADING" + dots;
    }
}


    

