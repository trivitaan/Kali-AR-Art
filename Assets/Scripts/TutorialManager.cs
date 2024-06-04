using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject[] tutorialSections;
    public Button nextButton;
    public Button prevButton;
    public Button quitTutorialButton;
    public Button openTutButton;


    private int currentSection = 0;

    private void Start()
    {
        // Disable previous button at the start
        prevButton.interactable = false;
    }


    private bool isTutorialPanelOpen = false;

    public void OpenMainPanel()
    {
        // Access the text component of the button
        Text buttonText = openTutButton.GetComponentInChildren<Text>();
        
        // Toggle the active state of the mainPanel
        isTutorialPanelOpen = !isTutorialPanelOpen;

        // Set the active state of the mainPanel
        mainPanel.SetActive(isTutorialPanelOpen);

        // Set the button text
        buttonText.text = isTutorialPanelOpen ? "Close" : "Tutorial";

        // Access the colors of the button
        ColorBlock colors = openTutButton.colors;

        // Change the normal color
        colors.normalColor = isTutorialPanelOpen ? Color.red : Color.green;

        // Apply the updated colors to the button
        openTutButton.colors = colors;
    }


    public void ShowNextSection()
    {
        if (currentSection < tutorialSections.Length - 1)
        {
            tutorialSections[currentSection].SetActive(false);
            currentSection++;
            tutorialSections[currentSection].SetActive(true);

            // Enable previous button when moving to a new section
            prevButton.interactable = true;

            // Check if we're at the last section
            if (currentSection == tutorialSections.Length - 1)
            {
                nextButton.interactable = false; // Disable next button
                
                SwitchButton();
            }
        }
    }

    public void SwitchButtonBack()
    {
        // Deactivate the current button
        nextButton.gameObject.SetActive(true);
        // Activate the alternative button
        quitTutorialButton.gameObject.SetActive(false);

    }

    public void ShowPreviousSection()
    {
        if (currentSection > 0)
        {
            tutorialSections[currentSection].SetActive(false);
            currentSection--;
            tutorialSections[currentSection].SetActive(true);

            // Enable next button when moving to a previous section
            nextButton.interactable = true;

            // Check if we're at the first section
            if (currentSection == 0)
            {
                prevButton.interactable = false; // Disable previous button
            }

            SwitchButtonBack();
        }
    }

    private void SwitchButton()
    {
        // Deactivate the current button
        nextButton.gameObject.SetActive(false);
        // Activate the alternative button
        quitTutorialButton.gameObject.SetActive(true);
    }
}
