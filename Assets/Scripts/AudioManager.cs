using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;  // Array to hold audio clips
    public AudioSource audioSource; // The audio source component to play the clips

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Start playing random audio clips
        PlayRandomAudio();
    }

    void PlayRandomAudio()
    {
        // Pick a random audio clip from the array
        AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

        // Play the selected audio clip
        audioSource.clip = randomClip;
        audioSource.Play();

        // Wait for the current audio clip to finish before playing the next one
        Invoke("PlayRandomAudio", randomClip.length);
    }
}