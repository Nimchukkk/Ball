using UnityEngine;
public class InputControl : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Line _line;
    
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseButtonDown();
        }

        if (Input.GetMouseButton(0))
        {
            MouseButton();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseButtonUp();
        }
    }
    private void MouseButtonDown() => _startPosition = Input.mousePosition;
    private void MouseButton()
    {
        _endPosition = Input.mousePosition;
        var differenceMousePosition = (_startPosition - _endPosition);
        _direction = new Vector3 (differenceMousePosition.x, 0f, differenceMousePosition.y);
        
        _line.ShowLine();
        _line.SetLine(_direction / 35f);
    }

    private void MouseButtonUp()
    {
        var speed = Vector3.Distance(_startPosition, _endPosition);
        _ball.SetSpeed(speed / 500);
        _line.HideLine();
        _ball.SetDirectionAndStartMove(_direction);
    }
}