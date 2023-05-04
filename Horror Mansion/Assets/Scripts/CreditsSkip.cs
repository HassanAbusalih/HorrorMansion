using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is placed in the credits scene, and has two variables that represent time. waitTime is the time until the player can skip the credits, while creditsTime is the 
/// total time of the credits. When the time is greater than the waitTime and the player presses space, or when the time is greater than the creditsTime, the main menu scene is loaded.
/// </summary>

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
