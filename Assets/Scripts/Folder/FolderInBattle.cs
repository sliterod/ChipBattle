using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FolderInBattle : MonoBehaviour {

    BattleHud battleHud;
    Gamestate gamestate;
    CharacterControl characterControl;

    List<string> equippedFolder;        //Current equipped folder.
    string[] selectionChips;            //Chips to be shown on selection screen.
    string[] guideChips;                //Currently selected chips while selection screen is being shown
    string[] inUseChips;                //Chips currently in use after selection screen is hidden

    public string[] SelectedChips
    {
        get
        {
            return selectionChips;
        }

        set
        {
            selectionChips = value;
        }
    }


    // Use this for initialization
    void Awake () {

        characterControl = GameObject
                            .Find("Player1")
                            .GetComponent<CharacterControl>();

        battleHud = GameObject
                    .Find("HUD")
                    .GetComponent<BattleHud>();

        gamestate = GameObject
                    .Find("Gamestate")
                    .GetComponent<Gamestate>();

        inUseChips = new string[4];

        LoadCurrentEquippedFolder();
        //SetSelectionScreenChips();
	}
	

    /**********************************************************
                            FOLDER LOADING
    **********************************************************/

    /// <summary>
    /// Loads data from current equipped chip folder on a string list
    /// and sorts its contents
    /// </summary>
    void LoadCurrentEquippedFolder() {
        equippedFolder = new List<string>()
        {   "Cannon",
            "Cannon",
            "Heal50",
            "Heal100",
            "Heal150",
            "Heal300",
            "Cannon",
            "GrenadeChip"/*,
            "GrenadeChip",
            "GrenadeChip",
            "Cannon",
            "Cannon",
            "GrenadeChip",
            "GrenadeChip"*/
        };
    }

    /// <summary>
    /// Set the first six chips of folder list
    /// to the selection screen
    /// </summary>
    void SetSelectionScreenChips() {

        string[] equippedFolderArray;

        equippedFolderArray = equippedFolder.ToArray();

        //Checking if folder array has enough available chips
        if (equippedFolderArray.Length >= 6)
        {
            selectionChips = new string[6];
        }
        else {
            selectionChips = new string[equippedFolderArray.Length];
        }

        //Checking if there is any available chips on folder
        if (selectionChips.Length > 0)
        {
            for (int i = 0; i < selectionChips.Length; i++)
            {
                selectionChips[i] = equippedFolderArray[i];
            }

            battleHud.SetChipList(selectionChips);
        }
        else {
            battleHud.SetChipList();
        }
    }


    /**********************************************************
                        FOLDER SETUP (BATTLE)
    **********************************************************/
    /// <summary>
    /// Sets chip on button slot to use. Applies to both UI and Player
    /// </summary>
    /// <param name="selectedChips">Current selection on chips</param>
    void SetChipOnSlot(string[] selectedChips) {

        //Setting chips on array
        inUseChips = selectedChips;

        //Setting chips on player
        for (int i = 0; i < inUseChips.Length; i++) {
            string chipName = inUseChips[i];

            if (chipName != null) {
                Debug.Log("Chip not empty, setting on slot");
                characterControl.setChip(chipName, i + 1);
                RemoveChipFromFolder(chipName);
            }
        }

    }

    /**********************************************************
                        FOLDER UPDATE (BATTLE)
    **********************************************************/
    /// <summary>
    /// Removes a selected chip from current in-use folder
    /// </summary>
    /// <param name="chipToRemove">Name of the chip to remove</param>
    void RemoveChipFromFolder(string chipToRemove) {
        Debug.Log("Removing chip from folder");
        equippedFolder.Remove(chipToRemove);

        Debug.Log("Remaining chips: "+ equippedFolder.Count);
        foreach (string chip in equippedFolder) {
            Debug.Log(chip);
        }
    }
}
