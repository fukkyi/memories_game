using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Memories", fileName = "Memories")]
public class Memories : ScriptableObject
{
    public GameObject gameObj;

    public string memoryName;

    [TextArea]
    public string description;
}
