using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    private Item[] items;
    private Item[] slotItems;
    public Item[] SlotItems
    {
        get { return slotItems; }
        set { slotItems = value; }
    }
    private int itemNum = 3;
    private int slotItemNum = 2;

    private PreWave curPreWave;
    private void Awake()
    {

        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this) 
                    Destroy(this.gameObject); 
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        curPreWave = null;
        items = new Item[itemNum];
        slotItems = new Item[slotItemNum];
    }

    public void GetPreWave()
    {
        if (!GameManager.instance.currentWave.isBattleWave)
        {
            curPreWave = (PreWave) GameManager.instance.currentWave;
        }
    }
    public void ActiveItem(int number)
    {
        if(items[number].isActiveItem)
        {
            EnrollActiveItems(items[number]);
            Debug.Log("active call");
        }
        else
        {
            items[number].ApplyItem();
            Debug.Log("passiave call");

        }
    }

    public void ActiveSlotItem(int number)
    {
        slotItems[number].ApplyItem();
        Debug.Log("Hi");
    }

    public void SelectItems()
    {
        List<int> itemNums = new List<int>();

        int i = 0;
        while(i < items.Length)
        {
            int rand = Random.Range(0, curPreWave.items.Length);
            if (!itemNums.Contains(rand))
            {
                itemNums.Add(rand);
                items[i] = curPreWave.items[rand];
                i++;
            }
        }
    }



    public void ClearItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(UIManager.instance.ItemSelectUI.transform.GetChild(0).GetChild(i).GetChild(1).gameObject);
        }
    }
    public void ClearActiveItem(int number)
    {
        Destroy(UIManager.instance.activeItemsUI.transform.GetChild(number).GetChild(0).gameObject);
    }

    public void InstantiateItems()
    {
        for(int i = 0; i < items.Length; i++)
        {
            Transform createPoint = UIManager.instance.ItemSelectUI.transform.GetChild(0).GetChild(i);
            Item item = Instantiate(items[i], createPoint);
            item.gameObject.SetActive(true);
            item.gameObject.transform.SetParent(createPoint);
            items[i] = item;

            TextMeshProUGUI itemDescription = createPoint.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            itemDescription.text = item.description;
        }
    }

    public void EnrollActiveItems(Item item)
    {
        for (int i = 0; i < slotItemNum; i++)
        {
            if (UIManager.instance.activeItemsUI.transform.GetChild(i).childCount == 0)
            {
                Transform createPoint = UIManager.instance.activeItemsUI.transform.GetChild(i);
                Item activeItem = Instantiate(item, createPoint);
                activeItem.gameObject.SetActive(true);
                activeItem.gameObject.transform.SetParent(createPoint);
                slotItems[i] = activeItem;
                break;
            }
        }
    }
}