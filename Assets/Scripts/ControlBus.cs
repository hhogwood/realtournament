using UnityEngine;

public class ControlBus : MonoBehaviour 
{
	private OnControlUpdate ControlUpdate;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Subscribe(OnControlUpdate _controlUpdate)
	{
		ControlUpdate += _controlUpdate;
	}
	
	public void UnSubscribe(OnControlUpdate _controlUpdate)
	{
		ControlUpdate -= _controlUpdate;
	}
	
	public void UpdateControl(ControlState _controlState)
	{
		if(ControlUpdate != null)
		{
			ControlUpdate.Invoke(_controlState);
		}
	}
}

public delegate void OnControlUpdate(ControlState _controlState);

[System.Serializable]
public class ControlState
{
    public Vector3 movement;
    public Vector3 rotation;
}