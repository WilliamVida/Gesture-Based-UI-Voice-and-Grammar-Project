using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From https://www.youtube.com/watch?v=MjH5rsmYmQY&ab_channel=ElectronicBrain.
public class ButtonSounds : MonoBehaviour
{

    // Declare variables.
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // Set the hover button sound.
    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    // Set the click button sound.
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

}
