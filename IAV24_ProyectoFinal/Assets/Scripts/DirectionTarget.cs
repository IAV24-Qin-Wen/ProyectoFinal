using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-700)]
public class DirectionTarget : MonoBehaviour,IDirection  {
	[SerializeField]
	MapServer map;
	[SerializeField]
	float weight=1;

	Vector2I position;
	public Vector3 GetDirection()
	{
		position = map.GetGridPosition (transform.position);

		return weight*(GetInfluence(position+Vector2I.forward)*Vector3.forward
		               +GetInfluence(position-Vector2I.forward)*-Vector3.forward
		               +GetInfluence(position+Vector2I.right)*Vector3.right
		               +GetInfluence(position-Vector2I.right)*-Vector3.right

		               +GetInfluence(position+Vector2I.right+Vector2I.forward)*(Vector3.right+Vector3.forward)
		               +GetInfluence(position+Vector2I.right-Vector2I.forward)*(Vector3.right-Vector3.forward)
		               +GetInfluence(position-Vector2I.right+Vector2I.forward)*(-Vector3.right+Vector3.forward)
		               +GetInfluence(position-Vector2I.right-Vector2I.forward)*(-Vector3.right-Vector3.forward)
		              );
	}

	float GetInfluence(Vector2I position)
	{
		var influence = map.GetInfluence (position);
		if (influence == -Mathf.Infinity)
			return 0f;
		else
			return map.GetInfluence (position);
	}
}
