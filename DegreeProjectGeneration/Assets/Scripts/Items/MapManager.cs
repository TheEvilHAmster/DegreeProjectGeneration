using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [SerializeField] public Room[] Rooms;


    [Header("Default")] [SerializeField] private int amountOfRooms = 21;
    [SerializeField] private bool isRandom = true;
    
    [Header("Rooms")] 
    [SerializeField] int minRoom = 4, 
        maxRoom = 8;
    public Room[,] MapLayout;

    [Header("Room Distances")]
    //[SerializeField]private float columnSpace = 1;
    //[SerializeField] private float rowSpace = 1;
    [SerializeField] private float xSpace = 0, ySpace = 0;
    [SerializeField] private float xStart = 0, yStart = 0;

    private void Start()
    {

        
        WaitForTime(1);
        FillRoomLists();
        WaitForTime(1);
        CheckRoomList();
        RandomAmountOfRoom(minRoom, maxRoom);
        MapLayout = new Room[amountOfRooms, amountOfRooms];
        GenerateRoomLayout(amountOfRooms / 2,amountOfRooms / 2);
        PutGenerationOnMap();
        
        
    }

    IEnumerable<WaitForSeconds> WaitForTime(float T)
    {
        yield return new WaitForSeconds(T);
        //yield return null;

    }
    private void CheckRoomList()
    {
        int i = 0;
        foreach (var room in Rooms)
        {
            i++;
            room.SetTheRooms();
            if (room.AmountOfRooms <= 0)
            {
                Debug.LogError("Room nr " + i + "didnt load correctly");
                continue;
            }
            Debug.Log(i+": loaded correctly");
            
        }
    }

    private void PutGenerationOnMap()
    {
        for (int i = 0; i < amountOfRooms; i++)
        {
            for (int x = 0; x < amountOfRooms; x++)
            {
                if (MapLayout[x,i] != null )
                {
                     Instantiate(MapLayout[x, i], new Vector3((xStart + (xSpace * x)),
                         (yStart+(ySpace * i))), quaternion.identity);
                     Debug.Log("x:"+ x+ " y: "+ i+ " Object: "+ MapLayout[x,i]+ "" );
                }

            }
        }
    }

    private void FillRoomLists()
    {
        Rooms = Resources.LoadAll<Room>("Rooms");
    }


    private void RandomAmountOfRoom(int minRoom, int maxRoom)
    {
        if (isRandom)
        {
            amountOfRooms = (int)Random.Range(minRoom, maxRoom);
        }
        
    }

    private int ConnectDoors(Door first, Door second)
    {
        if (first.isPaired != false || second.isPaired != false || first.otherDoor != null ||
            second.otherDoor != null) return 0;
        
        first.otherDoor = second;
        second.otherDoor = first;
        first.isPaired = true;
        second.isPaired = true;

        return 1;

    }

    Room GetRoomWithDirection(int x, int y, Direction direction)
    {
        //todo
        //check the Random rooms direction to be correct
        
        bool succsessnt = true;
        Room[] TempRooms = new Room[8];
        int i = 0;

        foreach (var room in Rooms)
        {
            if (room.hasDirection[(int)direction])
            {
                TempRooms[i] = room;
                i++;
            }
        }
        
        while (succsessnt)
        {

            if (direction is Direction.East or Direction.West)
            {
                 return Rooms[Random.Range(0,8)];
            }
            
            return Rooms[Random.Range(0, 9)];
            
           //Clean this 
            //Need to look at this if as its supposed to look if there is a room where its not supposed to be a room.
            if (MapLayout[x,y+1] != null && direction != Direction.North || MapLayout[x,y-1] != null && direction != Direction.South ||
                MapLayout[x+1,y] != null && direction != Direction.East || MapLayout[x-1,y] != null && direction != Direction.West)
            {
                continue;
            }

        }

        return GetRoomWithDirection(x,y,direction);
    }
    
    private void GenerateRoomLayout(int x, int y)
    {
        int amoutOfUnconectedDoors = 0;
        int LoopTimes = 0;
        bool KeepGenerating = true;
        Room tempRoom;
        
        //TODO Generation 
        // place first room and check how manny doors there are and what direction they are inv
        // generate random rooms in the diffrent direction and be shure that the new room has a door in the direction oposite the direction
        // add check to see how manny generated doors there are and if they are filled or not
        // move to the next room and repete Check if room is already filled and dont generate new room. 
        //Recursion of this instead

        if (OutsideArray(y + 1))
        {
            Debug.LogError("NUOB");
            return;
        }
        if (OutsideArray(y - 1))
        {
            Debug.LogError("SUOB");
            return;
        }
        if (OutsideArray(x + 1))
        {
            Debug.LogError("EUOB");
            return;
        }
        if (OutsideArray(x - 1))
        {
            Debug.LogError("WUOB");
            return;
        }
        
        
        
        //while (KeepGenerating)
        {
            LoopTimes++;
            if (MapLayout[x, y]==null)
            {
                 MapLayout[x, y] = Rooms[Random.Range(0,15)];
                 amoutOfUnconectedDoors += MapLayout[x, y].AmountOfRooms;
            }
           
            
            // found out what doors are there

            if (MapLayout[x,y].hasDirection[(int)Direction.North])
            {
                //y += 1;
                if (MapLayout[x,y+1] == null)
                {
                    tempRoom = Rooms[Random.Range(0,15)];
                    if (tempRoom.hasDirection[1])
                    {
                        MapLayout[x, y + 1] = GetRoomWithDirection(x, y + 1, Direction.South);
                        amoutOfUnconectedDoors += MapLayout[x, y + 1].AmountOfRooms;
                        amountOfRooms -= ConnectDoors(MapLayout[x,y].DoorS[(int)Direction.North], MapLayout[x,y + 1].DoorS[(int)Direction.South]);
                        GenerateRoomLayout(x,y + 1);
                        
                    }
                }
            }
            if (MapLayout[x,y].hasDirection[(int)Direction.South] )
            {
                //y += -1;
                if (MapLayout[x,y-1] == null)
                {
                    tempRoom = Rooms[Random.Range(0,15)];
                    if (tempRoom.hasDirection[0])
                    {
                        MapLayout[x, y-1] = GetRoomWithDirection(x, y-1, Direction.North);
                        amoutOfUnconectedDoors += MapLayout[x, y - 1].AmountOfRooms;
                        amountOfRooms -= ConnectDoors(MapLayout[x,y].DoorS[(int)Direction.South], MapLayout[x,y - 1].DoorS[(int)Direction.North]);
                        GenerateRoomLayout(x,y-1);
                    }

                }
            }
            if (MapLayout[x,y].hasDirection[(int)Direction.East])
            {
                //x += 1;
                if (MapLayout[x+1,y] == null)
                {
                    tempRoom = Rooms[Random.Range(0,15)];
                    if (tempRoom.hasDirection[3])
                    {
                        MapLayout[x+1, y] = GetRoomWithDirection(x+1,y, Direction.West);
                        amoutOfUnconectedDoors += MapLayout[x+1, y].AmountOfRooms;
                        amountOfRooms -= ConnectDoors(MapLayout[x,y].DoorS[(int)Direction.East], MapLayout[x+1,y].DoorS[(int)Direction.West]);
                        GenerateRoomLayout(x+1,y);
                    }

                }
            }
            if (MapLayout[x,y].hasDirection[(int)Direction.West])
            {
               //x += -1;
               if (MapLayout[x-1,y] == null)
               {
                   MapLayout[x-1,y] = GetRoomWithDirection(x-1, y, Direction.East);
                   amoutOfUnconectedDoors += MapLayout[x - 1, y].AmountOfRooms;
                   amountOfRooms -= ConnectDoors(MapLayout[x,y].DoorS[(int)Direction.West], MapLayout[x,y + 1].DoorS[(int)Direction.East]);
                   GenerateRoomLayout(x-1,y);
               }
            }


            if (amoutOfUnconectedDoors < 0)
            {
                Debug.LogError("This isnt supposed to happen, Exit generation loop");
            }
            if (amoutOfUnconectedDoors == 0)
            {
                KeepGenerating = false;   
            }
        }
        
        /* make the shaped map*/
        
        // MapLayout[startingPoint, startingPoint] = Rooms[7];
        // MapLayout[startingPoint+1, startingPoint] = Rooms[14];
        // MapLayout[startingPoint, startingPoint+1] = Rooms[10];
        // MapLayout[startingPoint-1, startingPoint] = Rooms[0];
        // MapLayout[startingPoint, startingPoint-1] = Rooms[2];
        
        
        
    }

    bool OutsideArray(int goingTo)
    {
        if (goingTo < 0 || goingTo > amountOfRooms)
        {
            return true;
        }

        return false;
    }


}