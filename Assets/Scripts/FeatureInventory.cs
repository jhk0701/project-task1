using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataDefinition;

public class FeatureInventory : MonoBehaviour
{
    [SerializeField] GameObject _pnlInventory;

    [Space(10f)]
    // position - item id
    Dictionary<int, int> _ownedWeapons = new Dictionary<int, int>();
    [SerializeField] GameObject _prefWeapon;
    [SerializeField] List<WeaponUI> _weaponInsts;

    [Space(10f)]
    [SerializeField] Transform _tfBackground;
    [SerializeField] Transform _tfContainer;

    void Start()
    {
        // test : 0 위치에 id 1 아이템
        _ownedWeapons[0] = 1;
    }
    
    public void OpenInventory(){
        _pnlInventory.SetActive(true);
        
        if(_weaponInsts.Count > 0)
            Clear();
        
        Set();
    }

    public void CloseInventory(){
        _pnlInventory.SetActive(false);
        Clear();
    }

    void Set(){
        for (int i = 0; i < _ownedWeapons.Count; i++)
        {
            if(_ownedWeapons[i] > 0)
            {
                // tfBackground.GetChild(i);
                GameObject inst = Instantiate(_prefWeapon, _tfContainer);
                inst.transform.localPosition = _tfBackground.GetChild(i).transform.localPosition;
                
                WeaponUI ui = inst.GetComponent<WeaponUI>();
                ui.Init(_ownedWeapons[i], _tfContainer);

                _weaponInsts.Add(ui);
            }
        }
    }

    void Clear(){
        GameObject destroy = new GameObject();
        for (int i = 0; i < _weaponInsts.Count; i++)
        {
            if(_weaponInsts[i] != null)
                _weaponInsts[i].transform.SetParent(destroy.transform);
        }

        Destroy(destroy);
        _weaponInsts.Clear();
    }
    
    public void Move(){

    }

    public void Migrate(){
        
    }
}
