using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float mouseSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 150f;

    [Header("Joint settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 5f;
    [SerializeField]
    private float jointMaxForce = 10f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        motor = GetComponent<PlayerMotor>();
        SetJointSettings(jointSpring);
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

        // Thruster Force
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
            SetJointSettings(jointSpring);
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
