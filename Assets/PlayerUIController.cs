using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject playerHealthIconPrefab = null;
    public Transform playerHealthIconParent = null;

    private static PlayerUIController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void UpdateHealth(int amount)
    {
        if(amount > instance.playerHealthIconParent.childCount)
        {
            instance.AddHealthsIcon(amount - instance.playerHealthIconParent.childCount);
        } else
        {
            instance.RemoveHealthsIcon(instance.playerHealthIconParent.childCount - amount);
        }
    }

    private void AddHealthsIcon(int amount)
    {
        while(amount > 0)
        {
            Instantiate(playerHealthIconPrefab, Vector3.zero, Quaternion.identity, playerHealthIconParent);
            amount--;
        }
    }

    private void RemoveHealthsIcon(int amount)
    {
        while (amount > 0 && playerHealthIconParent.childCount - amount >= 0)
        {
            Destroy(playerHealthIconParent.GetChild(playerHealthIconParent.childCount - amount).gameObject);
            amount--;
        }
    }
}
