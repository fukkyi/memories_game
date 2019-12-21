using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Memories", fileName = "Memories")]
public class Memories : ScriptableObject
{
    public Sprite memorySprite;

    public string memoryName;

    [TextArea]
    public string description;
}
