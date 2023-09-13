using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryPanel : MonoBehaviour
{
    public TextMeshProUGUI adventurer;
    public TextMeshProUGUI adventure;
    public TextMeshProUGUI conclusion;

    public Color failColor;
    public Color successColor;

    public void Initialize(bool successful, Quest quest)
    {
        adventure.text = quest.summary;

        if (successful)
        {
            conclusion.text = "Quest successful !";
            conclusion.color = successColor;
        }
        else
        {
            conclusion.text = "Quest failed !";
            conclusion.color = failColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        adventurer.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
