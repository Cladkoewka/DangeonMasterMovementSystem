using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _smoothTransition;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private float _transitionRotationSpeed;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Checker _forwardChecker;
    [SerializeField] private Checker _backChecker;
    [SerializeField] private Checker _leftChecker;
    [SerializeField] private Checker _rightChecker;

    private Vector3 _targetGridPosition;
    private Vector3 _targetRotation;

    private const float STEP = 2;

    private void Start()
    {
        _targetGridPosition = _startPoint.position;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown("w") && !_forwardChecker.CheckWallCollisions()) MoveForward();
        if (Input.GetKeyDown("s") && !_backChecker.CheckWallCollisions()) MoveBack();
        if (Input.GetKeyDown("a") && !_leftChecker.CheckWallCollisions()) MoveLeft();
        if (Input.GetKeyDown("d") && !_rightChecker.CheckWallCollisions()) MoveRight();
        if (Input.GetKeyDown("q")) RotateLeft();
        if (Input.GetKeyDown("e")) RotateRight();
    }
    private void MovePlayer()
    {
        Vector3 targetPosition = _targetGridPosition;

        if (_targetRotation.y > 270) _targetRotation.y = 0;
        if (_targetRotation.y < 0) _targetRotation.y = 270;

        if (!_smoothTransition)
        {
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(_targetRotation);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _transitionSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_targetRotation), Time.deltaTime * _transitionRotationSpeed);
        }
    }

    private void RotateLeft() { if (AtRest) _targetRotation -= Vector3.up * 90; }
    private void RotateRight() { if (AtRest) _targetRotation += Vector3.up * 90; }
    private void MoveForward() { if (AtRest) _targetGridPosition += transform.forward * STEP; }
    private void MoveBack() { if (AtRest) _targetGridPosition -= transform.forward * STEP; }
    private void MoveLeft() { if (AtRest) _targetGridPosition -= transform.right * STEP; }
    private void MoveRight() { if (AtRest) _targetGridPosition += transform.right * STEP; }
    
    private bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, _targetGridPosition) < 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, _targetRotation) < 0.05f))
                return true;
            else
                return false;
        }
    }
}
