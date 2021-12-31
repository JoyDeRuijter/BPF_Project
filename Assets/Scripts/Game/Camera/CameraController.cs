using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [Range (150f, 450f)]
    [SerializeField] private float mouseSensitivity;

    private float yRotation;
    private Transform playerBody;
    #endregion

    void Awake()
    {
        mouseSensitivity = 300f;
        yRotation = 0;
        playerBody = GameObject.FindWithTag("Player").GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera() 
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -50f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
