﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor : MonoBehaviour {
	
	public StatePatternArtur parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	private void OnTriggerEnter(Collider other)
	{
		parent.ChildCollisionEnter(other);
	}
}
