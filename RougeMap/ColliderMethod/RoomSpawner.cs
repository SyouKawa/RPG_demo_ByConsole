using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int openingDirection;
	//1->need bottom door
	//2->need top door
	//3->need left door
	//4->need right door
	private RoomManager manager;
	private Random random;
	public bool isSpawned;

	private void Start()
	{
		isSpawned = false;
		random = new Random();
		manager = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomManager>();
		Invoke("Spawn" , 0.1f);
	}
	private void TestFuncDelay() { }

	private void Spawn()
	{
		if (!isSpawned)
		{
			int index = -1;//save random index.
			switch (openingDirection)
			{
				case 1://need bottom-door
					index = Random.Range(0 , manager.bottom.Count);
					Instantiate(manager.bottom[index] , transform.position , manager.bottom[index].transform.rotation);
					break;
				case 2://need top-door
					index = Random.Range(0 , manager.top.Count);
					Instantiate(manager.top[index] , transform.position , manager.top[index].transform.rotation);
					break;
				case 3://need left-door
					index = Random.Range(0 , manager.left.Count);
					Instantiate(manager.left[index] , transform.position , manager.left [index].transform.rotation);
					break;
				case 4://need right-door
					index = Random.Range(0 , manager.right .Count);
					Instantiate(manager.right [index] , transform.position , manager.right [index].transform.rotation);
					break;
			}
		}
		isSpawned = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("SpawnPoint"))
		{
			if (collision.GetComponent<RoomSpawner>().isSpawned == true && isSpawned == false)
			{//face a already spawned.
				isSpawned = true;
				//Destroy(gameObject);
				//Destroy(collision.gameObject);
			}
			if (collision.GetComponent<RoomSpawner>().isSpawned == false && isSpawned == false)
			{
				//Spawn();
				isSpawned = true;
			}
		}
	}
}
