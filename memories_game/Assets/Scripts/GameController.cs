using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Memories[] memories = null;

    [SerializeField]
    private float insTime = 1.0f;
    private float currentInsTime = 0;

    [SerializeField]
    private Transform insParent = null;
    [SerializeField]
    private float insHeight = 8.0f;
    private Quaternion insRot;

    private GameObject currentMemory = null;

    // Start is called before the first frame update
    void Start()
    {
        memories = LoadMemories();
        insRot = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {        
        if (currentMemory == null) {

            currentInsTime += Time.deltaTime;

            if (currentInsTime > insTime) {

                currentMemory = InsMemories(Vector2.zero, insRot, insParent);
                currentMemory.GetComponent<Rigidbody2D>().simulated = false;
                currentInsTime = 0;
            }
        }
        else {

            float mouseX = GetMousePos2D().x;
            currentMemory.transform.position = new Vector3(mouseX, insHeight, 0);

            float mouseRotate = Input.GetAxis("Rotate");

            if (Input.GetButtonDown("Drop")) {

                currentMemory.GetComponent<Rigidbody2D>().simulated = true;
                currentMemory = null;
            }
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

    private Vector2 GetMousePos2D()
    {
        Vector3 mousePos = Input.mousePosition;
        Camera camera = Camera.main;

        mousePos.z = camera.transform.position.z;

        return camera.ScreenToWorldPoint(mousePos);
    }
}
