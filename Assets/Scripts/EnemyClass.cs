using UnityEngine;

public class EnemyClass : MonoBehaviour
{
	private float _range = 25f;
	[SerializeField] private LayerMask Mask;
	[SerializeField]
	private Transform Player;

	void Update()
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
		}
		else
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * _range, Color.green);
		}
	}
}
