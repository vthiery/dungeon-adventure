﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit {
    public string direction;
    [TextArea] public string description;
    public Room valueRoom;
}
