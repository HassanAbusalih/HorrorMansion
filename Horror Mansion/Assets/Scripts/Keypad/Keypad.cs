using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class Keypad : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    public string GetName() => nameof(outgoing);
    [SerializeField] TextMeshPro codeText;
    [SerializeField] string password = "1234";
    private string userInput;
    private bool check;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip wrongSound;
    AudioSource audioSource;
    bool solved;

    private void Start()
    {
        ResetUserInput();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        AddNumber(number);
        if (check)
        {
            check = false;
            if (userInput == password)
            {
                ResetUserInput();
                if (!solved)
                {
                    audioSource.PlayOneShot(openSound);
                    outgoing.Notify();
                    solved = true;
                }
            }
            else
            {
                ResetUserInput();
                audioSource.PlayOneShot(wrongSound);
            }
        }
    }

    private void AddNumber(string number)
    {
        for (int i = 0; i < userInput.Length; i++)
        {
            if (userInput[i] == '-')
            {
                if (i < userInput.Length - 1)
                {
                    userInput = userInput.Substring(0, i) + number + userInput.Substring(i + 1);
                }
                else
                {
                    userInput = userInput.Substring(0, i) + number;
                    check = true;
                }
                break;
            }
        }
        codeText.text = userInput;
    }

    void ResetUserInput()
    {
        userInput = "";
        for (int i = 0; i < password.Length; i++)
        {
            userInput += "-";
        }
        codeText.text = userInput;
    }

}
