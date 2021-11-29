using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    [SerializeField]
    private float healAmount;
    [SerializeField]
    private string itemName; 

    private ItemData itemData;

    void Start()
        => itemData = new ItemData(healAmount, itemName);

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ovPlayer"))
        {
            SFXController.PlaySFX(SFXData.itempickup);
            ItemLibraries.instance.AddItem(itemData);
            ScoreCounter.itemget++;
            Destroy(this.gameObject);
        }
    }
}
