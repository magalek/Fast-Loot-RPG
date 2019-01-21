using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    [SerializeField] GameObject itemSpriteSlot;

    public bool isEmpty;

    public Item item;

    public void HandleAddedItem()
    {
        isEmpty = false;
        itemSpriteSlot.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    public void HandleRemovedItem()
    {
        isEmpty = true;
        itemSpriteSlot.GetComponent<SpriteRenderer>().sprite = null;
    }

    public virtual void SlotClick()
    {

    }
}
