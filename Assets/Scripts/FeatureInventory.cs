using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataDefinition;

public class FeatureInventory : MonoBehaviour
{
    [SerializeField] GameObject _pnlInventory;
    [SerializeField] List<Item> items;
    
    public void OpenInventory(){
        _pnlInventory.SetActive(true);
        SetData();
    }

    void SetData(){

    }
}
