using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new quest", menuName = "Data/Quest")]
public class Quest : ScriptableObject
{
    
    [TextArea]
    public string[] textTemplateList;

    [Header("replaces {1} in the template")]
    [TextArea]
    public string[] introText;
    [Header("replaces {2} in the template")]
    [TextArea]
    public string[] goalText;

    public QuestTag[] questTags;
}

public enum QuestTag
{
    Undeads,
    Bandits,
    Climbing,
    DarkLocations,
    LightDanger,
    MediumDanger,
    HeavyDanger,
    Cold,
    WeakToFire
}
