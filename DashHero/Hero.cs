using UnityEngine;

public class Hero : MonoBehaviour
{
	public enum Status
	{
		Idle,
		BeHit,
		Walk,
		Attack,
		Dead
	}
	public enum HeroTypes
	{
		Melee,
		Archer,
		Caster,
		Pastor
	}

	//人物数据
	public int HP;//血量
	public float Radius;//射程
	public Status status;//个体状态
	public HeroTypes type;//英雄类型

	//检测变量
	public Vector2 offset;//检测射线相对中心偏移量
	public Transform preHero;//身前有可能重叠穿模的英雄
	public Transform curCheckedMonster;//当前锁定怪物
	
	//动画
	public Animator animtr;
	public int statusID;

	void Start()
	{
		//Init Anim
		animtr = GetComponent<Animator>();
		statusID = Animator.StringToHash("AnimStatus");
		status = Status.Idle;

		//Init OtherData
		offset = new Vector2(1.4f , -0.8f);
		Radius = 10f;
		type = HeroTypes.Archer;
	}
	void Update()
	{
		//循环检测队伍指令
		WaittingCommand();
		
		//循环检测可行动状态
		if (status != Status.Attack)
		{
			//非攻击状态下，寻找攻击目标
			FindingMonster();
		}
		else
		{
			//攻击状态下，检测怪物是否死亡，进行新的行为
			AttackCurMonster();
		}
		
	}
	#region Hero 行为AI函数
	void WaittingCommand()
	{
		switch (GameMode.Instance.command)
		{
			case GameMode.Command.Shoot:
				Shoot();
				break;
			case GameMode.Command.Default:
				break;
			case GameMode.Command.Inspire:
				break;
		}
	}
	void FindingMonster()
	{
		//人物位置+碰撞体偏移  作为射线发射原点，进行射线检测
		Vector2 origin = new Vector2(transform.position.x+offset.x , transform.position.y+offset.y);
		Ray2D ray = new Ray2D(origin , transform.right);
		RaycastHit2D hitInfo;
		hitInfo = Physics2D.Raycast(origin , transform.right ,Radius);

		//1.未找到可攻击目标，且射线未击中任何物品： 用于Debug 和 初始移动
		if (!hitInfo)
		{
			Debug.DrawRay(origin , transform.right * Radius , Color.green);
			Move();
		}
		else
		{
			Vector2 dir = (Vector2)hitInfo.transform.position - origin;
			Debug.DrawRay(origin , dir , Color.green);
			//2.如果检测到身前有其他人物挡住射线，进行下一步判断：
			if (hitInfo.collider.CompareTag("hero"))
			{
				print(hitInfo.transform.name);
				preHero = hitInfo.transform;
				Hero preHeroData = preHero.GetComponent<Hero>();
				//2.1 当身前人物为 远程英雄时， 
				//2.1.1 前方的人物移动则跟着移动，
				//2.1.2 前方的人物攻击，则检测身前英雄锁定的怪物是否已位于自己的攻击范围内，超过攻击范围则移动，在范围内则直接攻击
				if (preHeroData.type == HeroTypes.Archer)//2.1
				{
					if (preHeroData.status == Status.Walk)//2.1.1
					{
						Move();
					}
					if (preHeroData.status == Status.Attack)//2.1.2
					{
						if (Radius > preHeroData.curCheckedMonster.position.x - transform.position.x)
						{
							CheckAttackDistance();//防止人物站在同一地点的穿模检测
						}
						else
						{
							Move();
						}
					}
				}
				////2.2 当身前人物为 近战英雄时，
				////2.2.1 前方的人物移动则跟着移动，
				//if (preHeroData.type == HeroTypes.Melee)
				//{
				//	if (preHeroData.status == Status.Walk)//2.1.1
				//	{
				//		Move();
				//	}
				//	if (preHeroData.status == Status.Attack)//2.1.2
				//	{
				//		if (Radius > preHeroData.curCheckedMonster.position.x - transform.position.x)
				//		{
				//			CheckAttackDistance();//防止人物站在同一地点的穿模检测
				//		}
				//		else
				//		{
				//			Move();
				//		}
				//	}
				//}
				else//2.2
				{
					Move();
				}
			}
			//3.检测到前方有可攻击的怪物，锁定怪物并进行穿模检测
			if (hitInfo.collider.CompareTag("monster"))
			{
				curCheckedMonster = hitInfo.transform;
				CheckAttackDistance();
			}
		}
	}
	void AttackCurMonster()
	{
		Monster monster = curCheckedMonster.GetComponent<Monster>();
		if (monster.HP <= 0)
		{
			status = Status.Idle;
			print("re");
		}
	}
	void CheckAttackDistance()
	{
		if (preHero == null)
		{
			Attack();
			return;
		}
		if (Mathf.Abs(transform.position.x - preHero.transform.position.x) < 2)
		{
			Move();
		}
		else
		{
			Attack();
		}
	}
	#endregion

	#region Hero 基本行动函数
	protected void Attack()
	{
		//add morale
		status = Status.Attack;
		animtr.SetInteger(statusID , 3);

		//TODO :Instantiate Bullet with Physical
	}
	public void Behit(Collider collider)
	{
		status = Status.BeHit;
		animtr.SetInteger(statusID , 1);

		if (HP <= 0)
		{
			Dead();
		}
		else
		{
			if (collider.CompareTag("Monster"))
			{
				HP -= collider.gameObject.GetComponent<Monster>().touchDamage;
			}
			if (collider.CompareTag("DangerObject"))
			{
				HP -= collider.gameObject.GetComponent<DangerObject>().damage;
			}
		}
	}
	protected void Move()
	{
		status = Status.Walk;
		animtr.SetInteger(statusID , 2);

		transform.Translate(new Vector3(0.1f , 0 , transform.position.z));
	}
	public void Dead()
	{
		status = Status.Dead;
		animtr.SetInteger(statusID , 4);

		tag = "Untagged";
		Invoke("DestroySelf" , 2.0f);
	}
	public void Shoot()
	{

	}
	#endregion

	#region 其他工具函数
	void DestroySelf()
	{
		Destroy(gameObject);
	}
	#endregion
}
