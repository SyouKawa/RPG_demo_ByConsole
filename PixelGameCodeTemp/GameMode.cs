using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonTool;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
	public event EventHandler OnGameOver;
	public event EventHandler OnReStart;
	public event EventHandler OnPause;
	public Player player;
	public Fader fader;
	public Light mainDirctLight;

	//TODO:游戏暂停弹框 

	public bool pauseBtn;//暂停测试

	#region Singleton
	public static GameMode Instance { get; private set;}
	private void Awake()
	{
		Instance = this;
	}
	#endregion

	#region 画面效果
	/// <summary>
	/// 淡出
	/// </summary>
	public void FadeBlack()
	{
		StartCoroutine(fader.FadeBlack());
	}
	/// <summary>
	/// 淡入
	/// </summary>
	public void FadeWhite()
	{
		StartCoroutine(fader.FadeWhite());
	}
	/// <summary>
	/// 淡入淡出
	/// </summary>
	public void FadeInOut()
	{
		StartCoroutine(fader.FadeInOut());
	}
	#endregion

	#region 游戏内时间设置  或  游戏外环境控制
	/// <summary>
	/// 游戏暂停
	/// </summary>
	public void PauseGameEnvir()
	{
		Time.timeScale = 0;
	}
	/// <summary>
	/// 恢复游戏
	/// </summary>
	public void RecoverGameEnvir()
	{
		Time.timeScale = 1;
	}
	#endregion

	#region 时间控制

	//1.实现一个spriteAlpha逐渐1->0,同时另一个spriteAlpha0->1
	public List<GameObject> DayNodes;
	public List<SpriteRenderer> daySprt;
	public List<float> dayLightIntensity;

	public List<GameObject> NightNodes;
	public List<SpriteRenderer> nightSprt;
	public List<float> nightLightIntensity;

	public List<SpriteRenderer> GreenMask;
	public List<SpriteRenderer> BlueMask;

	public List<float> GreenAlpha;
	public List<float> BlueAlpha;

	public float perValue;//alpha每帧变化量
	public bool ChangeTimeLock;//改变时间协程锁
	public bool isChanging;//是否在改变时间

	//2.实现碰撞

	/// <summary>
	/// 获取图片
	/// </summary>
	/// <param name="包含同类型Sprite的主节点"></param>
	/// <param name="要返回的Sprite列表"></param>
	public void GetSprites(List<GameObject> Nodes,out List<SpriteRenderer> sprites)
	{
		List<SpriteRenderer> Sprt = new List<SpriteRenderer>();
		foreach (GameObject Node in Nodes)
		{
			SpriteRenderer[] tempArr=Node.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer perSprt in tempArr)
			{
				Sprt.Add(perSprt);
			}
		}
		sprites = Sprt;
	}
	/// <summary>
	/// 初始化光照贴图的Alpha强度
	/// </summary>
	/// <param name="sprites"></param>
	/// <param name="Intensity"></param>
	public void InitLightIntensity(List<SpriteRenderer> sprites,out List<float> Intensity)
	{
		Intensity = new List<float>();
		foreach (SpriteRenderer sprite in sprites)
		{
			if (sprite.gameObject.tag.Equals("Light"))
			{
				Intensity.Add(sprite.color.a);
			}
			else
			{
				break;//说明光源贴图已经到头
			}
		}
	}
	/// <summary>
	/// 初始化遮罩Alpha通道的阈值
	/// </summary>
	/// <param name="砖块的颜色遮罩"></param>
	/// <param name="遮罩的Alpha通道最大值"></param>
	public void InitMasksAlpahaValues(List<SpriteRenderer> Mask, out List<float> list)
	{
		list = new List<float>();
		foreach (SpriteRenderer sr in Mask)
		{
			list.Add(sr.color.a);
		}
	}

	/// <summary>
	/// 时间加速
	/// </summary>
	public void ChangeTime()
	{
		switch (curTime)
		{
			case TimeState.Day:
				DayToNight();
				SetNightLight();
				break;
			case TimeState.Night:
				//NightToDay();
				SetDayLight();
				break;
		}
	}

	public TimeState curTime;
	/// <summary>
	/// (协程封装)白天到黑夜
	/// </summary>
	public void DayToNight()
	{
		if (!ChangeTimeLock)
		{
			ChangeTimeLock = true;//挂锁
			StartCoroutine(DayToNight(perValue));
		}
	}
	/// <summary>
	/// （协程封装）黑夜到白天
	/// </summary>
	public void NightToDay()
	{
		if (!ChangeTimeLock)
		{
			ChangeTimeLock = true;
			StartCoroutine(NightToDay(perValue));
		}
	}

	//颜色设置
	public Color dayColor;
	public Color nightColor;
	public float dayIntensity;
	public float nightIntensity;

	public float onceTime;//一次变化时常

	/// <summary>
	/// （协程封装）夜晚->白天
	/// </summary>
	public void SetDayLight()
	{
		if (!ChangeTimeLock)
		{
			ChangeTimeLock = true;
			StartCoroutine(SetDayLight(onceTime));
		}
	}

	/// <summary>
	/// （协程）设置白天光照
	/// </summary>
	/// <param name="从黑夜到白天所经历的时间"></param>
	/// <returns></returns>
	public IEnumerator SetDayLight(float duringtime)
	{
		//1.获取变化总量
		Color deltaColor = dayColor - nightColor;
		float deltaIntensity = dayIntensity - nightIntensity;
		//2.获取根据总时间的每帧变化量
		Color perColor = deltaColor / duringtime;
		print(perColor);
		float perIntensity = deltaIntensity / duringtime;
		print(perIntensity);
		while (duringtime>0)
		{
			mainDirctLight.color += perColor;
			mainDirctLight.intensity += perIntensity;
			duringtime -= 1;
			yield return null;
		}
		curTime = TimeState.Day;
		ChangeTimeLock = false;//释放锁
		isChanging = false;
	}

	/// <summary>
	/// （协程封装）白天->夜晚
	/// </summary>
	public void SetNightLight()
	{
		if (!ChangeTimeLock)
		{
			ChangeTimeLock = true;
			StartCoroutine(SetNightLight(onceTime));
		}
	}

	/// <summary>
	/// 设置夜晚光照
	/// </summary>
	public IEnumerator SetNightLight(float duringtime)
	{
		//1.获取变化总量
		Color deltaColor = nightColor- dayColor;
		float deltaIntensity = nightIntensity - dayIntensity;
		//2.获取根据总时间的每帧变化量
		Color perColor = deltaColor / duringtime;
		print(perColor);
		float perIntensity = deltaIntensity / duringtime;
		print(perIntensity);
		while (duringtime > 0)
		{
			mainDirctLight.color += perColor;
			mainDirctLight.intensity += perIntensity;
			print(duringtime);
			duringtime -= 1;
			yield return null;
		}
		curTime = TimeState.Night;
		ChangeTimeLock = false;//释放锁
		isChanging = false;
	}

	/// <summary>
	/// （协程）白天到黑夜
	/// </summary>
	/// <param name="per"></param>
	/// <returns></returns>
	public IEnumerator DayToNight(float per)
	{
		for (float i= daySprt[0].color.a; curTime == TimeState.Day ;i-= per)
		{
			//每次必然是遍历了一遍的，所以取值只要开头的，终止判断只要最后的。

			//减弱
			for (int imask=0; imask < GreenMask.Count ; imask++ )
			{
				GreenMask[imask].color = GreenMask[imask].color - new Color(0 , 0 , 0 , per*2);
			}
			//减弱
			for (int index=0; index < daySprt.Count ; index++ )
			{
				daySprt[index].color = new Color(daySprt[index].color.r , daySprt[index].color.g , daySprt[index].color.b , i);
			}
			//加强
			for (int index = 0 ; index < nightSprt.Count ; index++)
			{
				if (nightSprt[index].gameObject.tag.Equals("Light"))
				{
					nightSprt[index].color = new Color(nightSprt[index].color.r , nightSprt[index].color.g , nightSprt[index].color.b , nightLightIntensity[index] - i);
					continue;
				}
				nightSprt[index].color = new Color(nightSprt[index].color.r , nightSprt[index].color.g , nightSprt[index].color.b , 1-i);
			}
			//加强
			for (int imask = 0 ; imask < BlueMask.Count ; imask++)
			{
				//如果Alpha的值小于完全值，则加强
				if (BlueMask[imask].color.a <= BlueAlpha[imask])
				{
					BlueMask[imask].color = BlueMask[imask].color + new Color(0 , 0 , 0 , per*2);
				}
			}

			//如果放开改变时间的按钮
			//if (!isChanging)
			//{
			//	ChangeTimeLock = false;
			//	yield break;
			//}
			if (daySprt[daySprt.Count-1].color.a <= 0)
			{
				curTime = TimeState.Night;
			}
			yield return null;
		}
		ChangeTimeLock = false;//释放锁
		isChanging = false;
	}
	/// <summary>
	/// （协程）黑夜到白天
	/// </summary>
	/// <param name="per"></param>
	/// <returns></returns>
	public IEnumerator NightToDay(float per)
	{
		for (float i = nightSprt[0].color.a ; curTime == TimeState.Night ; i -= per)
		{
			//减弱
			for (int imask = 0 ; imask < BlueMask.Count ; imask++)
			{
				BlueMask[imask].color = BlueMask[imask].color - new Color(0 , 0 , 0 , per*2);
			}
			//减弱
			for (int index = 0 ; index < nightSprt.Count ; index++)
			{
				nightSprt[index].color = new Color(nightSprt[index].color.r , nightSprt[index].color.g , nightSprt[index].color.b , i);
			}
			//加强
			for (int index = 0 ; index < daySprt.Count ; index++)
			{
				if (daySprt[index].gameObject.tag.Equals("Light"))
				{
					daySprt[index].color = new Color(daySprt[index].color.r , daySprt[index].color.g , daySprt[index].color.b , dayLightIntensity[index] - i);
					continue;
				}
				daySprt[index].color = new Color(daySprt[index].color.r , daySprt[index].color.g , daySprt[index].color.b , 1-i);
			}
			//加强
			for (int imask = 0 ; imask < GreenMask.Count ; imask++)
			{
				//如果Alpha的值小于完全值，则加强
				if (GreenMask[imask].color.a <= GreenAlpha[imask])
				{
					GreenMask[imask].color = GreenMask[imask].color + new Color(0 , 0 , 0 , per*2);
				}
			}

			//如果放开改变时间的按钮
			//if (!isChanging)
			//{
			//	ChangeTimeLock = false;
			//	yield break;
			//}
			if (nightSprt[nightSprt.Count-1].color.a <= 0)
			{
				curTime = TimeState.Day;
			}
			yield return null;
		}
		ChangeTimeLock = false;//释放锁
		isChanging = false;
	}
	#endregion

	void RegisterEmptyFunc() { }
	public void Start()
	{
		//委托注册
		OnGameOver += RegisterEmptyFunc;
		OnReStart += RegisterEmptyFunc;
		OnPause += PauseGameEnvir;

		GetSprites(DayNodes,out daySprt);
		GetSprites(NightNodes,out nightSprt);

		InitLightIntensity(daySprt , out dayLightIntensity);
		InitLightIntensity(nightSprt , out nightLightIntensity);

		InitMasksAlpahaValues(GreenMask , out GreenAlpha);
		InitMasksAlpahaValues(BlueMask ,  out BlueAlpha);
	}

	//只在有需要的时候调用一次，不用在Update中循环检测
	public void ReCheckGameStatus()
	{
		//其他脚本调用刷新：检测到玩家死亡时
		if (player.state == ChrState.Die)
		{
			//淡出
			FadeBlack();
			//GameOver时的函数指针汇总
			//TODO：死亡次数+1（成就）
			OnGameOver();
		}
		//其他脚本调用刷新：检测到玩家重生时
		if (player.state == ChrState.Resurrect)
		{
			FadeWhite();
			//重新开始，只有所有资源设置到位，玩家位于初始点都就绪时，
			//别的地方才会调用ReCheck，才能进入该分支
			OnReStart();
		}
		if (pauseBtn == true)
		{
			//FadeBlack();
			OnPause();
		}
		else
		{
			//TODO:恢复游戏
			//TODO:加入游戏的暂停框
			RecoverGameEnvir();
		}
	}


}
