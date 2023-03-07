using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager ;
    [SerializeField] private float _speed = 0.1f;

    private Transform _transform;
    private Vector3 _direction;

    private void Awake() => _transform = GetComponent<Transform>();
  

    public void SetDirectionAndStartMove(Vector3 direction)
    {
        StopCoroutine(nameof(Move));
        _direction = direction;
        StartCoroutine(nameof(Move));
    }

    private IEnumerator Move()
    {
        var currentSpeed = _speed;
        const int numberOfFramesOfBallMovement = 650;
        var slowDown = currentSpeed / numberOfFramesOfBallMovement;

        for (var i = 0; i < numberOfFramesOfBallMovement; i++)
        {
            _transform.Translate(_direction * (currentSpeed * Time.deltaTime) , Space.World);

            var directionRotation = new Vector3(_direction.z, -_direction.y, -_direction.x);
            var speedRotation = currentSpeed * 5000f;
            _transform.Rotate(directionRotation, speedRotation * Time.deltaTime , Space.World);

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
}