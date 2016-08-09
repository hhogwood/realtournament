using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Seek : Behaviour 
{
	public IControllable target;
	private ControlState State;

	List<SeekTarget> targets = new List<SeekTarget>();

	public override void Start () 
	{
		if(target == null)
		{
			target = GetComponent<IControllable>();
		}
	}
	
	public override void Update () 
	{
		targets.Sort();

		State.movement = (transform.position - targets[0].Position).normalized;
	}

	public void UpdateControlState(ControlState state)
	{
		
	}
}

public class SeekTarget : IComparer<SeekTarget>
{
	public Vector3 Position;
	public int Priority;

    public int Compare(SeekTarget x, SeekTarget y)
    {
        return x.Priority.CompareTo(y.Priority);
    }
}
