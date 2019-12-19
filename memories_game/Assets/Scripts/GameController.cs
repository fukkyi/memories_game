using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Memories[] memories = null;

    [SerializeField]
    private float insTime = 2.0f;
    private float currentInsTime = 0;

    [SerializeField]
    private Transform insParent = null;
    private Vector2 insPos;
    private Quaternion insRot;

    // Start is called before the first frame update
    void Start()
    {
        memories = LoadMemories();
        insPos = new Vector2(0, 9);
        insRot = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        currentInsTime += Time.deltaTime;

        if (currentInsTime > insTime) {

            InsMemories(insPos, insRot, insParent);
            currentInsTime = 0;
        }
    }

    private Memories[] LoadMemories()
    {
        Object[] loadObj = Resources.LoadAll("Memories/", typeof(Memories));

        Memories[] contents = new Memories[loadObj.Length];

        for(int i = 0; i < loadObj.Length; i++) {

            contents[i] = loadObj[i] as Memories;
        }

        return contents;
    }

    private GameObject InsMemories(Vector2 pos, Quaternion rot, Transform parent)
    {
        int memoriesLength = memories.Length - 1;

        int number = Random.Range(0, memoriesLength);

        return Instantiate(memories[number].gameObj, pos, rot, parent) as GameObject;
    }

    private GameObject InsMemories(Vector2 pos, Quaternion rot, Transform parent, int number)
    {
        int memoriesLength = memories.Length - 1;

        number = Mathf.Clamp(number, 0, memoriesLength);

        return Instantiate(memories[number].gameObj, pos, rot, parent) as GameObject;
    }
}
