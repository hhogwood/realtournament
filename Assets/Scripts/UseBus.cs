using UnityEngine;

public class UseBus : MonoBehaviour 
{
	public OnUse onUse;
	// OnUse this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Subscribe(OnUse _onUse)
	{
		onUse += _onUse;
	}
	
	public void UnSubscribe(OnUse _onUse)
	{
		onUse -= _onUse;
	}
	
	public void Use()
	{
		if(onUse != null)
		{
			onUse.Invoke();
		}
	}
}

public delegate void OnUse();
