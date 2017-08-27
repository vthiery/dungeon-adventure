using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {
    public Dictionary<string, string> examineDict = new Dictionary<string, string>();
    public Dictionary<string, string> takeDict = new Dictionary<string, string>();
    public List<InteractableObject> usableItems = new List<InteractableObject>();
    [HideInInspector] public List<string> identifiersInRoom = new List<string>();
    Dictionary<string, ActionResponse> useDict = new Dictionary<string, ActionResponse>(); 
    List<string> identifiersInInventory = new List<string>();
    GameController controller;

    void Awake() {
        controller = GetComponent<GameController>();
    }

    public string GetObjectsNotInInventory(Room currentRoom, InteractableObject interactableObject) {
        if (!identifiersInInventory.Contains(interactableObject.identifier)) {
            identifiersInRoom.Add(interactableObject.identifier);
            return interactableObject.description;
        }
        return null;
    }

    public void AddActionResponsesToUseDict() {
        foreach (string identifier in identifiersInInventory) {
            InteractableObject interactableObject = GetInteractableObjectFromUsableList(identifier);
            if (interactableObject != null) {
                foreach(Interaction interaction in interactableObject.interactions) {
                    if (interaction.actionResponse != null && !useDict.ContainsKey(identifier)) {
                        useDict.Add(identifier, interaction.actionResponse);
                    }
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string identifier) {
        foreach(InteractableObject interactableObject in usableItems) {
            if (interactableObject.identifier == identifier) {
                return interactableObject;
            }
        }
        return null;
    }

    public void DisplayInventory() {
        controller.LogStringWithReturn("You have:");
        foreach (string identifier in identifiersInInventory) {
            controller.LogStringWithReturn(identifier);
        }
    }

    public void ClearCollections() {
        examineDict.Clear();
        takeDict.Clear();
        identifiersInRoom.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords) {
        string identifier = separatedInputWords[1];
        if (identifiersInRoom.Contains(identifier)) {
            identifiersInInventory.Add(identifier);
            AddActionResponsesToUseDict();
            identifiersInRoom.Remove(identifier);
            return takeDict;
        }
        controller.LogStringWithReturn("There is no " + identifier + " here to take!");
        return null;
    }

    public void UseItem(string[] separatedInputWords) {
        string identifier = separatedInputWords[1];
        if (identifiersInInventory.Contains(identifier)) {
            if (useDict.ContainsKey(identifier)) {
                if (!useDict[identifier].DoActionResponse(controller)) {
                    controller.LogStringWithReturn("Hmm... Nothing happens.");
                }
            } else {
            controller.LogStringWithReturn("You can't use the " + identifier);
            }
        } else {
            controller.LogStringWithReturn("There is no " + identifier + " in your inventory.");
        }
    }
}
