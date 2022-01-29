using UnityEngine;

public class EnemyClass : MonoBehaviour
{
	private float range = 25f;
	[SerializeField] private LayerMask mask;

	// EnemyClass(float enemyRange)
	// {
	// 	range = enemyRange;
	// }

	void Update()
	{
		OnDetectPlayer();
	}

	void OnDetectPlayer()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, range, mask))
		{
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
		}
		else
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.green);
		}
	}
}
