using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour
{
    public string itemName;
    public Image picture;
    private MainCanvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<MainCanvas>();
    }

    public void OnBeginDrag(BaseEventData bases)
    {
        canvas.OnBeginDrag(this);
    }
    public void OnEndDrag()
    {
        canvas.OnEndDrag();
    }
    public void OnDrag()
    {
        canvas.OnDrag();
    }

}
