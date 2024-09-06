using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class PlayerInfo : MonoBehaviour {
    // inven position - id
    public Dictionary<int, int> ownedWeapons = new Dictionary<int, int>();
    public int gold = 1000;

    // type - id
    public Dictionary<int, int> equipment = new Dictionary<int, int>(){
        {(int)TypeItem.Weapon, 1}
    };
}
