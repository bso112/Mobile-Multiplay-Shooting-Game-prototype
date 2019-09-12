using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{

    [SerializeField]
    public int baseValue;

    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int result = baseValue;
        modifiers.ForEach(x => result += x);
        return result;
    }

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }

   
    
}
