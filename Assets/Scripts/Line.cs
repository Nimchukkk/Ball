using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _line;

    private void Awake() => _line = GetComponent<LineRenderer>();
    
    private void Start()
    {
        _line.positionCount = 2;
        HideLine();
    }

    private void Update()
    {
        if (_line.enabled)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void HideLine() => _line.enabled = false;

    public void ShowLine() => _line.enabled = true;

    public void SetLine(Vector3 endPoint)
    {
        _line.SetPosition(0, transform.localPosition);
        _line.SetPosition(1, endPoint);
    }
}