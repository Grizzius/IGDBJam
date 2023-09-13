using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public DragAndDrop dragAndDropTemplate;
    public itemButton buttonTemplate;
    List<itemButton> itemButtons = new();
    GameMode gameMode;
    public Transform buttonArea;
    public TextMeshProUGUI questText;
    public float textDelay;

    public TextMeshProUGUI tooltipName;
    public TextMeshProUGUI tooltipDescription;

    public TextMeshProUGUI timerText;

    public Transform summary;
    public Transform summaryPanelSlot;
    public SummaryPanel summaryTemplate;
    public TextMeshProUGUI scoreCountText;
    public TextMeshProUGUI dayCountText;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
        DisplaySummary(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDayCount(int value)
    {
        dayCountText.text = $"Day {value}";
    }

    public void UpdateScoreCount(int value)
    {
        scoreCountText.text = $"Score : {value}";
    }

    public void UpdateTimer()
    {
        timerText.text = $"{gameMode.currentTimer} S";
    }

    public void UpdateQuestText(string newText)
    {
        StartCoroutine(TypeText(newText));
    }

    IEnumerator TypeText(string text)
    {
        questText.text = null;
        foreach(char c in text.ToCharArray())
        {
            questText.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }

    public void OnPressSendButton()
    {
        gameMode.SendBag();
    }

    public void GenerateButtonList()
    {
        if(gameMode == null)
        {
            gameMode = FindObjectOfType<GameMode>();
        }

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

    public void ChangeToolTip(string itemName, string itemDescription)
    {
        tooltipName.text = itemName;
        tooltipDescription.text = itemDescription;
    }

    public void DisplaySummary(bool isDisplayed)
    {
        summary.gameObject.SetActive(isDisplayed);
        if (isDisplayed)
        {
            CreateSummaryDisplays();
        }
        
    }

    public void OnContinueButtion()
    {
        gameMode.StartNewDay();
    }

    public void CreateSummaryDisplays()
    {
        for(int i = 0; i < summaryPanelSlot.childCount; i++)
        {
            Destroy(summaryPanelSlot.GetChild(i).gameObject);
        }

        foreach (QuestAndScore questAndScore in gameMode.questAndScores)
        {
            SummaryPanel newPanel = Instantiate(summaryTemplate, summaryPanelSlot);
            bool success = false;
            if(questAndScore.score > 300)
            {
                success = true;
            }
            newPanel.Initialize(success,questAndScore.quest);
        }
    }
}
