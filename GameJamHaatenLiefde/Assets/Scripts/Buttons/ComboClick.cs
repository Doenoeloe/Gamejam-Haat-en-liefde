using UnityEngine;
using UnityEngine.UI;

public class ComboClick : MonoBehaviour
{
    public AudioClip[] comboClips;   // Array of sounds to play
    private AudioSource audioSource; // Reference to the AudioSource
    private int comboIndex = 0;      // Tracks which sound to play next

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayComboSound()
    {
        if (comboClips.Length == 0) return;

        // Play current sound
        audioSource.PlayOneShot(comboClips[comboIndex]);

        // Go to next sound (wrap around)
        comboIndex = (comboIndex + 1) % comboClips.Length;
    }
}
