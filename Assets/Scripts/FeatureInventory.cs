using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataDefinition;
using System.Linq;

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
    [SerializeField] Text _txtOwnedGold;


    public void OpenInventory(){
        if(_pnlInventory.activeInHierarchy || Manager.instance.trade.IsTrading())
            return;

        _ownedWeapons = Manager.instance.playerInfo.ownedWeapons;
        _txtOwnedGold.text = $"{Manager.instance.playerInfo.gold} G";

        _pnlInventory.SetActive(true);
        Set();
    }

    public void CloseInventory(){
        Manager.instance.playerInfo.ownedWeapons = _ownedWeapons;
        
        _pnlInventory.SetActive(false);
        Clear();
    }

    void Set(){
        List<int> keys = _ownedWeapons.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            int pos = keys[i];
            if(_ownedWeapons[pos] > 0)
            {
                // tfBackground.GetChild(i);
                GameObject inst = Instantiate(_prefWeapon, _tfContainer);
                inst.transform.localPosition = _tfBackground.GetChild(pos).transform.localPosition;
                
                WeaponUI ui = inst.GetComponent<WeaponUI>();
                ui.Init(_ownedWeapons[pos], pos, _tfContainer);

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
    
    
    public bool Edit(int curPos, int newPos, int id){
        if (_ownedWeapons.ContainsKey(newPos) && _ownedWeapons[newPos] > 0)
            return false;

        _ownedWeapons[curPos] = 0;

        if (_ownedWeapons.ContainsKey(newPos))
            _ownedWeapons[newPos] = id;
        else
            _ownedWeapons.Add(newPos, id);

        return true;
    }
}
