using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;
    public int coins = 0;
    public int gems = 0;
    public HashSet<string> keys = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log($"Coins: {coins}");
    }
    public void AddGem()
    {
        gems++;
        Debug.Log($"Gems: {gems}");
    }
    public void AddKey(string keyID)
    {
        keys.Add(keyID);
        Debug.Log($"Got key: {keyID}");
    }
    public bool HasKey(string keyID) => keys.Contains(keyID);
}
