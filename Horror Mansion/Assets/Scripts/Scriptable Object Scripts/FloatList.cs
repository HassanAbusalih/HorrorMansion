using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scriptable object class that contains a list of FloatVars and methods to access them.
/// </summary>

[CreateAssetMenu(fileName = "New Float List", menuName = "Custom/Float List")]
public class FloatList : ScriptableObject
{
    public List<FloatVar> floatVars = new();
    /// <summary>
    /// Takes a string parameter and returns the FloatVar with a matching name from the floatVars list. If no matching FloatVar is found, it returns null and Debug.Logs an error message.
    /// </summary>
    /// <param name="name"> The string 'name' in the desired FloatVar. </param>
    /// <returns> The FloatVar with the matching name. </returns>
    public FloatVar GetFloatVar(string name)
    {
        foreach(FloatVar entry in floatVars)
        {
            if (entry.name == name)
            {
                return entry;
            }
        }
        Debug.Log("You wrote a wrong string name for a FloatVar!");
        return null;
    }

    /// <summary>
    /// Takes a string parameter and returns the index position of the FloatVar with the matching name in the floatVars list. If no matching FloatVar is found, it returns -1 and Debug.Logs an error message.
    /// </summary>
    /// <param name="name"> The string 'name' in the desired FloatVar. </param>
    /// <returns> The index position of the FloatVar with the matching name. </returns>
    public int GetFloatVarPosition(string name)
    {
        for(int i = 0; i < floatVars.Count; i++)
        {
            if (floatVars[i].name == name)
            {
                return i;
            }
        }
        Debug.Log("You wrote a wrong string name for a FloatVar!");
        return -1;
    }
}

/// <summary>
/// A serializable class that contains a float 'value' and a string 'name'.
/// </summary>
[System.Serializable]
public class FloatVar
{
    public float value;
    public string name;
}
