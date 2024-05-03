using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirection{Vector3 GetDirection();}

[DefaultExecutionOrder(-700)]
public class DirectionRandom : MonoBehaviour,IDirection  {
	[SerializeField]
	float weight=1;
	public Vector3 GetDirection()
	{
		var direction = weight*( new Vector3(Random.Range(-50, 50),0,Random.Range(-50,50))-transform.position).normalized;

		return weight*( new Vector3(
			Random.Range(-50, 50),
			0,
			Random.Range(-50,50)
		)-transform.position).normalized;
	}
}
