using TMPro;
using UnityEngine;

/// <summary>
/// Handles dealing with input from the keypad's buttons, playing sound effects, and notifying its GameEvent when the correct code is inputted. Also updates the text to show the user's input.
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
    /// Plays a button click sound effect, then adds the number parameter to the userInput string. A check is then made to see if userInput equals the set password.
    /// If it is, then a GameEvent is notified and the 'correct' sound effect is played. If not, the 'wrong' sound effect is played and the userInput is reset.
    /// </summary>
    /// <param name="number"> The number on the keypad that the player has clicked. </param>
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
