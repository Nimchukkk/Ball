using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        const float speedRotation = 50f;
        transform.Rotate(new Vector3(0,90,0), speedRotation * Time.deltaTime);
    }
}
