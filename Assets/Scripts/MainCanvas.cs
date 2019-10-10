using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public RectTransform dragIcon;

    public Transform content;
    public DragObject dragPrefab;
    
    private DragObject currentDrag;
    private DragObject obj;
    public CureCheck cureCheck;

    public AudioSource drop, creation1, creation2;

    public void OnBeginDrag(DragObject target)
    {
        currentDrag = target;

        Image icon = dragIcon.GetComponent<Image>();
        icon.sprite = currentDrag.picture.sprite;

        dragIcon.gameObject.SetActive(true);
    }
    public void OnEndDrag()
    {
        currentDrag = null;
        dragIcon.gameObject.SetActive(false);
    }


    public void OnDrag()
    {
        dragIcon.position = Input.mousePosition;
    }

    public void OnDrop(DropObject target)
    {
        target.itemName = currentDrag.itemName;
        target.sprite = currentDrag.picture.sprite;
        GameManager.instance.AddItem(currentDrag.itemName);
        if (currentDrag.tag == "Cure")
        {
            currentDrag.tag = "CureInUse";
        }
        drop.Play();
    }

    public void OnDrop(Human target)
    {
        target.itemName = currentDrag.itemName;
        //GameManager.instance.AddItem(currentDrag.itemName);
        cureCheck.item = obj.name;
        cureCheck.Delete();
    }


    public void OnDrop(Trash target)
    {
        target.itemName = currentDrag.itemName;
        cureCheck.item = obj.name;
        cureCheck.Delete();
        drop.Play();
    }

    public void OnOKButton()
    {
        int random = UnityEngine.Random.Range(1, 3);
        if (random == 2)
        {
            creation1.Play();
        }
        else if (random == 1)
        {
            creation2.Play();
        }
        GameManager.instance.buttonOK.SetActive(false);
        GameObject curePanel = GameObject.Find("CurePanel");
        if (curePanel.transform.childCount < 4)
        {
            obj = Instantiate(dragPrefab, content);
            obj.picture.sprite = GameManager.instance.resultObject.picture.sprite;
            obj.itemName = GameManager.instance.resultObject.itemName;
            obj.name = obj.itemName;
            obj.tag = "Cure";
        }

        GameObject[] cureList = GameObject.FindGameObjectsWithTag("CureInUse");
        foreach (GameObject cure in cureList)
        {
            Destroy(cure);
        }
    
  

        GameManager.instance.ClearRecipe();

    }
}


