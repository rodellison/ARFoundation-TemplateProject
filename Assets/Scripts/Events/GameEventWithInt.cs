using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
	[Serializable]
	[CreateAssetMenu]
	public class GameEventWithInt : ScriptableObject
	{
		private List<GameEventWithIntListener> _listeners = new List<GameEventWithIntListener>();
	
		public void Raise(int value)
		{
			for (int i = _listeners.Count - 1; i >= 0; i--)
			{
				_listeners[i].OnEventRaised(value);
			}
		}

	
		public void RegisterListener(GameEventWithIntListener listener)
		{
			if (!_listeners.Contains(listener))
			{
				_listeners.Add(listener);
			}
			else
			{
				Debug.LogWarning("Listener re-registered to event");
			}
		}
	
		public void UnregisterListener(GameEventWithIntListener listener)
		{
			_listeners.Remove(listener);
		}
	
	}
}
