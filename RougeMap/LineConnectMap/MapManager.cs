using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public class Pos
	{
		public int x;
		public int y;

		public Pos(int _x , int _y)
		{
			x = _x;
			y = _y;
		}
	}
	public class AvailableDir
	{
		public bool top;
		public bool bottom;
		public bool left;
		public bool right;

		public AvailableDir()
		{
			top = false;
			bottom = false;
			left = false;
			right = false;
		}
	}

	public class Node
	{
		public GameObject node;
		public Pos pos;
		public Pos center;
		public bool top, bottom, left, right;
		public Node(int x,int y)
		{
			pos = new Pos(x*2 , y*2);

			node = new GameObject();
			node.name = "(" + x * 2 + "," + y * 2 + ")";
			node.transform.position = new Vector3(x * 2 , y * 2 , 10);
			center = new Pos(x * 2 + 1 , y * 2 + 1);//For Line-Connect
			node.AddComponent<SpriteRenderer>();
		}
		public void InitNodeWithGameobject()
		{
			pos = new Pos((int)node.transform.position.x , (int)node.transform.position.y);
		}

		void CheckSpawnCondition()
		{
			//if(node.)
		}
	}
	public int widthXCol;
	public int heightYRow;
	public Node[,] map;
	public bool[,] isExisted;

	//
	public int birthXCol;
	public int birthYRow;

	void Start()
	{
		map = new Node[heightYRow , widthXCol];
		//InitNode();
		CreateRandomMap();
	}
	void RandomBirth()
	{
		birthYRow = Random.Range(0 , heightYRow);
		birthXCol = Random.Range(0 , widthXCol);
		map[birthYRow , birthXCol] = new Node(birthXCol , birthYRow);
		map[birthYRow , birthXCol].node.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/RoomTemplates/TinyRoomCenter");
	}

	AvailableDir CheckSpawnCondition(Node cur)
	{
		AvailableDir res = new AvailableDir();
		int row = ((int)cur.node.transform.position.y) / 2;
		int col = ((int)cur.node.transform.position.x) / 2;

		if (row != 0)
		{
			if (map[row - 1 , col] != null) res.top=true;
		}
		if (col != 0)
		{
			if (map[row , col - 1] != null) res.bottom=true;
		}
		if (row != heightYRow - 1)
		{
			if (map[row + 1 , col] != null) res.right=true;
		}
		if (col != widthXCol - 1)
		{
			if (map[row , col + 1] != null) res.left=true;
		}

		return res;
	}
	void CreateRandomMap()
	{
		RandomBirth();
		//AvailableDir res =CheckSpawnCondition(map[birthYRow , birthXCol]);
		//if (res.top)
		//{
		//	CheckSpawnCondition(map[birthYRow , birthXCol])
		//}
		LineRenderer line=map[birthYRow , birthXCol].node.AddComponent<LineRenderer>();
		//map[1 , 1] = new Node(0,0);
		//map[1 , 1].node.AddComponent<LineRenderer>();
		line.positionCount = 3;
		line.SetPositions(new Vector3[] { new Vector3(1 , 1 , 10), new Vector3(map[birthYRow , birthXCol].node.transform.position.x + 1 , 1 , 10) , new Vector3(map[birthYRow , birthXCol].node.transform.position.x+1 , map[birthYRow , birthXCol].node.transform.position.y + 1 , 10) });
		line.startWidth = 0.1f;
	}
	//TestInitCode
	void InitNode()
	{
		for (int i = 0 ; i < map.GetLength(0) ; i++)
		{
			for (int j = 0 ; j < map.GetLength(1) ; j++)
			{
				//Init Instance&Buffer
				//map[i , j] = new Node();
				//Init Name&Transform
				map[i , j].node.name = "(" + j * 2 + "," + i * 2 + ")";
				map[i , j].node.transform.position = new Vector3(j * 2 , i * 2 , 10);
				//Init Default-Sprite
				map[i , j].node.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/RoomTemplates/TinyRoomCenter");
			}
		}
	}
}