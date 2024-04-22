using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private void Start()
    {
        
        UpdateButtonVisual("DungBeetle", true);
        UpdateButtonVisual("LadyBug", true);

       
        selectedCharacters.Add("DungBeetle");
        selectedCharacters.Add("LadyBug");

       
        UpdateMapButtonVisual("FlatMap", true);
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
        }
        else if (isAlreadySelected)
        {
            selectedCharacters.Remove(characterName);
        }
        else
        {
            // If we are trying to add a third character, deselect the first selected
            string toDeselect = selectedCharacters[0];
            selectedCharacters.RemoveAt(0);
            UpdateButtonVisual(toDeselect, false);

            selectedCharacters.Add(characterName);
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

        if (btn != null)
        {
            ColorBlock colors = btn.colors;
            colors.normalColor = isSelected ? selectedColor : notSelectedColor;
            btn.colors = colors;
        }
        else
        {
            Debug.LogError("Button for character " + characterName + " is not set or found.");
        }
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
}
