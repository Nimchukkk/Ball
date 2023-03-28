using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    private CharacterController _characterController;
    private Transform _transform;
    private Vector3 _direction;
    private float _speed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Partition>())
        {
            _direction = Vector3.Reflect(_direction, hit.normal);
        }

        if (hit.gameObject.GetComponent<Coin>())
        {
            _scoreManager.StartAddScore();
            Destroy(hit.gameObject);
        }
    }

    public void SetDirectionAndStartMove(Vector3 direction)
    {
        StopCoroutine(nameof(Move));
        _direction = direction;
        StartCoroutine(nameof(Move));
    }

    public void SetSpeed(float speed)
    {
        _speed = Mathf.Clamp(speed, 0, 0.5f);
    }

    private IEnumerator Move()
    {
        var numberOfFramesOfBallMovement = 850 * _speed;
        var slowDown = _speed / numberOfFramesOfBallMovement;

        for (var i = 0; i < (int)numberOfFramesOfBallMovement; i++)
        {
            _characterController.Move(_direction * _speed /2f);

            _transform.Rotate(new Vector3(_direction.z, -_direction.y, -_direction.x), 
                _speed * 2250f * Time.deltaTime, 
                Space.World);

            _speed -= slowDown;

            yield return null;
        }
    }
}