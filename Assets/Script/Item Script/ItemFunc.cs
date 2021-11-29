using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    private float healAmount;
    private string itemName;

    public float HEALamount { get { return healAmount; } }
    public string ITEMname { get { return itemName; } }

    public ItemData(float heal, string item)
    {
        healAmount = heal;
        itemName = item;
    }    
}