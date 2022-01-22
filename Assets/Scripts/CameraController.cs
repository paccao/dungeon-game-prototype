using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public CharacterController cameraController;

    public float angleRotationSpeed = 150f;
    public float speed = 17f;

    private Vector3 newCurrentPlayerPos;
    private Vector3 newCurrentCameraPos;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // newCurrentCameraPos = transform.position;
        newCurrentPlayerPos = player.transform.position;

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(newCurrentPlayerPos, Vector3.up, angleRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(newCurrentPlayerPos, Vector3.down, angleRotationSpeed * Time.deltaTime);
        }

        // Forward and backwards needs to move according to the global axis, Space.World or similar

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 movement = transform.rotation * Vector3.forward;
            cameraController.Move(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 movement = transform.rotation * Vector3.left;
            cameraController.Move(movement * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 movement = transform.rotation * Vector3.back;
            cameraController.Move(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 movement = transform.rotation * Vector3.right;
            cameraController.Move(movement * speed * Time.deltaTime);
        }
    }
}
