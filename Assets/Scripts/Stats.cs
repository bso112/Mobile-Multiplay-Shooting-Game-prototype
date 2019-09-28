using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{

    [SerializeField]
    public float baseValue;

    private List<float> modifiers = new List<float>();

    public float GetValue()
    {
        float result = baseValue;
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
