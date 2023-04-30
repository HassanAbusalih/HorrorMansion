using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Subscribes to a game event, and changes the scene using the specificed index when it is notified.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameEvent incoming;
    [SerializeField] int sceneIndex;

    void Start()
    {
        incoming.Subscribe(SceneChange);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
