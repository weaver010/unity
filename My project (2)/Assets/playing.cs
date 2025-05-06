using UnityEngine;
using TMPro;

public class TalkToObject : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI promptText;

    private bool playerIsNear = false;
    private bool hasInteracted = false;

    void Start()
    {
        // Make sure text is hidden at start
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerIsNear && !hasInteracted)
        {
            // Show prompt when player is near
            promptText.gameObject.SetActive(true);

            // Detect Y key press
            if (Input.GetKeyDown(KeyCode.Y))
            {
                // Play audio if not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                // Hide prompt after pressing Y
                promptText.gameObject.SetActive(false);
                hasInteracted = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            hasInteracted = false; // Reset interaction when re-entering
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            promptText.gameObject.SetActive(false); // Hide text when leaving
        }
    }
}
