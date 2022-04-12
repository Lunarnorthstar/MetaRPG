using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClick : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousepos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition).x), (Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
       

        if(Input.GetMouseButtonDown(0))
        {
            gameObject.transform.position = new Vector3(mousepos.x, mousepos.y, gameObject.transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero);
            
            if (hit.collider != null)
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                print("sium");
            }
        }
    }

    
}
