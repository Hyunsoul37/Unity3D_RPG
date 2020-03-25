using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public enum State
	{
		Idle,
		Walk,
		Attack,
		Attack_Idle,
		Dead,
	}

	public State CurrentState = State.Idle;

	private PlayerAni m_Ani;

	private Vector3 CurrentTargetPos;
	public float rotAngle = 360f;
	public float moveSpeed = 2f;

	private GameObject CurrentEnemy;
	private float Attack_Delay = 2f;
	private float Attack_Timer = 0f;
	private float Attack_Distance = 1.5f;
	

	PlayerParameter p_Parameter;
	EnemyParameter e_Parameter;

	private void Start()
	{
		m_Ani = GetComponent<PlayerAni>();
		ChangeState(State.Idle, PlayerAni.Ani_Idle);

		p_Parameter = GetComponent<PlayerParameter>();
		p_Parameter.InitParameter();
		p_Parameter.DeadEvent.AddListener(ChangeToPlayerDead);
	}

	private void Update()
	{
		UpdateState();
	}

	public void AttackEnemy(GameObject Enemy)
	{
		if(CurrentEnemy != null && CurrentEnemy == Enemy)
			return;

		e_Parameter = Enemy.GetComponent<EnemyParameter>();

		if(e_Parameter.isDead == false)
		{
			CurrentEnemy = Enemy;
			CurrentTargetPos = CurrentEnemy.transform.position;

			GameManager.Getinstance().ChangeCurrentTarget(CurrentEnemy);

			ChangeState(State.Walk, PlayerAni.Ani_Walk);
		}
		else
		{
			e_Parameter = null;
		}
	}

	private void UpdateState()
	{
		switch(CurrentState)
		{
			case State.Idle:
				Idle_State();
				break;
			case State.Walk:
				Walk_State();
				break;
			case State.Attack:
				Attack_State();
				break;
			case State.Attack_Idle:
				Attack_Idle_State();
				break;
			case State.Dead:
				Dead_State();
				break;

			default:
				break;
		}
	}

	private void ChangeState(State newState, int aniNum)
	{
		if (CurrentState == newState)
			return;

		m_Ani.ChangeAni(aniNum);
		CurrentState = newState;
	}

	private void Idle_State()
	{

	}

	private void Walk_State()
	{
		TurnToDestination();
		MoveToDestination();
	}

	private void Attack_State()
	{
		Attack_Timer = 0f;
		this.transform.LookAt(CurrentTargetPos);
		ChangeState(State.Attack_Idle, PlayerAni.Ani_Attack_Idle);
	}

	private void Attack_Idle_State()
	{
		if(Attack_Timer > Attack_Delay)
		{
			ChangeState(State.Attack, PlayerAni.Ani_Attack);
		}

		Attack_Timer += Time.deltaTime;
	}

	private void Dead_State()
	{
		
	}

	public void MoveTo(Vector3 pos)
	{
		if (CurrentState == State.Dead)
			return;

		CurrentEnemy = null;
		CurrentTargetPos = pos;
		ChangeState(State.Walk, PlayerAni.Ani_Walk);
	}

	private void TurnToDestination()
	{
		Quaternion lookRotation = Quaternion.LookRotation(CurrentTargetPos - this.transform.position);

		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, lookRotation, Time.deltaTime * rotAngle);
	}

	private void MoveToDestination()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, CurrentTargetPos, Time.deltaTime * moveSpeed);

		if(CurrentEnemy == null)
		{
			if (this.transform.position == CurrentTargetPos)
			{
				ChangeState(State.Idle, PlayerAni.Ani_Idle);
			}
		}
		else if(Vector3.Distance(this.transform.position, CurrentTargetPos) < Attack_Distance)
		{
			ChangeState(State.Attack, PlayerAni.Ani_Attack);
		}
	}

	public void AttackCalculate()
	{
		if (CurrentEnemy == null)
			return;

		int AttackPower = p_Parameter.GetRandomAttack();
		e_Parameter.SetEnemyAttack(AttackPower);
	}

	public void CurrentEnemyDead()
	{
		ChangeState(State.Idle, PlayerAni.Ani_Idle);
		print("enemy was Killed");

		QuestManager.Getinstace().SendMessage("QuestCheck", CurrentEnemy);
		CurrentEnemy = null;
	}

	public void ChangeToPlayerDead()
	{
		print("Player was Dead");
		ChangeState(State.Dead, PlayerAni.Ani_Dead);
	}

	public void ConverseNPC(GameObject NPC)
	{
		NPC.GetComponent<ShopNPC>().ConversePlayer();
	}

	//public void CutDownTree(GameObject Tree)
	//{
	//	Tree.GetComponent<TreeScript>().CutDownTree();
	//}
}
