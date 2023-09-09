using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagArea : MonoBehaviour
{
    public List<Items> itemsList = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<DragAndDrop>())
        {
            itemsList.Add(collision.gameObject.GetComponent<DragAndDrop>().item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DragAndDrop>())
        {
            itemsList.Remove(collision.gameObject.GetComponent<DragAndDrop>().item);
        }
    }
}
