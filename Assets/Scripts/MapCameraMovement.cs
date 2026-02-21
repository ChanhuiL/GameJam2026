using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class MapCameraMovement : MonoBehaviour
{
    [Header("Movmenet")]
    public float        movementDrag = 3f;
    public float        movementSpeed = 4f;

    [Header("Zoom")]
    public float        scrollSensitive = 1f;
    public float        zoomDrag = 1f;
    public float        minSize = 1f;
    public float        maxSize = 10f;

    [SerializeField] CinemachineCamera   virtualCam;
    [SerializeField] InputActionProperty scrollAction;

    Vector2             vInput = Vector2.zero;
    float               targetZoomSize = 4f;
    bool                isFocusing = false;

    private void Awake()
    {
    }

    void OnMove(InputValue value)   // ���� ���Ҷ��� ȣ���
    {
        Vector2 curInput = value.Get<Vector2>();

        vInput = curInput;
    }

    void Update()
    {
        if (isFocusing) return;
        if(vInput != Vector2.zero) 
            transform.position += new Vector3(vInput.x, vInput.y, 0) * Time.deltaTime * movementSpeed;

        Vector2 scrollValue = scrollAction.action.ReadValue<Vector2>();
        if(scrollValue.y != 0f)
        {
            targetZoomSize += scrollValue.y * scrollSensitive;
            targetZoomSize = Mathf.Clamp(targetZoomSize, minSize, maxSize);
        }

        virtualCam.Lens.OrthographicSize = Mathf.Lerp(virtualCam.Lens.OrthographicSize, targetZoomSize, Time.deltaTime * zoomDrag);
    }

    private void OnEnable()
    {
        scrollAction.action.Enable();
    }

    public void FocusCameraToHere(Vector2 pos)
    {
        isFocusing = true;
        transform.position = pos + Vector2.right * targetZoomSize;
    }

    public void UnfocusCamera()
    {
        isFocusing = false;
    }
}
