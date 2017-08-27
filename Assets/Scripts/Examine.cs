using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/InputActions/Examine")]
public class Examine : InputAction {
    public override void RespondToInput(GameController controller, string[] separatedInputWords) {
        controller.LogStringWithReturn(controller.TestVerbDictWithIdentifier(controller.interactableItems.examineDict, separatedInputWords[0], separatedInputWords[1]));
    }
}
