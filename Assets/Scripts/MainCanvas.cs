using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public DragAndDrop dragAndDropTemplate;
    public itemButton buttonTemplate;
    List<itemButton> itemButtons = new();
    GameMode gameMode;
    public Transform buttonArea;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateButtonList()
    {
        foreach(Items item in gameMode.guildInventory.Keys)
        {
            itemButton newButton = Instantiate(buttonTemplate, buttonArea);
            newButton.item = item;

            itemButtons.Add(newButton);
            newButton.Initialize();
        }
    }

    public void UpdateAllButtons()
    {
        foreach(itemButton button in itemButtons)
        {
            button.UpdateText();
        }
    }

    public void CreateItem(Items item)
    {
        dragAndDropTemplate.item = item;
        DragAndDrop newItem = Instantiate(dragAndDropTemplate);
        newItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newItem.StartDrag();
    }
}
