using System.Collections; // Grants access to collections and data structures like Arrays, Lists, etc.
using System.Collections.Generic; // Grants access to generic collections like List<T>, Dictionary<TKey, TValue>, etc.
using UnityEditor.U2D.Animation;
using UnityEngine; // Grants access to Unity's core classes and functions, such as MonoBehaviour, GameObject, etc.

public class WindFanActivator : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------
    [Header("REFERENCES")]
    private Dictionary<GameObject, float> objectsOriginalGravity = new Dictionary<GameObject, float>(); // Dictionary to store the original gravity scale of the blown entities

    [Header("REGULATOR")]
    public float blownIntensity; // The intensity of the wind fan

    [Header("AUDIO")]
    public AudioSource[] audioSource; // Reference to the AudioSource component

    // ------------------------- METHODS -------------------------
    private void OnTriggerEnter2D(Collider2D actor)
    {
        if (actor.gameObject.CompareTag("Player")) // Only executes if the object colliding with the platform has the tag "Player"
        {
            PlayAudio(); // Call the function to play audio
        }
    }

    private void OnTriggerStay2D(Collider2D actor) // Triggered when a game object collides with the platform
    {
        if (actor.gameObject.CompareTag("Player")) // Only executes if the object colliding with the platform has the tag "Player"
        {
            ActivateBlownEffect(actor.gameObject); // Call the function to enable the wind fan
        }
    }

    private void OnTriggerExit2D(Collider2D actor) // Triggered when a game object collides with the platform
    {
        if (actor.gameObject.CompareTag("Player")) // Only executes if the object colliding with the platform has the tag "Player"
        {
           DisableBlownEffect(actor.gameObject); // Call the function to disable the wind fan
           StopAudio(); // Call the function to stop audio
        }
    }

    private void ActivateBlownEffect(GameObject blownEntity) // Function to enable the wind fan effect
    {
        Rigidbody2D rbTemp = blownEntity.GetComponent<Rigidbody2D>(); // Temporary placeholder for the Rigidbody2D component of the blown entity

        if (rbTemp != null && !objectsOriginalGravity.ContainsKey(blownEntity)) // Check if the Rigidbody2D component exists
        {
            objectsOriginalGravity.Add(blownEntity, rbTemp.gravityScale); // Store the original gravity scale of the blown entity
        }
        rbTemp.gravityScale = blownIntensity; // Sets the intensity of the wind fan to the blown entity
    }

    private void DisableBlownEffect(GameObject blownEntity) // Function to disable the wind fan effect
    {
        Rigidbody2D rbTemp = blownEntity.GetComponent<Rigidbody2D>(); // Temporary placeholder for the Rigidbody2D component of the blown entity
        
        if (rbTemp != null && objectsOriginalGravity.ContainsKey(blownEntity)) // Check if the blown entity is in the dictionary
        {
            rbTemp.gravityScale = objectsOriginalGravity[blownEntity]; // Reset the gravity scale of the blown entity to its original value
            objectsOriginalGravity.Remove(blownEntity); // Remove the blown entity from the dictionary
        }
    }

    void PlayAudio() // Play audio files related
    {
        if (audioSource != null)
        {
            foreach (AudioSource audio in audioSource) // Loop through all audio sources
            {
                Debug.Log($"Playing {audio}"); // Debug message
                audio.Play(); // Plays audio clip
            }
        }
    }

    void StopAudio() // Play audio files related
    {
        if (audioSource != null)
        {
            foreach (AudioSource audio in audioSource) // Loop through all audio sources
            {
                audio.Stop(); // Plays audio clip
            }
        }
    }

}
