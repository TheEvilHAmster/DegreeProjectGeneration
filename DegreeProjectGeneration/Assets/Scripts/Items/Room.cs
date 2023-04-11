using System;
using System.Collections;
using System.Collections.Generic;
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

    private void FindNorthDoor()
    {
        if (transform.Find("doorN") == null) return;
        hasDirection[0] = true;
        DoorS[0] = transform.Find("doorN").GetComponent<Door>();
    }
    private void FindSouthDoor()
    {
        if (transform.Find("doorS") == null) return;
        hasDirection[1] = true;
        DoorS[1] = transform.Find("doorS").GetComponent<Door>();
    }
    private void FindEastDoor()
    {
        if (transform.Find("doorE") == null) return;
        hasDirection[2] = true;
        DoorS[2] = transform.Find("doorE").GetComponent<Door>();
    }
    private void FindWestDoor()
    {
        if (transform.Find("doorW") == null) return;
        hasDirection[3] = true;
        DoorS[3] = transform.Find("doorW").GetComponent<Door>();
    }
    
    void SetTheRooms()
    {
        if (hasDirection[0] == true)
        {
            AmountOfRooms++;
        }
        if (hasDirection[1] == true)
        {
            AmountOfRooms++;
        }
        if (hasDirection[2] == true)
        {
            AmountOfRooms++;
        }
        if (hasDirection[3] == true)
        {
            AmountOfRooms++;
        }
        
    }
    
}
