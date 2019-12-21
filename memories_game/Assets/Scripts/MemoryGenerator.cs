using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGenerator : ComponentManager {

    [SerializeField]
    private MemoryObject generateContent = null;

    private Memories[] memories = null;

    void Start () {

        memories = LoadMemories();
    }

    private Memories[] LoadMemories() {

        Object[] loadObj = Resources.LoadAll("Memories/", typeof(Memories));

        Memories[] contents = new Memories[loadObj.Length];

        for (int i = 0; i < loadObj.Length; i++) {
            contents[i] = loadObj[i] as Memories;
        }

        return contents;
    }

    public GameObject InsMemories(Vector2 pos, Quaternion rot) {

        int memoriesLength = memories.Length;
        int number = Random.Range(0, memoriesLength);

        GameObject generateObj = Instantiate(generateContent.gameObject, pos, rot, transform) as GameObject;
        generateObj.GetComponent<MemoryObject>().InitObj(memories[number].name, memories[number].memorySprite);
        GetUIComponent().setText("MemoryName", memories[number].memoryName);

        return generateObj;
    }
}
