using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour
{
    Image btnImage;

    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;

        btnImage = tempBtn.GetComponent<Image>(); // �ش� ������Ʈ�� Image ������Ʈ�� ����
        btnImage.color = Color.blue; // �ش� �̹����� ������ blue�� ����

        Debug.Log(tempBtn);
    }
}

/*    GameObject slotitem0 = GameObject.Find("Slot_0");
    GameObject slotitem1 = GameObject.Find("Slot_1");
    Image slotimg;
    Image itemimg;
*/
/*    public void itemselect()
    {
        GameObject CurButton = EventSystem.current.currentSelectedGameObject;
        Image al = GameObject.Find("Slot_0").GetComponent<Image>();
        itemimg = CurButton.GetComponentInChildren<Image>();
        slotimg = slotitem0.GetComponent<Image>();
        slotimg = itemimg;
        Debug.Log(slotitem0);

    }*/

/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour
{
    Image btnImage;

    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;

        btnImage = tempBtn.GetComponent<Image>(); // �ش� ������Ʈ�� Image ������Ʈ�� ����
        btnImage.color = Color.blue; // �ش� �̹����� ������ blue�� ����

        Debug.Log(tempBtn);
    }

}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour
{
    GameObject slotitem0 = GameObject.Find("Slot_0");
    GameObject slotitem1 = GameObject.Find("Slot_1");
    Image slotimg;
    Image itemimg;

    public void itemselect()
    {
        GameObject CurButton= EventSystem.current.currentSelectedGameObject;
        Image al = GameObject.Find("Slot_0").GetComponent<Image>();
        itemimg=CurButton.GetComponentInChildren<Image>();
        slotimg=slotitem0.GetComponent<Image>();
        slotimg=itemimg;
        Debug.Log(slotitem0);
    }
}
*/
