using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitSetter : MonoBehaviour
{
	[SerializeField]
	private Image portraitHolder;

	[SerializeField]
	private PortraitLibrary library;

	public void SetPortrait(string portraitId)
	{
		Portrait targetPortrait = library.portraits.Find(portrait => portrait.Id == portraitId);

		if (targetPortrait.Id == null)
		{
			Debug.Log("Unable to find a portrait with id: " + portraitId);
			return;
		}

		portraitHolder.sprite = targetPortrait.Sprite;
	}
}
