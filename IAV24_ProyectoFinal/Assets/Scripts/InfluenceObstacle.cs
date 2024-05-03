using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceObstacle : MonoBehaviour {
	public Bounds GetBounds()
	{
		return GetComponent<MeshRenderer> ()? GetComponent<MeshRenderer>().bounds : new Bounds(transform.position, Vector3.one);
	}

	void OnEnable()
	{
		foreach(var m in FindObjectsOfType<InfluenceMapControl>())
			m.RegisterObstacle(this);
	}
	void OnDisable()
	{
		foreach(var m in FindObjectsOfType<InfluenceMapControl>())
			m.DeregisterObstacle(this);
	}
}
