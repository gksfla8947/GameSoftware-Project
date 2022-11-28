using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invetory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 3; //slot °³¼ö
    public GameObject slotPrefab;

    private void Start()
    {
        GameObject slotPanel = GameObject.Find("Itempanel");

        for (int i = 0; i < maxSlot; i++) 
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name="Slot_"+ i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }
}
