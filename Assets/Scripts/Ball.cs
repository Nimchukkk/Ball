using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    private Transform _transform;
    private Vector3 _direction;
    private float _speed;
    private int _obstacleLayer;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    public void SetDirectionAndStartMove(Vector3 direction)
    {
        StopCoroutine(nameof(Move));
        _direction = direction;
        StartCoroutine(nameof(Move));
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private IEnumerator Move()
    {
        var currentSpeed = Mathf.Clamp(_speed, 0, 0.45f);
        ;
        var numberOfFramesOfBallMovement = 1000 * currentSpeed;
        var slowDown = currentSpeed / numberOfFramesOfBallMovement;

        for (var i = 0; i < (int)numberOfFramesOfBallMovement; i++)
        {
            _transform.Translate(_direction * (currentSpeed / 10 * Time.deltaTime), Space.World);

            var directionRotation = new Vector3(_direction.z, -_direction.y, -_direction.x);
            var speedRotation = currentSpeed * 1250f;
            _transform.Rotate(directionRotation, speedRotation * Time.deltaTime, Space.World);

            currentSpeed -= slowDown;

            var ray = new Ray(_transform.position, _direction);
            const float maxDistance = 0.5f;
            if (Physics.Raycast(ray, out var hit, maxDistance))
            {
                if (hit.collider.isTrigger)
                {
                    _scoreManager.StartAddScore();
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    var reflectDirection = Vector3.Reflect(_direction, hit.normal);
                    _direction = reflectDirection;
                }
            }
            
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position , .65f);
    }
}