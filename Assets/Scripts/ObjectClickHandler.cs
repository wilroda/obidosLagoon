using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;

public class ObjectClickHandler : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to the UI message text
    public TextMeshProUGUI questText; // Reference to the UI message text
    public GameObject messagePanel; // Reference to the UI message panel
    private static int globalClickCount = 0; // Static variable to track the global click count
    private static int globalAnimalsToFind = 0; // Static variable to track the global click count
    public float messageDuration = 2f; // Duration for which the message should be displayed
    public string tagToCount = "Animal"; // Tag to count objects
    public float rayDistance = 1000f; // Ray cast distance
    public LayerMask layerMaskToIgnore;
    bool found = false;
    bool foundAll = false;

    void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCount);

        // Get the count of objects found
        globalAnimalsToFind = objectsWithTag.Length;
    }

    void Update()
    {
        // Check if the player clicked the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            // Check if the ray hits this object
            if (Physics.Raycast(ray, out hit, rayDistance, ~layerMaskToIgnore) && hit.collider.gameObject == gameObject && !found)
            {
                
                globalClickCount++;
                found = true;
                // Show the UI message with the object name
                ShowMessage(hit.collider.gameObject.name);
                QuestMessage(hit.collider.gameObject.name);
            }
        }

        if(globalClickCount == globalAnimalsToFind && !foundAll)
        {   
            foundAll = true;
            ShowMessage("all Animals");
        }
    }

    void ShowMessage(string objectName)
    {
        // Activate the message panel
        messagePanel.SetActive(true);
        
        if(foundAll)
        {
            messageText.text = "Congratulations you found " + objectName + "!";
        }
        else
        {
            // Set the message text
            messageText.text = "You found the " + objectName + "!";
        }
        

        // Start a coroutine to hide the message after a delay
        StartCoroutine(HideMessage());
    }

    void QuestMessage(string objectName)
    {
        if (questText == null) return;

        // Set the message text
        questText.text += "\n" + "Found the " + objectName;
    }

    IEnumerator HideMessage()
    {
        if(foundAll)
        {
            yield return new WaitForSeconds(4f);

            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();
        
            // Reload the current scene
            SceneManager.LoadScene(currentScene.name);

            // Reset variables
            globalClickCount = 0;
            globalAnimalsToFind = 0;
        } 
        else 
        {
            // Wait for the specified duration
            yield return new WaitForSeconds(messageDuration);
        }

        // Deactivate the message panel
        messagePanel.SetActive(false);

        
    }
}