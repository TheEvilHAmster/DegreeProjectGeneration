using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horInput;
    [SerializeField] private float verInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 move;
    [SerializeField] private float speedModifyer = 10;
    public bool CanTeleport = true;

    private float Timer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
        move.Set(horInput,verInput);
        rb.AddForce(move * speedModifyer);
        if (!CanTeleport)
        {
            SetTeleportingState();
        }

        
    }

    void SetTeleportingState()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            CanTeleport = true;
            resetTimer();
        }
    }

    void resetTimer()
    {
        Timer = 0.5f;
    }
    
}
