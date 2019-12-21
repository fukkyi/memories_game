using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ComponentManager {

    [SerializeField]
    private MemoryGenerator memory = null;
    [SerializeField]
    private Transform originTransform = null;

    [SerializeField]
    private float insTime = 1.0f;
    [SerializeField]
    private float insHeight = 8.0f;
    [SerializeField]
    private float onceRotValue = 15f;
    [SerializeField]
    private float memoryHeight = 0f;

    private Quaternion currentRot;
    private GameObject currentMemory = null;

    private float currentInsTime = 0;   
    private float eulerZ = 0f;

    // Start is called before the first frame update
    void Start() {

        SetMemoryRotation();
    }

    // Update is called once per frame
    void Update() {

        if (currentMemory == null) {

            currentInsTime += Time.deltaTime;

            if (currentInsTime > insTime) {

                currentMemory = memory.InsMemories(Vector2.zero, currentRot);
                currentMemory.GetComponent<Rigidbody2D>().simulated = false;
                currentInsTime = 0;
            }
        }
        else {

            float mouseX = GetMousePos2D().x;
            currentMemory.transform.position = new Vector3(mouseX, insHeight, 0);

            InputUpdate();
        }

        memoryHeight = GetMemoryHeight();
    }

    private void InputUpdate() {

        float mouseRotate = Input.GetAxis("Rotate");

        if (mouseRotate != 0) {

            eulerZ += onceRotValue * Mathf.Sign(mouseRotate);
            SetMemoryRotation();
        }

        if (Input.GetButtonDown("Drop")) {

            currentMemory.GetComponent<Rigidbody2D>().simulated = true;
            currentMemory = null;
        }
    }

    private Vector2 GetMousePos2D() {

        Vector3 mousePos = Input.mousePosition;
        Camera camera = Camera.main;

        mousePos.z = camera.transform.position.z;

        return camera.ScreenToWorldPoint(mousePos);
    }

    private void SetMemoryRotation() {

        currentRot = Quaternion.Euler(0, 0, eulerZ);

        if (currentMemory != null) {

            currentMemory.transform.rotation = currentRot;
        }
    }

    private float GetMemoryHeight()
    {
        float originY = originTransform.position.y + 0.5f;
        float height = 0f;

        RaycastHit2D result = Physics2D.BoxCast(
            origin: new Vector2(originTransform.position.x, insHeight),
            size: new Vector2(100f, 0.1f),
            angle: 0f,
            direction: Vector2.down,
            distance: Mathf.Abs(insHeight - originY),
            layerMask: 1 << LayerMask.NameToLayer("CollidedMemories")
        );

        if (result) {

            height = result.point.y - originY;
            GetUIComponent().setText("Height", height.ToString("f3"));
        }

        return height;
    }
}
