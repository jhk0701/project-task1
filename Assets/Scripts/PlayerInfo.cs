using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class PlayerInfo : MonoBehaviour {
    public Dictionary<int, int> ownedWeapons = new Dictionary<int, int>();
    public int gold = 1000;
    public Equipment equipment;
}
