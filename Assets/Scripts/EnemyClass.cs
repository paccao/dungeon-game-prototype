using UnityEngine;

public class EnemyClass : MonoBehaviour
{
	private float _range = 25f;
	private float _rotateSpeed = 6f;
	[SerializeField] private LayerMask Mask;
	[SerializeField]
	private Transform Player;

	private void FixedUpdate()
	{
		Vector3 directionToPlayer = Player.transform.position - transform.position;
		directionToPlayer.Normalize();
		OnDetectPlayer(directionToPlayer);
	}

	void OnDetectPlayer(Vector3 directionToPlayer)
	{
		Ray ray = new Ray(transform.position, directionToPlayer);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, _range, Mask))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

			// Smoothly rotate towards player
			var targetRotation = Quaternion.LookRotation(Player.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
		}
		else
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * _range, Color.green);
		}
	}
}
