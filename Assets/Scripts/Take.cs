using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/InputActions/Take")]
public class Take : InputAction {
    public override void RespondToInput(GameController controller, string[] separatedInputWords) {
        Dictionary<string, string> takeDict = controller.interactableItems.Take(separatedInputWords);
        if (takeDict != null) {
            controller.LogStringWithReturn(controller.TestVerbDictWithIdentifier(takeDict, separatedInputWords[0], separatedInputWords[1]));
        }
    }
}
