using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
