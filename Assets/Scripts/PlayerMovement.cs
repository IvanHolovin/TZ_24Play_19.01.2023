using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _boardWidth;
    [SerializeField] private Transform _mainCameraTransform;
    private Transform _playerTransform;
    private Rigidbody _rb;
    

    private void Awake()
    {
        _playerTransform = this.transform;
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.touchCount == 1)
        {
            MovePlayer();
        }
    }

    private void LateUpdate()
    {
        Vector3 camPosition = new Vector3(_mainCameraTransform.transform.position.x, _mainCameraTransform.transform.position.y,
            _playerTransform.position.z);
        _mainCameraTransform.transform.position = camPosition;
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width/2;
        float xPosition;
        if (Input.GetMouseButton(0))
        {
            xPosition = (Input.mousePosition.x - halfScreen) / halfScreen; 
        }
        else
        {
            Touch touch = Input.GetTouch(0);
            xPosition = (touch.position.x - halfScreen) / halfScreen; 
        }
        
        float resultPosition = Mathf.Clamp(xPosition * _boardWidth, -_boardWidth,_boardWidth);

        _playerTransform.localPosition = new Vector3(resultPosition, _playerTransform.position.y, _playerTransform.position.z);
        
        _playerTransform.position += Vector3.forward * _speed * Time.deltaTime;
    }
    
    
    
}
