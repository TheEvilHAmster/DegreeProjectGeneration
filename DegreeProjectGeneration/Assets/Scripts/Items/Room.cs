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

    private void NotAwake()
    {
        FindNSEWDoor();
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

    private void FindNSEWDoor()
    {
        bool N = true, S = true, E = true, W = true;
        
        if (transform.Find("doorN") == null) N = false;
        if (N)
        {
             hasDirection[0] = true;
             DoorS[0] = transform.Find("doorN").GetComponent<Door>();
        }

        if (transform.Find("doorS") == null) S=false;
        if (S)
        {
            hasDirection[1] = true;
            DoorS[1] = transform.Find("doorS").GetComponent<Door>();
        }

        
        if (transform.Find("doorE") == null) E = false;
        if (E)
        {
            hasDirection[2] = true;
            DoorS[2] = transform.Find("doorE").GetComponent<Door>();
        }
        
        if (transform.Find("doorW") == null) W = false;
        if (W)
        {
            hasDirection[3] = true;
            DoorS[3] = transform.Find("doorW").GetComponent<Door>();
        }
        
        
        
    }
    private void FindDoors()
    {
        Door[] doors;
        doors = transform.GetComponentsInChildren<Door>();
        foreach (var door in doors)
        {
            DoorS[(int)door.direction] = door;
            hasDirection[(int)door.direction] = true;
        }
    }
    
    public void SetTheRooms()
    {
        FindNSEWDoor();

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
