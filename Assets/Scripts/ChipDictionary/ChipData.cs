using UnityEngine;
using System.Collections;

/// <summary>
/// ChipData class. Used to store attack and effect values of a chip
/// on a dictionary. This way, critical data can be modified while
/// creating the object for each chip and not on each script.
/// </summary>
public class ChipData{

    private int _damage;        //Chip total damage
    private Status _effect;     //Chip effect after impact
    private Element _element;    //Chip elemental attribute

    /// <summary>
    /// Initializes Chip Data object
    /// </summary>
    /// <param name="dmg">Amount of damage to be dealt by the chip</param>
    /// <param name="chipEffect">Special effect after chip made impact</param>
    /// <param name="chipElement">Element attribute for current chip</param>
    public ChipData(int dmg, Status chipEffect, Element chipElement) {
        _damage = dmg;
        _effect = chipEffect;
        _element = chipElement;
    }
    /// <summary>
    /// Get/set chip damage value
    /// </summary>
    public int Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }

    /// <summary>
    /// Get/set effect damage value
    /// </summary>
    public Status Effect
    {
        get
        {
            return _effect;
        }

        set
        {
            _effect = value;
        }
    }

    /// <summary>
    /// Get/set element attribute value
    /// </summary>
    public Element Element
    {
        get
        {
            return _element;
        }

        set
        {
            _element = value;
        }
    }
}
