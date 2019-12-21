using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ComponentManager {

    [SerializeField]
    private MemoryGenerator memory = null;
    [SerializeField]
    private Transform originTransform = null;

    [SerializeField]
    private Animator telopAnim = null;
    [SerializeField]
    private Animator resultAnim = null;

    [SerializeField]
    private float gameTime = 180f;
    [SerializeField]
    private float insTime = 1.0f;
    [SerializeField]
    private float initHeight = 8.0f;
    [SerializeField]
    private float onceRotValue = 15f;
    [SerializeField]
    private float heightMargin = 3.0f;

    private Quaternion currentRot;
    private Vector3 startCameraPos = Vector3.zero;
    private GameObject currentMemory = null;

    private bool isStarting = false;
    private bool isPlaying = false;
    private bool canExit = false;
    private bool canGenetate = true;

    private float insHeight = 0f;
    private float currentInsTime = 1.0f;   
    private float eulerZ = 0f;
    private float memoryHeight = 0f;

    // Start is called before the first frame update
    void Start() {

        SetMemoryRotation();
        insHeight = initHeight;
        startCameraPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (!isStarting) {

            StartCoroutine(StartManage());
            isStarting = true;
        }

        if (isPlaying) {

            GameUpdate();
        }

        if (canExit) {

            if (Input.GetMouseButtonDown(0)) {

                SceneManager.LoadSceneAsync("TitleScene");
            }
        }
    }

    private void GameUpdate()
    {
        if (currentMemory == null) {

            currentInsTime += Time.deltaTime;

            if (currentInsTime > insTime && canGenetate) {

                adjustInsHeight();

                currentMemory = memory.InsMemories(Vector2.zero, currentRot);
                currentMemory.GetComponent<Rigidbody2D>().simulated = false;
                currentInsTime = 0;

                canGenetate = false;
            }
        }
        else {

            float mouseX = GetMousePos2D().x;
            currentMemory.transform.position = new Vector3(mouseX, insHeight, 0);

            InputUpdate();
        }

        memoryHeight = GetMemoryHeight();
        UpdateTime();

        if (gameTime < 0f) {

            Destroy(currentMemory);
            StartCoroutine(EndManage());
            isPlaying = false;
        }
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

    private float GetMemoryHeight() {

        float originY = originTransform.position.y + 0.5f;
        float height = 0f;
        float rayDistance = insHeight + heightMargin * 2;

        RaycastHit2D result = Physics2D.BoxCast(
            origin: new Vector2(originTransform.position.x, rayDistance),
            size: new Vector2(100f, 0.1f),
            angle: 0f,
            direction: Vector2.down,
            distance: rayDistance,
            layerMask: 1 << LayerMask.NameToLayer("CollidedMemories")
        );

        if (result) {

            height = result.point.y - originY;
            GetUIComponent().setText("Height", height.ToString("f3") + "m");
        }

        return height;
    }

    private void adjustInsHeight() {

        if (memoryHeight > initHeight - heightMargin) {

            insHeight = memoryHeight + heightMargin;
        }
        else {

            insHeight = initHeight;
        }

        Vector3 upDatePos = new Vector3(
            x: startCameraPos.x, 
            y: startCameraPos.y + insHeight - initHeight, 
            z: startCameraPos.z
        );
        Camera.main.transform.position = upDatePos;
    }

    private void UpdateTime() {

        gameTime -= Time.deltaTime;

        int min = (int)(gameTime / 60);
        int sec = (int)(gameTime % 60);

        GetUIComponent().setText("Time", min.ToString("d2") + ":" + sec.ToString("d2"));
    }

    public void DecTime(float value) {

        gameTime = Mathf.Clamp(gameTime -= value, 0, Mathf.Infinity);
    }

    public void Collided() {

        canGenetate = true;
    }

    private IEnumerator StartManage() {

        yield return new WaitForSeconds(0.5f);

        telopAnim.SetTrigger("Start");

        yield return new WaitForSeconds(1.1f);

        isPlaying = true;
    }

    private IEnumerator EndManage()
    {
        SaveRecord();

        telopAnim.SetTrigger("Finish");

        yield return new WaitForSeconds(1.5f);

        resultAnim.SetTrigger("Result");

        yield return new WaitForSeconds(1.0f);

        canExit = true;
    }

    private void SaveRecord() {

        if (PlayerPrefs.HasKey("Record")) {

            if (PlayerPrefs.GetFloat("Record") < memoryHeight) {

                PlayerPrefs.SetFloat("Record", memoryHeight);
                PlayerPrefs.Save();
            }
        }
        else {

            PlayerPrefs.SetFloat("Record", memoryHeight);
            PlayerPrefs.Save();
        }
    }
}
