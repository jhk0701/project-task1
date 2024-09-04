using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDefinition {

    [Serializable] 
    public class Item {
        public string name;
        public int id;
        public int price;
    }

    [Serializable] 
    public class Weapon : Item {
        public int power;
    }
}