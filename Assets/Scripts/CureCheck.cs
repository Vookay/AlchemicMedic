using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureCheck : MonoBehaviour
{
    private GameObject cure = null;
    public string item;
    public void Delete()
    {
        cure = gameObject.transform.Find(item).gameObject;
        Destroy(cure,0.1f);
        cure = null;
    }

}
