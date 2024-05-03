using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
	Vector3 direction;
	Vector3 velocity;
	CharacterController character;
	NavMeshAgent agent;

	[SerializeField]
	float _speed=2f;

	public static int pathfindingIterationsPerFrame;
		
	// Use this for initialization
	void Start()
	{
		//navmesh performance and init
		pathfindingIterationsPerFrame = pathfindingIterationsPerFrame+10;
		NavMesh.pathfindingIterationsPerFrame = pathfindingIterationsPerFrame;
		agent = GetComponent<NavMeshAgent> ();
		if (agent)
			agent.velocity = velocity;
		
		character = GetComponent<CharacterController> ();
		delayUpdateDirection += Random.value*delayUpdateDirection;
	}

	[SerializeField]
	float delayUpdateDirection=.1f;
	float timer;

	void Update()
	{
		if (Time.time > timer) {
			direction = Vector3.zero;
			timer = Time.time + delayUpdateDirection;
			var directions = GetComponents<IDirection> ();
			foreach (var d in directions)
				direction += d.GetDirection ();
			if (agent && agent.enabled)
				agent.SetDestination (transform.position+ velocity);
		}

		velocity = direction;
		velocity.Normalize();
		velocity *= _speed;
		velocity.y = 0;

//		transform.position += _velocity * Time.deltaTime;
		if (character&& character.enabled)
			character.SimpleMove(velocity);
	}
}