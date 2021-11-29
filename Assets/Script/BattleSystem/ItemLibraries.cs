using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLibraries : MonoBehaviour
{
    public static ItemLibraries instance;
    public readonly string[] item = { "Apple Leaf", "Grape Leaf", "Flower Petal" };

    [SerializeField]
    private GameObject battlescreen;

    [HideInInspector]
    public List<ItemData> items = new List<ItemData>();
    private Transform firstGrid;

    void Start()
    {
        instance = this;
    }

    public void AddItem(ItemData itemToAdd)
        => items.Add(itemToAdd);

    public void RemoveItem(int position)
        => items.RemoveAt(position);

    public void UseItem(int itemid)
    {
        int id = itemid-1;

        if (items[id] == null) { return; }
        if (id <= 0) { id = 0; } 

        SFXController.PlaySFX(SFXData.heal);
        DialogueBox.DoText($"{OptionsSystem.instance.pData.playername} use {items[id].ITEMname}!");
        HealthSystem.health += items[id].HEALamount;

        ItemLibraries.instance.RemoveItem(id);
    }
}
