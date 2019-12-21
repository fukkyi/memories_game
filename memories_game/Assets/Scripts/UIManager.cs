using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private TextMeshProUGUI[] texts = null;

    void Awake() {

        texts = GetComponentsInChildren<TextMeshProUGUI>();
        setText("MemoryName", string.Empty);
        setText("Time", "00:00");
    }

    public void setText(string uiName, string inputText) {

        if (texts == null) return;

        foreach (TextMeshProUGUI text in texts) {

            if (text.name == uiName) {

                text.text = inputText;
                break;
            }
        }
    }
}
