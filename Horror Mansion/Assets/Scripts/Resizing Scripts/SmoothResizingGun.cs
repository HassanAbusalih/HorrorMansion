using UnityEngine;

/// <summary>
/// - Takes input from player
/// - Does a raycast to check if the player is looking at a SmoothResizable
/// - If they are, a public method inside SmoothResizable is called to resize the object up or down
/// - Also plays a sound effect that is pitched up or down based on the size of the object. If the min/max size is reached, a different sound effect is played.
/// </summary>

public class SmoothResizingGun : MonoBehaviour
{
    [SerializeField] KeyCode enlargeButton = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkButton = KeyCode.Mouse1;
    [SerializeField] FloatList resizeSpeed;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip resizeSound;
    [SerializeField] AudioClip limitSound;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    float resizingSpeed;
    SmoothResizable resizable;
    Vector3 screenCenter = new (0.5f, 0.5f);
    Camera rayDirection;

    private void Start()
    {
        rayDirection = FindObjectOfType<Camera>();
        resizingSpeed = resizeSpeed.GetFloatVar("Resize Speed").value;
    }

    void Update()
    {
        if (Input.GetKey(enlargeButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                float sizePercentage = resizable.Enlarge(resizingSpeed);
                if (source != null)
                {
                    PlayAudio(sizePercentage);
                }
            }
        }
        else if (Input.GetKey(shrinkButton))
        {
            CheckForResizable();
            if (resizable != null)
            {
                float sizePercentage = resizable.Shrink(resizingSpeed);
                if (source != null)
                {
                    PlayAudio(sizePercentage);
                }
            }
        }
        if (source.isPlaying && (!(Input.GetKey(enlargeButton) || Input.GetKey(shrinkButton)) || resizable == null) && source.clip != limitSound)
        {
            source.volume -= 10 * Time.deltaTime;
            if (source.volume <= 0)
            {
                source.Stop();
            }
        }
    }

    private void PlayAudio(float sizePercentage)
    {
        if ((Input.GetKey(enlargeButton) || Input.GetKey(shrinkButton)) && !source.isPlaying)
        {
            source.clip = resizeSound;
            source.volume = 0;
            source.Play();
        }
        if (source.volume < 1)
        {
            source.volume += 10 * Time.deltaTime;
        }
        if (source.clip == resizeSound)
        {
            source.pitch = Mathf.Lerp(minPitch, maxPitch, sizePercentage);
        }
        if (sizePercentage <= 0 || sizePercentage >= 1)
        {
            source.clip = limitSound;
            source.pitch = 1;
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    void CheckForResizable()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayDirection.ViewportPointToRay(screenCenter), out hit, 3.5f, LayerMask.GetMask("Default"))
            || Physics.SphereCast(rayDirection.ViewportPointToRay(screenCenter), 0.2f, out hit, 3.5f, LayerMask.GetMask("Default")))
        {
            if (hit.transform.TryGetComponent<SmoothResizable>(out resizable))
            {
                return;
            }
        }
        resizable = null;
    }
}
