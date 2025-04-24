using System.Collections; // Grants access to collections and data structures like Arrays, Lists, etc.
using System.Collections.Generic; // Grants access to generic collections like List<T>, Dictionary<TKey, TValue>, etc.
using UnityEditor.U2D.Animation;
using UnityEngine; // Grants access to Unity's core classes and functions, such as MonoBehaviour, GameObject, etc.

public class PlatformActivator : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------
    [Header("REFERENCES")]
    public ActivatingPlatform activatingPlatformScript; // Reference to the MovingPlatform.cs script

    // ------------------------- METHODS -------------------------
    private void OnCollisionStay2D(Collision2D actor) // Triggered when a game object collides with the platform
    {
        if (actor.gameObject.CompareTag("Player")) // Only executes if the object colliding with the platform has the tag "Player"
        {
            activatingPlatformScript.isActivated = true; // Toggle the activation state of the platform
        }
    }

    private void OnCollisionExit2D(Collision2D actor) // Triggered when a game object collides with the platform
    {
        if (actor.gameObject.CompareTag("Player")) // Only executes if the object colliding with the platform has the tag "Player"
        {
            activatingPlatformScript.isActivated = false; // Toggle the activation state of the platform
        }
    }

}
