using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float List", menuName = "Custom/Float List")]
public class FloatList : ScriptableObject
{
    public List<FloatVar> floatVars = new();
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
        return 0;
    }
}

[System.Serializable]
public class FloatVar
{
    public float value;
    public string name;
}
