using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Subscribes to a game event, and changes the scene using the specified index when it is notified. The scene change can be delayed by a set time.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameEvent incoming;
    [SerializeField] int sceneIndex;
    [SerializeField] int time;

    void Start()
    {
        incoming.Subscribe(InvokeChangeScene);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void InvokeChangeScene()
    {
        Invoke(nameof(SceneChange), time);
    }
}
