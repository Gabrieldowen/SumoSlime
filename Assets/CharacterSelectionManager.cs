using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    private List<string> selectedCharacters = new List<string>();
    public Button dungBeetleButton;
    public Button rolyPolyButton;
    public Button ladyBugButton;
    public Button flatMapButton;
    public Button donutMapButton;
    private Button selectedMapButton;
    public Color selectedColor = Color.yellow;
    public Color notSelectedColorDungBeetle = new Color(0.6f, 0.4f, 0.2f, 1f);
    public Color notSelectedColorRolyPoly = Color.gray;
    public Color notSelectedColorLadyBug = Color.red;
    public TextMeshProUGUI dungBeetleP1Indicator;
    public TextMeshProUGUI ladyBugP1Indicator;
    public TextMeshProUGUI rolyPolyP1Indicator;
    public TextMeshProUGUI dungBeetleP2Indicator;
    public TextMeshProUGUI ladyBugP2Indicator;
    public TextMeshProUGUI rolyPolyP2Indicator;


    private void Start()
    {

        // Initialize default character selections
        ToggleCharacterSelection("DungBeetle"); // For P1
        ToggleCharacterSelection("LadyBug");    // For P2

        // Initialize default map selection
        SelectMap("FlatMap");
        UpdateMapButtonVisual("FlatMap", true); // You'll need to implement this

        // Hide all indicators initially
        HideAllIndicators();
        dungBeetleP1Indicator.enabled = true;
        ladyBugP2Indicator.enabled = true;
    }

    private void UpdateMapButtonVisual(string mapName, bool isSelected)
    {
        Button mapButton = null;
        switch (mapName)
        {
            case "FlatMap":
                mapButton = flatMapButton;
                break;
            case "DonutMap":
                mapButton = donutMapButton;
                break;
        }

        if (mapButton != null)
        {
            ColorBlock colors = mapButton.colors;
            colors.normalColor = isSelected ? selectedColor : Color.green; // Use the not-selected color
            mapButton.colors = colors;
            if (isSelected)
            {
                selectedMapButton = mapButton; // Keep track of the selected map button
            }
        }
    }

    public void ToggleCharacterSelection(string characterName)
    {
        bool isAlreadySelected = selectedCharacters.Contains(characterName);

        if (!isAlreadySelected && selectedCharacters.Count < 2)
        {
            selectedCharacters.Add(characterName);
            UpdatePlayerIndicator(characterName, selectedCharacters.Count);
        }
        else if (isAlreadySelected)
        {
            UpdatePlayerIndicator(characterName, 0); // 0 to indicate deselection
            selectedCharacters.Remove(characterName);
        }
        else
        {
            // If we are trying to add a third character, deselect the first selected
            string toDeselect = selectedCharacters[0];
            UpdatePlayerIndicator(toDeselect, 0); // Deselect the first character
            selectedCharacters.RemoveAt(0);

            selectedCharacters.Add(characterName);
            UpdatePlayerIndicator(characterName, selectedCharacters.Count); // This will be P2 now
        }

        UpdateButtonVisual(characterName, !isAlreadySelected);
        GameManager.Instance.selectedCharacters = selectedCharacters.ToArray();
    }


    private void UpdateButtonVisual(string characterName, bool isSelected)
    {
        Button btn = null;
        Color notSelectedColor = Color.white;

        switch (characterName)
        {
            case "DungBeetle":
                btn = dungBeetleButton;
                notSelectedColor = notSelectedColorDungBeetle;
                break;
            case "RolyPoly":
                btn = rolyPolyButton;
                notSelectedColor = notSelectedColorRolyPoly;
                break;
            case "LadyBug":
                btn = ladyBugButton;
                notSelectedColor = notSelectedColorLadyBug;
                break;
        }
        
        //if (btn != null)
        //{
        //    ColorBlock colors = btn.colors;
        //    colors.normalColor = isSelected ? selectedColor : notSelectedColor;
        //    btn.colors = colors;
        //}
        //else
        //{
        //    Debug.LogError("Button for character " + characterName + " is not set or found.");
        //}
    }
    public void SelectMap(string mapName)
    {
        // Update visual for previously selected map button
        if (selectedMapButton != null)
        {
            ColorBlock colors = selectedMapButton.colors;
            colors.normalColor = Color.green; // Or whatever the not-selected color should be
            selectedMapButton.colors = colors;
        }

        // Set the new selected map
        GameManager.Instance.selectedMap = mapName;

        // Update visual for the newly selected map button
        switch (mapName)
        {
            case "FlatMap":
                selectedMapButton = flatMapButton;
                break;
            case "DonutMap":
                selectedMapButton = donutMapButton;
                break;
            default:
                Debug.LogError("Map name " + mapName + " is not recognized.");
                return;
        }

        ColorBlock selectedColors = selectedMapButton.colors;
        selectedColors.normalColor = selectedColor; // Yellow
        selectedMapButton.colors = selectedColors;
    }
    private void UpdatePlayerIndicator(string characterName, int playerNumber)
    {
        // Hide all indicators by default
        HideAllIndicators();

        // Based on the current selection order, show the correct indicators
        for (int i = 0; i < selectedCharacters.Count; i++)
        {
            string selected = selectedCharacters[i];
            bool isP1 = i == 0; // First in the list is P1, second is P2

            switch (selected)
            {
                case "DungBeetle":
                    if (isP1) dungBeetleP1Indicator.enabled = true;
                    else dungBeetleP2Indicator.enabled = true;
                    break;
                case "LadyBug":
                    if (isP1) ladyBugP1Indicator.enabled = true;
                    else ladyBugP2Indicator.enabled = true;
                    break;
                case "RolyPoly":
                    if (isP1) rolyPolyP1Indicator.enabled = true;
                    else rolyPolyP2Indicator.enabled = true;
                    break;
            }
        }
    }

    private void HideAllIndicators()
    {
        dungBeetleP1Indicator.enabled = false;
        dungBeetleP2Indicator.enabled = false;
        ladyBugP1Indicator.enabled = false;
        ladyBugP2Indicator.enabled = false;
        rolyPolyP1Indicator.enabled = false;
        rolyPolyP2Indicator.enabled = false;
    }


}
