using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BagArea : MonoBehaviour
{
    List<GameObject> itemObjectList = new();
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
            itemObjectList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DragAndDrop>())
        {
            itemsList.Remove(collision.gameObject.GetComponent<DragAndDrop>().item);
            itemObjectList.Remove(collision.gameObject);
        }
    }

    public void Empty()
    {
        foreach (GameObject gameObject in itemObjectList.ToList())
        {
            Destroy(gameObject);
        }
        itemObjectList.Clear();
        itemsList.Clear();
    }
}
