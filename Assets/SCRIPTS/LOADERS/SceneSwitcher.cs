using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 
using UnityEngine.Events; // Grants access to the pre-built custom event systems for triggering functions

public class SceneSwitcher : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------

    [SerializeField] UnityEvent onTriggerEnter; // Event to be invoked when an object enters the trigger
    [SerializeField] UnityEvent onTriggerExit; // Event to be invoked when an object exits the trigger

    // ------------------------- FUNCTIONS -------------------------

    // Trigger Enter Event
    void OnTriggerEnter2D(Collider2D other) // Triggers when a gameObject enters the trigger of the gameObject that has this script
    {
        if (onTriggerEnter != null) // Ensures the event has listeners before invoking
        {
            onTriggerEnter.Invoke(); // Calls all subscribed functions to the event
        }
        Debug.Log($"{other.gameObject.name} Entered the 2D trigger!"); // Logs the name of the object that entered the trigger
    }

    // Trigger Exit Event
    void OnTriggerExit2D(Collider2D other) // Triggers when a gameObject exits the trigger of the gameObject that has this script
    {
        if (onTriggerExit != null) // Ensures the event has listeners before invoking
        {
            onTriggerExit.Invoke(); // Calls all subscribed functions to the event
        }
        Debug.Log($"{other.gameObject.name} Exited the 2D trigger!"); // Logs the name of the object that exited the trigger
    }
}
