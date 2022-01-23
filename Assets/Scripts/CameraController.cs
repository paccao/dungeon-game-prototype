using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public CharacterController cameraController;
    public Transform target;

    public float angleRotationSpeed = 150f;
    public float speed = 17f;

    private Vector3 currentPlayerPos;
    private Vector3 currentTargetPos;

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentPlayerPos = player.transform.position;
        currentTargetPos = target.transform.position;

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(target.position, Vector3.up, angleRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(target.position, Vector3.down, angleRotationSpeed * Time.deltaTime);
        }

        // Forward and backwards needs to move according to the global axis, Space.World or similar

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 dir = Vector3.forward;
            dir = transform.InverseTransformDirection(dir);
            dir.y = 0;
            dir.Normalize();
            cameraController.Move(dir * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 movement = transform.rotation * Vector3.left;
            cameraController.Move(movement * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 dir = Vector3.back;
            dir = transform.TransformDirection(dir);
            dir.y = 0;
            dir.Normalize();
            cameraController.Move(dir * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 movement = transform.rotation * Vector3.right;
            cameraController.Move(movement * speed * Time.deltaTime);
        }
    }
}
