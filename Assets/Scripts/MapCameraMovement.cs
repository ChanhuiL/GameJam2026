using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCameraMovement : MonoBehaviour
{
    public float movementDrag = 2f;
    public float movementSpeed = 2f;
    float   movingTimer = 0f;
    bool    isMoving = false;
    Vector2 vInput = Vector2.zero;

    void OnMove(InputValue value)   // 값이 변할때만 호출됨
    {
        Vector2 curInput = value.Get<Vector2>();

        if(curInput != Vector2.zero)
        {
            isMoving = true;
            vInput = curInput;
        }
        else
        {
            isMoving = false;
        }
    }

    void Update()
    {
        movingTimer += Time.deltaTime * movementDrag * (isMoving ? 1f : -1f);
        movingTimer = Mathf.Clamp01(movingTimer);
        
        if(movingTimer > 0f)
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(vInput.x, vInput.y, 0) * Time.deltaTime * movementSpeed, movingTimer);
    }
}
