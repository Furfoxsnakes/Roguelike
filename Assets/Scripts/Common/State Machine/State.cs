using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour 
{
	public virtual void Enter ()
	{
		enabled = true;
		AddListeners();
	}
	
	public virtual void Exit ()
	{
		RemoveListeners();
		enabled = false;
	}

	protected virtual void OnDestroy ()
	{
		RemoveListeners();
	}

	protected virtual void AddListeners ()
	{

	}
	
	protected virtual void RemoveListeners ()
	{

	}
}