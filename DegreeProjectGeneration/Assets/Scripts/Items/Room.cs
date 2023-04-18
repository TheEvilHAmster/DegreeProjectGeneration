using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool[] hasDirection = new bool[4]{false, false, false, false}; //N S E W
    public Door[] DoorS = new Door[4];
    public int AmountOfRooms = 0;
    
    
    private void Awake()
    {
        
        SetTheRooms(); 
    }


    bool FindDoor(string name, Direction dir)
    {
        if (transform.Find(name) == null) return false;

        hasDirection[(int)dir] = true;
        DoorS[(int)dir] = transform.Find(name).GetComponent<Door>();
        return true;
    }

    
    public void SetTheRooms()
    {
        FindDoor("doorN", Direction.North);
        FindDoor("doorS", Direction.South);
        FindDoor("doorE", Direction.East);
        FindDoor("doorW", Direction.West);

        if (hasDirection[0])
        {
            AmountOfRooms++;
        }
        if (hasDirection[1])
        {
            AmountOfRooms++;
        }
        if (hasDirection[2])
        {
            AmountOfRooms++;
        }
        if (hasDirection[3])
        {
            AmountOfRooms++;
        }
        
    }
    
}
