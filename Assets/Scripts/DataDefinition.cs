using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDefinition {

    [Serializable] 
    public class Item {
        public string name;
        public int id;
        public int grade;
        public int price;
    } 
    public enum TypeItem : int {
        Weapon = 0,
        Armor = 1
        /// Çï¸ä 2, °ß°© 3...
    }
    [Serializable] 
    public class Weapon : Item {
        public int power;
        public TypeItem type;
        public Sprite sprite;
        public GameObject pref;
    }

}