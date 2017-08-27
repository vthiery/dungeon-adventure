using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {
    public Room changeTo;
    public override bool DoActionResponse(GameController controller) {
        if (controller.roomNavigation.currentRoom.identifier == requiredKey) {
            controller.roomNavigation.currentRoom = changeTo;
            controller.DisplayRoomText();
            return true;
        }
        return false;
    }
}
