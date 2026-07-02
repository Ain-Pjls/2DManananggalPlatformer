using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public HazardType hazardType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Characters characters = collision.GetComponent<Characters>();
        if (characters == null) return;

        if (IsSafe(characters.playerType))
        {
            Debug.Log($"{characters.playerType} is safe on {hazardType}");
            return;
        }

        Debug.Log($"{characters.playerType} touched {hazardType} and died!");
        ResetLevel();
    }

    private bool IsSafe(PlayerType playerType)
    {
        switch (hazardType)
        {
            case HazardType.Lava: return playerType == PlayerType.Fireboy;
            case HazardType.Water: return playerType == PlayerType.Watergirl;
            default: return false;
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
