using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/InputActions/Use")]
public class Use : InputAction {
    public override void RespondToInput(GameController controller, string[] separatedInputWords) {
        controller.interactableItems.UseItem(separatedInputWords);
    }
}
