using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	#region Singleton
	public static InputController Instance { get; private set; }
	private void Awake()
	{
		Instance = this;
	}
	#endregion
	public GameObject camera;
	private Player player;
	public float inputAngle;
	public float preAngle;
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		camera = Camera.main.gameObject;

		inputAngle = 0f;
		preAngle = inputAngle;
	}
	
	void FixedUpdate ()
	{
		InputCheck();
	}
	private void InputCheck()
	{
		//Move
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool viewMode = camera.GetComponent<FollowCamera>().isFirstView;
		if (h < 0.000001 && v < 0.000001)
		{
			player.Stand();
		}
		if (h != 0)
		{
			player.Move(h , true, viewMode);
			//TODO SetDir 变更为Time.deltaTime的渐变效果
			player.SetDirection(h,true);
		}
		if (v != 0)
		{
			player.Move(v , false, viewMode);
			player.SetDirection(v , false);
		}
		//Jump
		if (Input.GetKeyDown(KeyCode.Space))
		{
			player.Jump();
		}
		//Turn
		if (Input.GetKey(KeyCode.Q))
		{
			inputAngle -= player.Turn(true);
		}
		if (Input.GetKey(KeyCode.E))
		{
			inputAngle += player.Turn(false);
		}
		if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
		{//弹起时更新坐标系：将人物的朝向定为新坐标系的delta量，在（0,0,0）坐标上变换得到新的坐标系
			if (inputAngle != preAngle)//有角度变换（输入）请求
			{
				try
				{
					float deltaAngle;
					Vector3 temp;//dir
					//拿到玩家当前想要的朝向，将其变为新的input变换量，待SetDir调用
					player.transform.rotation.ToAngleAxis(out deltaAngle , out temp);
					print(temp);//让角度带上符号
					if (temp.x != 0f) deltaAngle *= temp.x;
					if (temp.y != 0f) deltaAngle *= temp.y;
					if (temp.z != 0f) deltaAngle *= temp.z;
					inputAngle = deltaAngle;
					print(inputAngle);
				}
				catch
				{
					//防止恰好转到360或0的无方向情况
					inputAngle = 0f;
					Vector3 temp = transform.forward;
				}
			}
			preAngle = inputAngle;
		}
		//Attack
		if (Input.GetMouseButtonDown(0))
		{
			player.LeftAttack();
		}
		if (Input.GetMouseButtonDown(1))
		{
			player.RightAttack();
		}
		//ChangeCameraMode
		if (Input.GetMouseButtonDown(2))
		{
			bool preValue=camera.GetComponent<FollowCamera>().isFirstView;
			camera.GetComponent<FollowCamera>().isFirstView = !preValue;
		}
	}
}
