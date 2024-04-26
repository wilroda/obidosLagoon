using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Token> inventory;

    private List<Token> toAdd = new List<Token>();
    private List<Token> toRemove = new List<Token>();

    private static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inventory = new List<Token>();        
    }

    private void LateUpdate()
    {
        // Commit changes
        foreach (var t in toAdd) inventory.Add(t);
        toAdd.Clear();

        foreach (var t in toRemove) inventory.Remove(t);
        toRemove.Clear();
    }

    public static void AddToken(Token token)
    {
        if (instance == null) instance = GameObject.FindObjectOfType<InventoryManager>();

        instance.toAdd.Add(token);
    }

    public static void RemoveToken(Token token)
    {
        if (instance == null) instance = GameObject.FindObjectOfType<InventoryManager>();

        instance.toRemove.Add(token);
    }

    public static bool HasToken(Token token)
    {
        if (instance == null) instance = GameObject.FindObjectOfType<InventoryManager>();

        return (instance.inventory.IndexOf(token) != -1);
    }

    public static int Count(Token token)
    {
        if (instance == null) instance = GameObject.FindObjectOfType<InventoryManager>();

        int count = 0;

        foreach (var t in instance.inventory)
        {
            if (t == token) count++;
        }

        return count;
    }
}
