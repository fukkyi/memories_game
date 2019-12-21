using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour {

    [SerializeField]
    private Animator startAnim = null;
    [SerializeField]
    private TextMeshProUGUI recordText = null;

    private bool isStart = false;

    void Start()
    {
        recordText.text = PlayerPrefs.GetFloat("Record").ToString("f3") + "m";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStart) {

            StartCoroutine(start());
        }
    }

    private IEnumerator start()
    {
        isStart = true;
        startAnim.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadSceneAsync("GameScene");
    }
}
