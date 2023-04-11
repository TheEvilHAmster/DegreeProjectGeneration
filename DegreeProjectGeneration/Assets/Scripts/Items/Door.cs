using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North = 0,
    South = 1,
    East = 2,
    West = 3,
    Exit = 4,
    non = -1
}
public class Door : MonoBehaviour
{

    [SerializeField] public Door otherDoor;
    [SerializeField] public bool isPaired = false;
    [SerializeField] public bool isOpen = true;
    public Direction direction = Direction.non; // 1 = N, S = 2, E = 3, W = 4
    [SerializeField] private bool MapExit = false;

    

    protected void PairDoor(Door door)
    {
        
        if (door == null)
        {
            return;
        }
        
        isPaired = true;
        otherDoor = door;
        if (!door.isPaired)
        {
            door.isPaired = true;
            door.otherDoor = this;
        }
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen && otherDoor == null)
        {
            return;
        }
        
        if (collision.CompareTag("Player"))
        {

            if (collision.GetComponent<PlayerController>().CanTeleport)
            {
                if (MapExit)
                {
                    GoToNextWorld();
                    return;
                }
                Teleport(collision.transform);
                collision.GetComponent<PlayerController>().CanTeleport = false;
            }
            
        }
    }

    private void GoToNextWorld()
    {
        throw new NotImplementedException();
    }


    void Teleport(Transform tr)
    {
        if (otherDoor != null)
        {
            tr.SetPositionAndRotation(otherDoor.transform.position, Quaternion.identity);
        }
        
    }
    
}
