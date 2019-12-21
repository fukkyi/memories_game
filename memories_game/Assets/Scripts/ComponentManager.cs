using UnityEngine;

public class ComponentManager : MonoBehaviour {

    protected UIManager GetUIComponent(){

        return GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    }

    protected GameController GetGameComponent() {

        return GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

}
