using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Scriptable Object/Round Data", order = int.MaxValue)]
public class Round : ScriptableObject
{
    public int[] monsters;
}
