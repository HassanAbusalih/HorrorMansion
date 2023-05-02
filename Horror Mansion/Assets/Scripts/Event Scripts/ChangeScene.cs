using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Subscribes to a game event, and changes the scene using the specificed index when it is notified.
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
        Invoke("SceneChange", time);
    }
}
