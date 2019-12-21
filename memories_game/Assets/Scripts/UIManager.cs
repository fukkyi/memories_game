using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private TextMeshProUGUI[] texts = null;

    void Awake() {

        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void setText(string uiName, string inputText) {

        foreach(TextMeshProUGUI text in texts) {

            if (text.name == uiName) {

                text.text = inputText;
                break;
            }
        }
    }
}
