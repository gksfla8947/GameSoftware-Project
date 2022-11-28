using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour
{
    Image btnImage;

    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;

        btnImage = tempBtn.GetComponent<Image>(); // 해당 오브젝트의 Image 컴포넌트를 받음
        btnImage.color = Color.blue; // 해당 이미지의 색상을 blue로 변경

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

        btnImage = tempBtn.GetComponent<Image>(); // 해당 오브젝트의 Image 컴포넌트를 받음
        btnImage.color = Color.blue; // 해당 이미지의 색상을 blue로 변경

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
