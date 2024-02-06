using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cat/Enemy", menuName = "Scriptable Object/CatData")]
public class CatandEnemy : ScriptableObject
{
    public enum Type { Cat, Enemy}
    // Start is called before the first frame update

    [Header("GongTong")]
    public Type type;
    public int id;
    public string name;

    [Header("# For Cat")]

    public Sprite buttonImage;
    public Sprite CatImage;
    public GameObject Cat;

    [Header("# For Enemy")]
    public float monsterid;
    public float respawntime;
}
