using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text displayText;
    public InputAction[] inputActions;
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    [HideInInspector] public InteractableItems interactableItems;
    List<string> actionLog = new List<string>();

	void Awake () {
		roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
	}

    void Start() {
        DisplayRoomText();
        DisplayLogText();
    }

    public void DisplayLogText() {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void DisplayRoomText() {
        ClearCollectionsForNewRoom();
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combineText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combineText);
    }

    void UnpackRoom() {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom) {
        foreach (InteractableObject interactableObject in currentRoom.interactableObjects) {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, interactableObject);
            if (descriptionNotInInventory != null) {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory);
            }
            foreach (Interaction interaction in interactableObject.interactions) {
                if (interaction.inputAction.keyword == "examine") {
                    interactableItems.examineDict.Add(interactableObject.identifier, interaction.textResponse);
                } else if (interaction.inputAction.keyword == "take") {
                    interactableItems.takeDict.Add(interactableObject.identifier, interaction.textResponse);
                }
            }
        }
    }

    public string TestVerbDictWithIdentifier(Dictionary<string, string> verbDict, string verb, string identifier) {
        if (verbDict.ContainsKey(identifier)) {
            return verbDict[identifier];
        }
        return "You can't " + verb + " " + identifier;
    }

    void ClearCollectionsForNewRoom() {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
        interactableItems.ClearCollections();
    }

    public void LogStringWithReturn(string stringToAdd) {
        actionLog.Add(stringToAdd + "\n");
    }
}
