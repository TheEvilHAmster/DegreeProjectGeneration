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
        FindNorthDoor();
        FindSouthDoor();
        FindEastDoor();
        FindWestDoor();
        SetTheRooms(); 
    }

    private void NotAwake()
    {
        FindNorthDoor();
        FindSouthDoor();
        FindEastDoor();
        FindWestDoor();
    }


    private bool FindNorthDoor()
    {
        if (transform.Find("doorN") == null) return false;
        hasDirection[0] = true;
        DoorS[0] = transform.Find("doorN").GetComponent<Door>();
        return true;
    }
    private bool FindSouthDoor()
    {
        if (transform.Find("doorS") == null) return false;
        hasDirection[1] = true;
        DoorS[1] = transform.Find("doorS").GetComponent<Door>();
        return true;
    }
    private bool FindEastDoor()
    {
        if (transform.Find("doorE") == null) return false;
        hasDirection[2] = true;
        DoorS[2] = transform.Find("doorE").GetComponent<Door>();
        return true;
    }
    private bool FindWestDoor()
    {
        if (transform.Find("doorW") == null) return false;
        hasDirection[3] = true;
        DoorS[3] = transform.Find("doorW").GetComponent<Door>();
        return true;
    }
    
    public void SetTheRooms()
    {
        NotAwake();
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
