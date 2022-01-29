using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private AnimationCurve curve;
	[SerializeField] private float speed = 0.1f;

	private Vector3 newPosition;

	private float current, target;

	private void Awake()
	{
		newPosition = transform.position;
	}

	private void Update()
	{
		MovePlayer();
	}

	private void MovePlayer()
	{
		if (Input.GetMouseButtonDown(0))
		{

			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float hitInfo;
			if (plane.Raycast(ray, out hitInfo))
			{
				newPosition = ray.GetPoint(hitInfo);
				newPosition.y += 1;
			}
		}
		transform.position = Vector3.MoveTowards(transform.position, newPosition, curve.Evaluate(speed * Time.deltaTime));
	}

	private void OnMouseDown()
	{
		CameraController.instance.followTransform = transform;
	}
}
