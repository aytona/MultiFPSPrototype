using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float mouseSensitivity = 3f;
    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movHorizontal = transform.right * horizontal;
        Vector3 movVertical = transform.forward * vertical;
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        motor.Move(velocity);

        // Player Rotation
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * mouseSensitivity;
        motor.Rotate(rotation);

        // Camera Rotation
        float xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRotation, 0f, 0f) * mouseSensitivity;
        motor.CameraRotate(cameraRotation);
    }
}
