using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Adventure/InteractableObject")]
public class InteractableObject : ScriptableObject {
    public string identifier;
    [TextArea] public string description;
    public Interaction[] interactions;
}
