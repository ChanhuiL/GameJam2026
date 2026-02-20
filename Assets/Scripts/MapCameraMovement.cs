using UnityEngine;
using UnityEngine.InputSystem;

public class MapCameraMovement : MonoBehaviour
{
    Vector2 vInput = Vector2.zero;

    void OnMove(InputValue value)   // 값이 변할때만 호출됨
    {
        vInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (vInput.sqrMagnitude > 0f)
        {
            transform.position += new Vector3(vInput.x, vInput.y, 0) * -Time.deltaTime;
        }
    }
}
