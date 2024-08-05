using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectsSelector : MonoBehaviour
{
    // Array to hold the different objects prefabs
    [SerializeField]
    private GameObject[] objectPrefabs;

    // Index of the currently selected objects
    private int currentObjectsIndex = -1;
    private ARFaceManager arFaceManager;

    // Dictionary to keep track of instantiated objects by their TrackableId
    private Dictionary<TrackableId, GameObject> instantiatedObjects = new Dictionary<TrackableId, GameObject>();

    private void Awake()
    {
        // Get the ARFaceManager component attached to the same GameObject
        arFaceManager = GetComponent<ARFaceManager>();
    }

    private void Start()
    {
        // Show the first object when the app starts
        SelectObject(0);
    }

    private void OnEnable()
    {
        // Subscribe to the facesChanged event
        if (arFaceManager != null)
        {
            arFaceManager.facesChanged += OnFacesChanged;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the facesChanged event
        if (arFaceManager != null)
        {
            arFaceManager.facesChanged -= OnFacesChanged;
        }
    }

    // Handler for the facesChanged event
    private void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        // Instantiate objects for newly added faces
        foreach (var face in args.added)
        {
            InstantiateObjects(face);
        }

        // Update objects for updated faces
        foreach (var face in args.updated)
        {
            UpdateObjects(face);
        }

        // Remove objects for removed faces
        foreach (var face in args.removed)
        {
            RemoveObjects(face);
        }
    }

    // Method to select an object by index
    public void SelectObject(int index)
    {
        if (index >= 0 && index < objectPrefabs.Length)
        {
            currentObjectsIndex = index;
            UpdateAllObjects();
        }
    }

    // Method to instantiate objects on a face
    private void InstantiateObjects(ARFace face)
    {
        if (currentObjectsIndex >= 0 && currentObjectsIndex < objectPrefabs.Length)
        {
            var objects = Instantiate(objectPrefabs[currentObjectsIndex], face.transform);
            objects.transform.localPosition = Vector3.zero; // Adjust as needed
            objects.transform.localRotation = Quaternion.identity; // Adjust as needed
            objects.tag = "Objects"; // Tag the glasses for identification            

            instantiatedObjects[face.trackableId] = objects;
        }
    }

    // Method to update objects on a face
    private void UpdateObjects(ARFace face)
    {
        // Remove the existing objects
        if (instantiatedObjects.TryGetValue(face.trackableId, out var objects))
        {
            objects.SetActive(false);
            Destroy(objects);
        }

        // Instantiate new objects
        if (currentObjectsIndex >= 0 && currentObjectsIndex < objectPrefabs.Length)
        {
            var newObjects = Instantiate(objectPrefabs[currentObjectsIndex], face.transform);
            newObjects.transform.localPosition = Vector3.zero; // Adjust as needed
            newObjects.transform.localRotation = Quaternion.identity; // Adjust as needed
            newObjects.tag = "Objects"; // Tag the objects for identification            

            instantiatedObjects[face.trackableId] = newObjects;
        }
    }

    // Method to remove objects from a face
    private void RemoveObjects(ARFace face)
    {
        if (instantiatedObjects.TryGetValue(face.trackableId, out var objects))
        {
            Destroy(objects);
            instantiatedObjects.Remove(face.trackableId);
        }
    }

    // Method to update objetcs on all tracked faces
    private void UpdateAllObjects()
    {
        foreach (var face in arFaceManager.trackables)
        {
            UpdateObjects(face);
        }
    }

}
