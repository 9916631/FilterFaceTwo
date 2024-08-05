using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsButtonHandler : MonoBehaviour
{
    // Reference to the ObjectsSelector script which handles the selection of objects
    [SerializeField]
    private ObjectsSelector objectSelector;

    // Method to select the first object
    public void OnObject1Selected()
    {
        // Calls the SelectObject method in the ObjectsSelector script with index 0
        objectSelector.SelectObject(0);
    }

    // Method to select the second object
    public void OnObject2Selected()
    {
        // Calls the SelectObject method in the ObjectsSelector script with index 1
        objectSelector.SelectObject(1);
    }

    // Method to select the third object
    public void OnObject3Selected()
    {
        // Calls the SelectObject method in the ObjectsSelector script with index 2
        objectSelector.SelectObject(2);
    }

    // Method to select the fourth object
    public void OnObject4Selected()
    {
        // Calls the SelectObject method in the ObjectsSelector script with index 3
        objectSelector.SelectObject(3);
    }

}
