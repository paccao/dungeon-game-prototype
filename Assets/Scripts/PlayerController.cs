using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 0.1f;

	private Vector3 newPosition;

	private float current, target;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{

			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float hitInfo;
			if (plane.Raycast(ray, out hitInfo))
			{
				newPosition = ray.GetPoint(hitInfo);
			}
		}

		transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
	}

	private void OnMouseDown()
	{
		CameraController.instance.followTransform = transform;
	}
}
