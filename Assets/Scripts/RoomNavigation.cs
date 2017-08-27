using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {
    public Room currentRoom;
    Dictionary<string, Room> exitDict = new Dictionary<string, Room>();
    GameController controller;

    void Awake() {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom() {
        foreach (Exit exit in currentRoom.exits) {
            exitDict.Add(exit.direction, exit.valueRoom);
            controller.interactionDescriptionsInRoom.Add(exit.description);
        }
    }

    public void AttemptToChangeRooms(string direction) {
        if (exitDict.ContainsKey(direction)) {
            currentRoom = exitDict[direction];
            controller.LogStringWithReturn("You head off to the " + direction);
            controller.DisplayRoomText();
        } else {
            controller.LogStringWithReturn("There is no path to the " + direction);
        }
    }

    public void ClearExits() {
        exitDict.Clear();
    }
}
