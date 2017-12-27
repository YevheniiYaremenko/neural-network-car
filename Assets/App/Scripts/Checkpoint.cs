using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	public bool passed;

	void Start ()
    {
		this.passed = false;
	}

	public void SetBool(bool t)
    {
		this.passed = t;
	}
}
