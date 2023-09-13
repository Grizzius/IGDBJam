using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public float gridSize = 1f;

    public Dictionary<Items, int> guildInventory = new();

    public InventoryData inventoryData;

    MainCanvas mainCanvas;

    public DragAndDrop draggedItem;

    public Quest[] quests;

    public Quest CurrentQuest;

    public BagArea bag;

    public Camera dioramaCam;
    public Camera dragAndDropCam;

    public Transform inventoryUI;

    public Adventurer AdventurerTemplate;
    public Transform tavernDoor;
    public Transform bar;
    Adventurer currentAdventurer;

    public int defaultTimer;
    public int currentTimer;
    public int questSuccessThreshold;
    int dayCount = 0;
    int score;

    public List<QuestAndScore> questAndScores = new();

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = FindObjectOfType<MainCanvas>();
        bag = FindObjectOfType<BagArea>();

        GenerateInventory();

        mainCanvas.GenerateButtonList();
        mainCanvas.UpdateScoreCount(score);
        StartNewDay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewDay()
    {
        dayCount++;
        questAndScores.Clear();
        GenerateInventory();

        mainCanvas.UpdateDayCount(dayCount);
        mainCanvas.DisplaySummary(false);
        ToggleInventory(false);
        StartCoroutine(Timer(defaultTimer));
        StartCoroutine(StartNewQuest());
    }

    IEnumerator Timer(int time)
    {
        currentTimer = time;
        mainCanvas.UpdateTimer();

        while (currentTimer > 0)
        {
            yield return new WaitForSeconds(1);
            currentTimer--;
            mainCanvas.UpdateTimer();
        }
        if(currentTimer <= 0)
        {
            if(currentAdventurer != null)
            {
                SendBag();
                currentAdventurer.Leave(tavernDoor);
            }
            else
            {
                FinishDay();
            }
        }
    }

    void FinishDay()
    {
        mainCanvas.DisplaySummary(true);

        foreach(QuestAndScore qands in questAndScores)
        {
            if(qands.score > questSuccessThreshold)
            {
                score += 100;
            }
        }

        mainCanvas.UpdateScoreCount(score);
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

        mainCanvas.UpdateAllButtons();
    }

    void GenerateQuest()
    {
        Quest newQuest = quests[Random.Range(0, quests.Length)];
        while (newQuest == CurrentQuest)
        {
            newQuest = quests[Random.Range(0, quests.Length)];
        }

        CurrentQuest = newQuest;
        string questText = GenerateQuestText(newQuest);

        Debug.Log(questText);
        mainCanvas.UpdateQuestText(questText);
    }

    string GenerateQuestText(Quest quest)
    {
        string dialog = quest.textTemplateList[Random.Range(0, quest.textTemplateList.Length)];
        string intro = quest.introText[Random.Range(0, quest.introText.Length)];
        string goal = quest.goalText[Random.Range(0, quest.goalText.Length)];

        string correctedDialog = dialog;
        correctedDialog = correctedDialog.Replace("{1}",intro);
        correctedDialog = correctedDialog.Replace("{2}", goal);

        return(correctedDialog);
    }

    public void SendBag()
    {
        QuestAndScore newScore = new()
        {
            quest = CurrentQuest,
            score = EvaluateQuestSuccess()
        };

        questAndScores.Add(newScore);

        bag.Empty();
        ToggleInventory(false);
        currentAdventurer.Leave(tavernDoor);
    }

    IEnumerator StartNewQuest()
    {
        yield return new WaitForSeconds(1.2f);
        
        CreateAdventurer();
        currentAdventurer.GoToBar(bar);
    }

    public void AdventurerReachBar()
    {
        GenerateQuest();
        ToggleInventory(true);
    }

    public void AdventurerLeavesTavern()
    {
        Destroy(currentAdventurer.gameObject);
        if(currentTimer > 3)
        {
            StartCoroutine(StartNewQuest());
        }
        else if(currentTimer <= 0)
        {
            FinishDay();
        }
        
    }

    void CreateAdventurer()
    {
        currentAdventurer = Instantiate(AdventurerTemplate, tavernDoor.position, Quaternion.identity);
        currentAdventurer.gameMode = this;
    }

    int EvaluateQuestSuccess()
    {
        float score = 0;

        List<QuestTag> tagList = new();

        foreach(Items item in bag.itemsList)
        {
            foreach(QuestTag tag in item.tags)
            {
                tagList.Add(tag);
            }
        }

        foreach (QuestTag tag in tagList)
        {
            if (CurrentQuest.questTags.Contains(tag))
            {
                score += 100;
            }
        }

        return Mathf.RoundToInt(score);
    }

    void ToggleInventory(bool isOpen)
    {
        dioramaCam.gameObject.SetActive(!isOpen);
        dragAndDropCam.gameObject.SetActive(isOpen);
        inventoryUI.gameObject.SetActive(isOpen);
    }
}

public struct QuestAndScore
{
    public Quest quest;
    public int score;
}
