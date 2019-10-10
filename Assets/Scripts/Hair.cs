using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hair : MonoBehaviour
{
    public List<Sprite> hair;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void HairChange()
    {
        int randomhair = UnityEngine.Random.Range(0, (hair.Count));
        for (int i = 0; i < hair.Count; i++)
        {
            gameObject.GetComponent<Image>().sprite = hair[randomhair];
        }
    }
    void Update()
    {
        
    }
}
