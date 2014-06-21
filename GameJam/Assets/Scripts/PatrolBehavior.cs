using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBehavior : MonoBehaviour
{
	private enum PatrolBehaviorState { MovingToNextPatrolPoint = 0, WaitingForNextMove = 1 }

	public GameObject patrolRouter;
	private Transform[] patrolPoints;
	public float patrolWalkSpeed = 2.0f;
	public AnimationClip[] patrolWalkAnimationList;
	public AnimationClip[] patrolIdleAnimationList;
	public float delayAtPatrolPointMin = 2.0f;
	public float delayAtPatrolPointMax = 5.0f;
	
	private PatrolBehaviorState patrolBehaviorState;
	private int patrolPointIndex;
	private float timeToWaitBeforeNextMove;
	
	void PlayRandomWalkAnimation()
	{
 		animation.CrossFade(patrolWalkAnimationList[Random.Range(0, patrolWalkAnimationList.GetLength(0))].name, 0.2f);
	}
	
	void PlayRandomIdleAnimation()
	{
		animation.CrossFade(patrolIdleAnimationList[Random.Range(0, patrolIdleAnimationList.GetLength(0))].name, 0.2f);
	}
	
	void Start()
	{
		patrolPoints = patrolRouter.GetComponent<patrolRouterBehaviour>().getRoute();
	
		patrolPointIndex = 0;
		patrolBehaviorState = PatrolBehaviorState.MovingToNextPatrolPoint;
		timeToWaitBeforeNextMove = -1.0f;
		PlayRandomWalkAnimation();
	}
	
	Vector3 GetCurrentDestination()
	{
		return patrolPoints[patrolPointIndex].position;
	}
	
	void ChooseNextDestination()
	{
		patrolPointIndex++;
		if (patrolPointIndex >= patrolPoints.GetLength(0))
		{
			patrolPointIndex = 0;
		}
	}
	
	void UpdateMovingToNextPatrolPoint()
	{
		Vector3 currentDestination = GetCurrentDestination();
		transform.forward = currentDestination - transform.position;
		transform.Translate(Vector3.forward * patrolWalkSpeed * Time.deltaTime);
		transform.LookAt(currentDestination);

		if ((GetCurrentDestination() - transform.position).magnitude < 0.1f)
		{
			timeToWaitBeforeNextMove = Random.Range(delayAtPatrolPointMin, delayAtPatrolPointMax);
			patrolBehaviorState = PatrolBehaviorState.WaitingForNextMove;
			PlayRandomIdleAnimation();
		}	
	}
	
	void UpdateWaitingForNextMove()
	{
		timeToWaitBeforeNextMove -= Time.deltaTime;
		if (timeToWaitBeforeNextMove < 0.0f)
		{
			ChooseNextDestination();
			patrolBehaviorState = PatrolBehaviorState.MovingToNextPatrolPoint;
			PlayRandomWalkAnimation();
		}
	}
	
	void Update()
	{
		if (patrolBehaviorState == PatrolBehaviorState.MovingToNextPatrolPoint)
		{
			UpdateMovingToNextPatrolPoint();
		}
		else if (patrolBehaviorState == PatrolBehaviorState.WaitingForNextMove)
		{
			UpdateWaitingForNextMove();
		}
	}
	
	void LateUpdate()
	{
		transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
	}
}