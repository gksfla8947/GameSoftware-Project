using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    private Item[] items;
    private Item[] slotItems;
    private int itemNum = 3;
    private int slotItemNum = 2;

    private PreWave curPreWave;
    private void Awake()
    {

        {
            if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
            {
                instance = this; //내자신을 instance로 넣어줍니다.
                DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
            }
            else
            {
                if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                    Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
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
            Destroy(UIManager.instance.ItemSelectUI.transform.GetChild(0).GetChild(i).GetChild(0).gameObject);
        }
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