using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies:")]
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement Behaviours Variables:")]
    [SerializeField] private float _moveSpeed = 200f;
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rb.velocity = new Vector2(_input.horizontal * _moveSpeed * Time.fixedDeltaTime, _input.vertical * _moveSpeed * Time.fixedDeltaTime);
    }
}
