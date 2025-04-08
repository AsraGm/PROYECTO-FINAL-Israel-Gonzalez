using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string _name;
    public string _description;
    public GameObject _prefab;
    public Sprite _sprite;
}
