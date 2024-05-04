using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
	public Vector3 direction;
    protected Vector3 velocity;
    protected CharacterController character;
    protected NavMeshAgent agent;
	 
	[SerializeField]
	float _speed=2f;

	public static int pathfindingIterationsPerFrame;

    // Use this for initialization
    protected virtual void Start()
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

	protected virtual void Update()
	{
		if (Time.time > timer) {
			direction = Vector3.zero;
			timer = Time.time + delayUpdateDirection;
			var directions = GetComponents<IDirection> ();
			foreach (var d in directions)
				direction += d.GetDirection ();
			//if (agent && agent.enabled)
			//	agent.SetDestination (transform.position+ velocity);
		}
		//Debug.Log(direction);
		//velocity = direction;
		//velocity.Normalize();
		//velocity *= _speed;
		//velocity.y = 0;

		////		transform.position += _velocity * Time.deltaTime;
		//if (character && character.enabled)
		//	character.SimpleMove(velocity);
	}
}