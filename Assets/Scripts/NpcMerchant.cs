using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class NpcMerchant : Npc
{
    [Header("Items")]
    [SerializeField] List<Item> itemsToSell = new List<Item>();

    public override void Interact()
    {
        
    }

}
