using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public Player player;

    [Header("Character Name")]
    [SerializeField] GameObject _pnlInputName;
    [SerializeField] InputField _ifName;

    [Header("Data")]
    public PlayerInfo playerInfo;
    public DataItem dataItem;

    [Header("Features")]
    public bool isUsingUI;
    public FeatureCursor cursor;
    public FeatureInventory inventory;
    public FeatureTrade trade;


    void Awake() {
        if (!instance)
            instance = this;
    }

    void Start()
    {
        player.enabled = false;
        _pnlInputName.SetActive(true);
    }

    void Update()
    {
        if(_pnlInputName.activeInHierarchy)
            return;

        if (Input.GetKeyUp(KeyCode.I))
            inventory.OpenInventory();
    }

    public void OnClickBtnConfirmName(){
        if(_ifName.text.Length == 0f) {
            Debug.Log("Name must be more than 1 character.");
            return;
        }
        StartGame();
    }

    void StartGame(){
        player.enabled = true;
        _pnlInputName.SetActive(false);
        player.SetName(_ifName.text);
    }
}
