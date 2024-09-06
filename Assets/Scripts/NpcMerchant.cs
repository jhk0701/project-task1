using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDefinition;

public class NpcMerchant : Npc {
    // position - id
    public Dictionary<int, int> sellingItem = new Dictionary<int, int>();
    public int gold;

    void Start()
    {
        sellingItem.Add(0, 1);
        sellingItem.Add(1, 2);
    }

    public override void Interact()
    {
        Manager.instance.trade.OpenTrade(this);
    }

}
