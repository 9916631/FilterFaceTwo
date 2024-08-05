using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GlassesSelector : MonoBehaviour
{
    // Array to hold the different glasses prefabs
    [SerializeField]
    private GameObject[] glassesPrefabs;

    // Index of the currently selected glasses
    private int currentGlassesIndex = -1;

    // Store the current color of the glasses
    private Color currentColor = Color.white;

    private ARFaceManager arFaceManager;

    // Dictionary to keep track of instantiated glasses by their TrackableId
    private Dictionary<TrackableId, GameObject> instantiatedGlasses = new Dictionary<TrackableId, GameObject>();

    private void Awake()
    {
        // Get the ARFaceManager component attached to the same GameObject
        arFaceManager = GetComponent<ARFaceManager>();
    }

    private void Start()
    {
        // Show the first pair of glasses when the app starts
        SelectGlasses(0);
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
        // Instantiate glasses for newly added faces
        foreach (var face in args.added)
        {
            InstantiateGlasses(face);
        }

        // Update glasses for updated faces
        foreach (var face in args.updated)
        {
            UpdateGlasses(face);
        }

        // Remove glasses for removed faces
        foreach (var face in args.removed)
        {
            RemoveGlasses(face);
        }
    }

    // Method to select a pair of glasses by index
    public void SelectGlasses(int index)
    {
        if (index >= 0 && index < glassesPrefabs.Length)
        {
            currentGlassesIndex = index;
            UpdateAllGlasses();
        }
    }

    // Method to instantiate glasses on a face
    private void InstantiateGlasses(ARFace face)
    {
        if (currentGlassesIndex >= 0 && currentGlassesIndex < glassesPrefabs.Length)
        {
            var glasses = Instantiate(glassesPrefabs[currentGlassesIndex], face.transform);
            glasses.transform.localPosition = Vector3.zero; // Adjust as needed
            glasses.transform.localRotation = Quaternion.identity; // Adjust as needed
            glasses.tag = "Glasses"; // Tag the glasses for identification

            // Apply the current color to the glasses
            var renderer = glasses.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = currentColor; // Apply the current color
            }

            // Store the instantiated glasses in the dictionary
            instantiatedGlasses[face.trackableId] = glasses;
        }
    }

    // Method to update glasses on a face
    private void UpdateGlasses(ARFace face)
    {
        // Remove the existing glasses
        if (instantiatedGlasses.TryGetValue(face.trackableId, out var glasses))
        {
            glasses.SetActive(false);
            Destroy(glasses);
        }

        // Instantiate new glasses
        if (currentGlassesIndex >= 0 && currentGlassesIndex < glassesPrefabs.Length)
        {
            var newGlasses = Instantiate(glassesPrefabs[currentGlassesIndex], face.transform);
            newGlasses.transform.localPosition = Vector3.zero; // Adjust as needed
            newGlasses.transform.localRotation = Quaternion.identity; // Adjust as needed
            newGlasses.tag = "Glasses"; // Tag the glasses for identification

            // Apply the current color to the new glasses
            var renderer = newGlasses.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = currentColor; // Apply the current color
            }

            // Store the new glasses in the dictionary
            instantiatedGlasses[face.trackableId] = newGlasses;
        }
    }

    // Method to remove glasses from a face
    private void RemoveGlasses(ARFace face)
    {
        if (instantiatedGlasses.TryGetValue(face.trackableId, out var glasses))
        {
            Destroy(glasses);
            instantiatedGlasses.Remove(face.trackableId);
        }
    }

    // Method to update glasses on all tracked faces
    private void UpdateAllGlasses()
    {
        foreach (var face in arFaceManager.trackables)
        {
            UpdateGlasses(face);
        }
    }

    // Method to change the color of the glasses
    public void ChangeColor(Color color)
    {
        // Update the current color
        currentColor = color;

        // Apply the new color to all instantiated glasses
        foreach (var glasses in instantiatedGlasses.Values)
        {
            MeshRenderer renderer = glasses.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }

}
