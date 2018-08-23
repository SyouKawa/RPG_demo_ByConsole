using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomManager:MonoBehaviour{
	
	#region Singleton
	private RoomManager _instance;
	public RoomManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject.Find("RoomManager").GetComponent<RoomManager>();
			}
			return _instance;
		}
	}
	#endregion

	string loadPath;
	public List<GameObject> bottom { get; private set; }
	public List<GameObject> top { get; private set; }
	public List<GameObject> left { get; private set; }
	public List<GameObject> right { get; private set; }

	private void Start()
	{
		//default
		loadPath = "Assets/Resources/Prefabs/";
		bottom = new List<GameObject>();
		top = new List<GameObject>();
		left = new List<GameObject>();
		right = new List<GameObject>();
		InitRoomArray();
	}

	private void InitRoomArray()
	{
		if (Directory.Exists(loadPath))
		{
			DirectoryInfo dirInfo = new DirectoryInfo(loadPath);
			FileInfo[] files = dirInfo.GetFiles("*" , SearchOption.AllDirectories);

			foreach (FileInfo curFile in files)
			{
				if (curFile.Name.EndsWith(".prefab"))
				{
					//Debug.Log(curFile.Name);
					string loadName = "Prefabs/" + curFile.Name.Split('.')[0];
					if (curFile.Name.Contains("T"))
					{
						top.Add(Resources.Load<GameObject>(loadName));
					}
					if (curFile.Name.Contains("B"))
					{
						bottom.Add(Resources.Load<GameObject>(loadName));
					}
					if (curFile.Name.Contains("L"))
					{
						left.Add(Resources.Load<GameObject>(loadName));
					}
					if (curFile.Name.Contains("R"))
					{
						right.Add(Resources.Load<GameObject>(loadName));
					}
				}
			}
		}
	}
}
