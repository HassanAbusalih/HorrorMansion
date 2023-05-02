using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsSkip : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float creditsTime;
    float currentTime;
    [SerializeField] TextMeshProUGUI skipText;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > waitTime)
        {
            skipText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }

        if(currentTime > creditsTime)
        {
            SceneManager.LoadScene(0);
        }
    }
}
