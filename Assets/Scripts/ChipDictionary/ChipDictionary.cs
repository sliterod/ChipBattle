using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipDictionary : MonoBehaviour {

    Dictionary<string, ChipData> chipAttributes;    //Dictionary. Stores all chip data (damage, status).

    void Awake() {
        InitializeDictionary();
    }

    void Start() {
        CreateChipDictionary();
    }

    /// <summary>
    /// Initializes chip dictionary
    /// </summary>
    void InitializeDictionary() {

        //Prevents object to be destroyed
        DontDestroyOnLoad(this);

        //Creates the dictionary where chip data will be stored
        chipAttributes = new Dictionary<string, ChipData>();
    }

    /// <summary>
    /// Adds an entry to the chip dictionary
    /// </summary>
    /// <param name="chipName">The name of the chip (as is on the prefab)</param>
    /// <param name="damage">Damage value</param>
    /// <param name="effect">Effect caused by the chip</param>
    /// <param name="element">Elemental attribute</param>
    void AddEntryToDictionary(string chipName, 
                                int damage, 
                                Status effect, 
                                Element element)
    {

        //Creating chip
        ChipData newChip;
        newChip = new ChipData(damage, effect, element);

        //Saving to dictionary
        chipAttributes.Add(chipName, newChip);
    }

    /// <summary>
    /// Gets all attribute values of a chip by using its prefab name
    /// </summary>
    /// <param name="chipName"></param>
    /// <returns></returns>
    public ChipData GetChipAttributes(string chipName) {

        ChipData chipData;

        if (chipAttributes.TryGetValue(chipName, out chipData))
        {
            Debug.Log("Key found: Chip Prefab " + chipName);
        }
        else {
            Debug.Log("Key not found, please set the correct Chip Name.");
            chipData = new ChipData(0, Status.none, Element.none);
        }
        
        return chipData;
    }

    /// <summary>
    /// Creates a dictionary with all the library of chips on the game.
    /// </summary>
    void CreateChipDictionary() {

        //Attack chips
        AddEntryToDictionary("Cannon", 40, Status.none, Element.none);
        AddEntryToDictionary("GrenadeChip", 40, Status.none, Element.none);
        AddEntryToDictionary("SkyBoxAttkChip", 60, Status.none, Element.none);

        //Healing chips
        AddEntryToDictionary("Heal50", 0, Status.none, Element.none);
        AddEntryToDictionary("Heal100", 0, Status.none, Element.none);
        AddEntryToDictionary("Heal150", 0, Status.none, Element.none);
        AddEntryToDictionary("Heal300", 0, Status.none, Element.none);

        //Support chips


        //Buff/Debuf chips
    }
}