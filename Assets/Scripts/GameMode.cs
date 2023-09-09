using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public float gridSize = 1f;

    public Dictionary<Items, int> guildInventory = new();

    public InventoryData inventoryData;

    MainCanvas mainCanvas;

    public DragAndDrop draggedItem;

    public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = FindObjectOfType<MainCanvas>();

        GenerateInventory();

        mainCanvas.GenerateButtonList();

        GenerateQuestText();
        GenerateQuestText();
        GenerateQuestText();
        GenerateQuestText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyInventory(Items key, int value)
    {
        guildInventory[key] += value;
        mainCanvas.UpdateAllButtons();
    }

    void GenerateInventory()
    {
        guildInventory.Clear();

        foreach(ItemRange itemRange in inventoryData.items)
        {
            guildInventory.Add(itemRange.item, Random.Range(itemRange.minimum, itemRange.maximum));
        }
    }

    void GenerateQuestText()
    {
        string dialog = quest.textTemplateList[Random.Range(0, quest.textTemplateList.Length)];
        string intro = quest.introText[Random.Range(0, quest.introText.Length)];
        string goal = quest.goalText[Random.Range(0, quest.goalText.Length)];

        string correctedDialog = dialog;
        correctedDialog = correctedDialog.Replace("{1}",intro);
        correctedDialog = correctedDialog.Replace("{2}", goal);

        Debug.Log(correctedDialog);
    }
}
