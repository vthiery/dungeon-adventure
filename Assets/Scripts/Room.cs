using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/Rooms")]
public class Room : ScriptableObject {
    [TextArea] public string description;
    public string identifier;
    public Exit[] exits;
    public InteractableObject[] interactableObjects;
}
