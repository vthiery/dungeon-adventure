using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
    public InputField inputField;
    GameController controller;

    void Awake() {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput) {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);
        // Process
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
        // Check that the input is well formed
        if (separatedInputWords.Length != 2) {
            controller.LogStringWithReturn("Hmm can't do that... maybe with a verb and a noun?");
        } else {
            foreach (InputAction inputAction in controller.inputActions) {
                if (inputAction.keyword == separatedInputWords[0]) {
                    inputAction.RespondToInput(controller, separatedInputWords);
                }
            }
        }     
        InputComplete();
    }

    void InputComplete() {
        controller.DisplayLogText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
