using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataItem")]
public class DataItem : ScriptableObject {
    
    [SerializeField] List<Item> items;
    [SerializeField] List<Weapon> weapons;

    public Item GetItem(int id){
        return items.Find(n=>n.id.Equals(id));
    }
    
    public Weapon GetWeapon(int id){
        return weapons.Find(n=>n.id.Equals(id));
    }

}
