using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataItem")]
public class DataItem : ScriptableObject {
    
    [SerializeField] List<Weapon> weapons;

    public Weapon GetWeapon(int id){
        return weapons.Find(n=>n.id.Equals(id));
    }

}
