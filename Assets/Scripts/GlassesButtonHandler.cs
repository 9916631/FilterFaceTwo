using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesButtonHandler : MonoBehaviour
{
    // Reference to the GlassesSelector script which handles the selection and color change of glasses
    [SerializeField]
    private GlassesSelector glassesSelector;

    // Method to select the first pair of glasses
    public void OnGlasses1Selected()
    {
        // Calls the SelectGlasses method in the GlassesSelector script with index 0
        glassesSelector.SelectGlasses(0);
    }

    // Method to select the second pair of glasses
    public void OnGlasses2Selected()
    {
        // Calls the SelectGlasses method in the GlassesSelector script with index 1
        glassesSelector.SelectGlasses(1);
    }

    // Method to select the third pair of glasses
    public void OnGlasses3Selected()
    {
        // Calls the SelectGlasses method in the GlassesSelector script with index 2
        glassesSelector.SelectGlasses(2);
    }

    // Method to select the fourth pair of glasses
    public void OnGlasses4Selected()
    {
        // Calls the SelectGlasses method in the GlassesSelector script with index 3
        glassesSelector.SelectGlasses(3);
    }

    // Method to change the color of the glasses to red
    public void OnColorOption1Selected()
    {
        // Calls the ChangeColor method in the GlassesSelector script with Color.red
        glassesSelector.ChangeColor(Color.red);
    }

    // Method to change the color of the glasses to green
    public void OnColorOption2Selected()
    {
        // Calls the ChangeColor method in the GlassesSelector script with Color.green
        glassesSelector.ChangeColor(Color.green);
    }

    // Method to change the color of the glasses to blue
    public void OnColorOption3Selected()
    {
        // Calls the ChangeColor method in the GlassesSelector script with Color.blue
        glassesSelector.ChangeColor(Color.blue);
    }

}
