using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeetTrigger : MonoBehaviour
{
    public GameObject otherCharacter; // Assign the other character in Inspector
    public float meetDistance = 1f;
    public bool missionComplete = false;

    void Update()
    {
        if (!missionComplete && otherCharacter != null)
        {
            float distance = Vector2.Distance(transform.position, otherCharacter.transform.position);

            if (distance <= meetDistance)
            {
                missionComplete = true;
                Debug.Log("Mission Complete! Characters have met!");
                // You can add other completion logic here
            }
        }
    }

    void OnDrawGizmos()
    {
        if (otherCharacter != null)
        {
            Gizmos.color = missionComplete ? Color.green : Color.yellow;
            Gizmos.DrawLine(transform.position, otherCharacter.transform.position);
            Gizmos.DrawWireSphere(transform.position, meetDistance);
        }
    }
}
