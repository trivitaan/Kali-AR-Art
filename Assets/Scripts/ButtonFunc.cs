using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunc : MonoBehaviour
{
    public placeObject placeObjectScript; // Reference to the placeObject script

    private void Awake()
    {
        // Find the placeObject script attached to the same GameObject or a parent GameObject
        placeObjectScript = GetComponentInParent<placeObject>();

        if (placeObjectScript == null)
        {
            Debug.LogError("placeObject script not found.");
        }
    }

    public void GoToARZone()
    {
        // Load ARZone scene.
        SceneManager.LoadScene("ARZone");
    }

    public void GoToContents()
    {
        // Load Contents scene.
        SceneManager.LoadScene("Contents Page");
    }

    public void GoToAbout()
    {
        // Load About scene.
        SceneManager.LoadScene("About Page");
    }
    
    public void GoToTutorial()
    {
        // Load About scene.
        SceneManager.LoadScene("Tutorial Page");
    }

    public void GoToHomepage()
    {
        // Load HomePage scene.
        SceneManager.LoadScene("HomePage");
    }

    public void CloseApp()
    {
        // Close the application (works in standalone builds).
        Application.Quit();
    }

    public void GoToNextObject()
    {
        // Call the GoToNext method in the placeObject script
        if (placeObjectScript != null)
        {
            placeObjectScript.GoToNext();
        }
    }
}
