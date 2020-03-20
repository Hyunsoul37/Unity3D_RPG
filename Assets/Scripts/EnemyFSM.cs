using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum M_State
	{
		Idle,
		Chase,
		Attack,
		Dead,
		Attack_Idle,
	}

	public M_State CurrentState = M_State.Idle;

	private EnemyAni e_Ani;

	private Transform player;

	private float Chase_Distance = 5f;
	private float Attack_Distance = 1.2f;
	private float ReChase_Distance = 3f;

	private float Attack_Delay = 2f;
	private float Attack_Timer = 0f;

	private float rotAngle = 360f;
	private float MoveSpeed = 1.5f;

	private PlayerParameter p_Parameter;
	private EnemyParameter e_Parameter;

	public GameObject SelectMark;

	private GameObject MyRespawnObj;
	public int SpawnID { get; set; }
	private Vector3 OriginPos;

	private CharacterController controller;

	private void Start()
	{
		e_Ani = GetComponent<EnemyAni>();
		ChangeState(M_State.Idle, EnemyAni.Idle);

		player = GameObject.FindGameObjectWithTag("Player").transform;

		e_Parameter = GetComponent<EnemyParameter>();
		e_Parameter.DeadEvent.AddListener(CallDeadEvent);
		p_Parameter = player.gameObject.GetComponent<PlayerParameter>();

		controller = GetComponent<CharacterController>();

		HideSelection();
	}

	private void Update()
	{
		UpdateState();
	}

	private void UpdateState()
	{
		switch(CurrentState)
		{
			case M_State.Idle:
				Idle_State();
				break;
			case M_State.Chase:
				Chase_State();
				break;
			case M_State.Attack:
				Attack_State();
				break;
			case M_State.Dead:
				Dead_State();
				break;
			case M_State.Attack_Idle:
				Attack_Idle_State();
				break;

			default:
				break;
		}
	}


	private void Idle_State()
	{
		if(GetDistanceFromPlayer() < Chase_Distance)
		{
			ChangeState(M_State.Chase, EnemyAni.Walk);
		}
	}

	public void ChangeState(M_State newState, int aniName)
	{
		if (CurrentState == newState)
			return;

		e_Ani.ChangeAni(aniName);
		CurrentState = newState;
	}

	private void Chase_State()
	{
		if(GetDistanceFromPlayer() < Attack_Distance)
		{
			ChangeState(M_State.Attack, EnemyAni.Attack);
		}
		else
		{
			TurnToDestination();
			MoveToDestination();
		}
	}

	private void Attack_State()
	{
		if(GetDistanceFromPlayer() > ReChase_Distance)
		{
			Attack_Timer = 0f;
			ChangeState(M_State.Chase, EnemyAni.Walk);
		}
		else
		{
			Attack_Timer = 0f;
			this.transform.LookAt(player.position);
			ChangeState(M_State.Attack_Idle, EnemyAni.Attack_Idle);
		}
	}

	private void Dead_State()
	{
		GetComponent<BoxCollider>().enabled = false;
	}

	private void Attack_Idle_State()
	{
		if (Attack_Timer > Attack_Delay)
		{
			ChangeState(M_State.Attack, EnemyAni.Attack);
		}

		Attack_Timer += Time.deltaTime;
	}

	private void TurnToDestination()
	{
		Quaternion LookRotation = Quaternion.LookRotation(player.position - this.transform.position);

		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, LookRotation, Time.deltaTime * rotAngle);
	}

	private void MoveToDestination()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, Time.deltaTime * MoveSpeed);
	}

	private float GetDistanceFromPlayer()
	{
		float distance = Vector3.Distance(this.transform.position, player.position);

		return distance;
	}

	private void CallDeadEvent()
	{
		ChangeState(M_State.Dead, EnemyAni.Dead);
		player.gameObject.SendMessage("CurrentEnemyDead");

		ObjectManager.instance.DropCoinToPosition(this.transform.position, e_Parameter.RewardMoney);
		p_Parameter.Current_Exp += e_Parameter.Exp;
		StartCoroutine(RemoveMeFromWorld());
	}

	public void AttackCalculate()
	{
		int AttackPower = e_Parameter.GetRandomAttack();
		p_Parameter.SetEnemyAttack(AttackPower);
	}

	public void HideSelection()
	{
		SelectMark.SetActive(false);
	}

	public void ShowSelection()
	{
		SelectMark.SetActive(true);
	}

	public void SetReSpawnObj(GameObject respawnObj, int spawnID, Vector3 originPos)
	{
		this.MyRespawnObj = respawnObj;
		this.SpawnID = spawnID;
		this.OriginPos = originPos;
	}

	IEnumerator RemoveMeFromWorld()
	{
		yield return new WaitForSeconds(1f);

		ChangeState(M_State.Idle, EnemyAni.Idle);

		MyRespawnObj.GetComponent<RespawnObj>().RemoveMonster(SpawnID);
	}

	public void AddToWorldAgain()
	{
		this.transform.position = OriginPos;

		GetComponent<EnemyParameter>().InitParameter();
		GetComponent<BoxCollider>().enabled = true;
	}
}
