using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColliderEnter : MonoBehaviour, IPointerEnterHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        print("Mouse Enter");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
