using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemButton : MonoBehaviour
{
    public Items item;
    MainCanvas mainCanvas;
    public TextMeshProUGUI nameTextMesh;
    public TextMeshProUGUI countTextMesh;
    GameMode gameMode;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        mainCanvas = GetComponentInParent<MainCanvas>();
        gameMode = FindObjectOfType<GameMode>();

        UpdateText();
    }

    public void UpdateText()
    {
        int value = FindItemQuantity();
        nameTextMesh.text = $"{item.itemName}";
        countTextMesh.text = $"{value}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateItem()
    {
        if(FindItemQuantity() > 0)
        {
            mainCanvas.CreateItem(item);
            gameMode.ModifyInventory(item, -1);
        }
        
    }

    int FindItemQuantity()
    {
        return gameMode.guildInventory[item];
    }
}
