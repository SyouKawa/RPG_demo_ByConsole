using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public int speed;
	private Rigidbody rigid;
	private Animator animtr;
	//private UIController ui;
	//private LineRenderer line;

	private bool isJump;
	private bool beHit;
	private bool deltaComplete;
	private int HP;
	private int score;
	public Vector3 test;
	public float spawnRadius { get; private set; }

	private void Start()
	{
		//FileRead
		speed = 2;//default test
		spawnRadius = 3.0f;
		HP = 100;
		score = 0;
		deltaComplete = true;

		rigid = GetComponent<Rigidbody>();
		animtr = GetComponent<Animator>();
		//ui = GameObject.Find("UI").GetComponent<UIController>();
		//line = GetComponent<LineRenderer>();
		//transform.GetChild(5).gameObject.SetActive(false);
	}
	#region Action
	public void Stand()
	{
		animtr.SetBool("isWalking" , false);
	}
	public void Move(float value , bool isHorizonal,bool isFstView)
	{
		if (!isFstView)
		{
			animtr.SetBool("isWalking" , true);
			Vector3 delta;
			//自带转向之后，forward->minus, value也为负数，结果就反过来了。所以去掉value的符号。
			delta = transform.forward * Mathf.Abs(value) * speed;
			transform.position = Vector3.Lerp(transform.position , transform.position + delta , Time.deltaTime);
		}
	}
	public void Jump()
	{
		rigid.AddForce(new Vector3(0 , speed , 0) , ForceMode.Impulse);
		isJump = true;
	}
	public float Turn(bool isLeft)
	{
		if (isLeft)
		{
			transform.Rotate(0 , -5 , 0);
			return 5;//minus be added in InputController.
		}
		else
		{
			transform.Rotate(0 , 5 , 0);
			return 5;
		}
	}
	public void SetDirection(float dirValue,bool isHorizon)
	{
		Quaternion quater = new Quaternion();
		Vector3 targetAngle;
		Vector3 temp;
		float srcAngle;
		transform.rotation.ToAngleAxis(out srcAngle , out temp);
		float AxisAngle = InputController.Instance.inputAngle;
		if (isHorizon)
		{
			if (dirValue > 0)
			{
				targetAngle = new Vector3(0 , AxisAngle+90 , 0);
			}
			else
			{
				targetAngle = new Vector3(0 , AxisAngle - 90 , 0);
			}
		}
		else
		{
			if (dirValue > 0)
			{
				targetAngle = new Vector3(0 , AxisAngle , 0);
			}
			else
			{
				targetAngle = new Vector3(0 , AxisAngle + 180 , 0);
			}
		}
		quater.eulerAngles = targetAngle;
		//TODO:将直接转向变更为LookRotation+球面差值
		//Quaternion delta = Quaternion.LookRotation(new Vector3(targetAngle.x , 0 , targetAngle.z) - new Vector3(transform.rotation.eulerAngles.x , 0 , transform.rotation.eulerAngles.z));
		//Quaternion slerp = Quaternion.Slerp(transform.rotation , delta , 10 * Time.deltaTime);
		//if (slerp == delta)
		//{
		//	deltaComplete = true;
		//}
		//transform.rotation = slerp;
		transform.rotation = quater;
	}
	public void LeftAttack()
	{
		
	}
	public void RightAttack()
	{
		
	}
	public void BeHit(int damage)
	{
		animtr.SetTrigger("Faint");
		HP -= damage;
		//ui.RefreshHp((float)HP / (float)100);
		rigid.velocity = Vector3.zero;
		if (HP <= 0)
		{
			HP = 0;
			//Death();
		}
		else
		{
			beHit = true;
			rigid.AddForce(-Vector3.forward * 2 + Vector3.up * 2 , ForceMode.Impulse);
		}
	}
	#endregion

	#region Collider Check
	private void OnCollisionEnter(Collision collision)//TODO move to otherCollider
	{
		//if (collision.gameObject.CompareTag("Hostile"))
		//{
		//	animtr.SetTrigger("Faint");
		//}
		//if (collision.gameObject.CompareTag("Bound"))
		//{
		//	collision.gameObject.tag = "CurArea";
		//	//GameModes.Instance.UpdateArea();
		//}
		if (collision.gameObject.CompareTag("CurArea"))
		{
			isJump = false;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (!isJump && collision.gameObject.CompareTag("CurArea"))
		{
			//collision.gameObject.tag = "Bound";
		}
		//GameModes.Instance.UpdateArea();
	}

	#endregion

	#region Get/Update Values/Status
	//public void AddScore(int value)
	//{
	//	//ResetAttackCD();
	//	score += value;
	//	ui.RefreshScore(score);
	//	if (score >= GameModes.Instance.GetWinScore())
	//	{
	//		Debug.Log(GameModes.Instance);
	//		//GameModes.Instance.NotifyGameEnd(true);
	//		print("You Win.");
	//	}
	//}
	//public void Death()
	//{
	//	GameModes.Instance.NotifyGameEnd(false);
	//}
	//public void Disable()
	//{
	//	transform.GetChild(5).gameObject.SetActive(false);
	//}
	#endregion
}
