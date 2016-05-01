using UnityEngine;

public class InputBus : MonoBehaviour 
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
	
	public void UpdateControl(InputState _inputState)
	{
		if(ControlUpdate != null)
		{
			ControlUpdate.Invoke(_inputState);
		}
	}
}

public delegate void OnControlUpdate(InputState _inputState);
