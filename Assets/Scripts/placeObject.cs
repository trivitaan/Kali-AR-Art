using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]

public class placeObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs; // Array of prefabs to spawn
    private int currentPrefabIndex = 0;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Button Next, Prev;
    public GameObject planeDetectedText;

    public Button instantiateButton; // Reference to the UI Button
    public Text statusText; // Reference to the UI Text

    private bool isPlaced = false; // Track if an object is already placed
    private GameObject spawnedObject; // Reference to the spawned object

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += OnPlanesChanged;
        //planeDetectedText.SetActive(false);


        currentPrefabIndex = 0; 
        
        Next.gameObject.SetActive(false);
        Prev.gameObject.SetActive(false);
        instantiateButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check if a plane is found and update the button text accordingly
        if (arPlaneManager.trackables.count > 0 && !isPlaced)
        {
            instantiateButton.gameObject.SetActive(true);
            //instantiateButton.interactable = true;
            instantiateButton.GetComponentInChildren<Text>().text = "Spawn Object";
        }
        else if(isPlaced){
            
            instantiateButton.GetComponentInChildren<Text>().text = "Destroy Object";
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Check if there are any tracked planes
        bool anyPlaneDetected = arPlaneManager.trackables.count > 0;

        // Show/hide the planeDetectedText based on whether any planes are detected
        planeDetectedText.SetActive(anyPlaneDetected);
        if(anyPlaneDetected){
            planeDetectedText.GetComponentInChildren<Text>().text = "Plane Detected";
        }
    }

    public void InstantiateObject()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (isPlaced)
        {
            DestroySpawnedObject();
        }
        else if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            ARRaycastHit hit = hits[0]; // Get the first hit from the list
            Pose pose = hit.pose;
            Vector3 position = pose.position + new Vector3(0f, 0.5f, 0f); // Offset the object in the Y-axis by 0.5 units

            // Modify the rotation to 90 degrees around the X-axis
            Quaternion rotation = Quaternion.Euler(90f, 0f, 180f);

            // Instantiate the prefab based on the current index in the array
            spawnedObject = Instantiate(prefabs[currentPrefabIndex], position, rotation);
            isPlaced = true;
            Next.gameObject.SetActive(true);
            Prev.gameObject.SetActive(true);
            instantiateButton.gameObject.SetActive(false);
            planeDetectedText.SetActive(false);

            //arPlaneManager.enabled = false; // Turn Off plane finder
        }
    }
    public void DestroySpawnedObject()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            isPlaced = false;
        }
    }

    public void GoToNext()
    {
        if (isPlaced)
        {
            // Destroy the currently spawned object before spawning the next one
            DestroySpawnedObject();
        }

        // Increment the index to move to the next prefab in the array
        currentPrefabIndex++;
        if (currentPrefabIndex >= prefabs.Length)
        {
            // If we reach the end of the array, reset the index to start from the beginning
            currentPrefabIndex = 0;
        }

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            ARRaycastHit hit = hits[0];
            Pose pose = hit.pose;
            Vector3 position = pose.position + new Vector3(0f, 0.5f, 0f);

            // Modify the rotation to 90 degrees around the X-axis
            Quaternion rotation = Quaternion.Euler(90f, 0f, 180f);

            // Instantiate the prefab based on the current index in the array
            spawnedObject = Instantiate(prefabs[currentPrefabIndex], position, rotation);
            isPlaced = true;
        }
    }

    public void GoToPreviousObject()
    {
        if (currentPrefabIndex >= 0 && isPlaced)
        {
            DestroySpawnedObject();
            arPlaneManager.enabled = true; // Re-enable the plane manager when the object is destroyed

            // Calculate the index of the previous prefab
            int previousIndex = currentPrefabIndex - 1;
            if (previousIndex < 0)
                previousIndex = prefabs.Length - 1;

            if (arRaycastManager.Raycast(new Vector2(Screen.width / 2f, Screen.height / 2f), hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;
                Vector3 position = pose.position + new Vector3(0f, 0.5f, 0f); // Offset the object in the Y-axis by 0.5 units

                    
                // Modify the rotation to 90 degrees around the X-axis
                Quaternion rotation = Quaternion.Euler(90f, 0f, 180f);

                // Instantiate the previous prefab
                spawnedObject = Instantiate(prefabs[previousIndex], position, rotation);
                isPlaced = true;

                arPlaneManager.enabled = false; // Deactivate the plane manager when the object is placed
                currentPrefabIndex = previousIndex; // Set the current index to the spawned object's index
            }
        }
    }
}
