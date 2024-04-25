using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Token> inventory;

    private static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inventory = new List<Token>();        
    }

    public static void AddToken(Token token)
    {
        instance.inventory.Add(token);
    }

    public static void RemoveToken(Token token)
    {
        instance.inventory.Remove(token);
    }

    public static bool HasToken(Token token)
    {
        return (instance.inventory.IndexOf(token) != -1);
    }

    public static int Count(Token token)
    {
        int count = 0;

        foreach (var t in instance.inventory)
        {
            if (t == token) count++;
        }

        return count;
    }
}
