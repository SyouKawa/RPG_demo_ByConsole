using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform target;
    public float height;
	public float back;
	public bool isFirstView;
	private void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		height = 2f;
		back = -4f;
		isFirstView = false;
}
	void Update () {
		//transform.RotateAround(transform.position , Vector3.up , speed * Input.GetAxis("Mouse X"));
		Vector3 pos;
		if (isFirstView)
		{
			pos = target.transform.position + Vector3.up * height;
		}
		else
		{
			pos = target.transform.position + Vector3.up * height+ Vector3.forward * back;
		}
		transform.position = pos;
	}
}
