using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController instance;

	public Transform followTransform;
	public Transform cameraTransform;

	private float normalSpeed = 0.14f;
	private float fastSpeed = 0.17f;
	private float movementSpeed = 0.14f;
	private float movementTime = 5f;
	private float rotationAmount = 0.33f;
	private float rotationPanAmount = 8f;

	private float minY = 0f;
	private float maxY = 19f;
	private float minZ = 0f;
	private float maxZ = 19f;

	[SerializeField] private Vector3 zoomAmount; // Y -0.055 Z 0.055
	[SerializeField] private Vector3 zoomAmountScroll; // Y -8 Z 8

	private Vector3 newPosition;
	private Quaternion newRotation;
	private Vector3 newZoom;

	private Vector3 dragStartPosition;
	private Vector3 dragCurrentPosition;
	private Vector3 rotateStartPosition;
	private Vector3 rotateCurrentPosition;


	void Start()
	{
		instance = this;
		newPosition = transform.position;
		newRotation = transform.rotation;
		newZoom = cameraTransform.localPosition;
	}

	void Update()
	{
		if (followTransform != null)
		{
			transform.position = followTransform.position;
		}
		else
		{
			HandleMouseInput();
			HandleMovementInput();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			followTransform = null;
		}
	}

	void HandleMouseInput()
	{
		if (Input.mouseScrollDelta.y != 0)
		{
			newZoom += Input.mouseScrollDelta.y * zoomAmountScroll;
		}

		if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftAlt))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry;

			if (plane.Raycast(ray, out entry))
			{
				dragStartPosition = ray.GetPoint(entry);
			}
		}

		if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt))
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry;

			if (plane.Raycast(ray, out entry))
			{
				dragCurrentPosition = ray.GetPoint(entry);

				newPosition = transform.position + dragStartPosition - dragCurrentPosition;
			}
		}

		if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftAlt))
		{
			rotateStartPosition = Input.mousePosition;
		}
		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt))
		{
			rotateCurrentPosition = Input.mousePosition;

			Vector3 difference = rotateStartPosition - rotateCurrentPosition;

			rotateStartPosition = rotateCurrentPosition;

			newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / rotationPanAmount));
		}
	}

	void HandleMovementInput()
	{
		// Fast movement
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			movementSpeed = fastSpeed;
		}
		else
		{
			movementSpeed = normalSpeed;
		}

		// Normal movement
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			newPosition += (transform.forward * movementSpeed);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			newPosition += (transform.forward * -movementSpeed);
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			newPosition += (transform.right * -movementSpeed);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			newPosition += (transform.right * movementSpeed);
		}

		// Rotate
		if (Input.GetKey(KeyCode.Q))
		{
			newRotation *= Quaternion.Euler(-Vector3.up * rotationAmount);
			// transform.RotateAround(cameraTransform.position, Vector3.up, rotationAmount * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.E))
		{
			newRotation *= Quaternion.Euler(-Vector3.up * -rotationAmount);
			// transform.RotateAround(cameraTransform.position, Vector3.down, rotationAmount * Time.deltaTime);
		}

		newZoom.y = Mathf.Clamp(newZoom.y, minY, maxY);
		newZoom.z = Mathf.Clamp(newZoom.z, minZ, maxZ);

		// Interpolate movement for smoothness
		transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, movementTime * Time.deltaTime);
		cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, movementTime * Time.deltaTime);
	}
}
