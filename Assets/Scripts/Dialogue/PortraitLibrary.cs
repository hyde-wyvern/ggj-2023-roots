using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Portrait Library", menuName = "Scriptable Objectes/Portrait Library", order = 1)]
public class PortraitLibrary : ScriptableObject
{
	public List<Portrait> portraits = new List<Portrait>();
}

[Serializable]
public struct Portrait
{
	[field: SerializeField]
	public string Id { get; private set; }

	[field: SerializeField]
	public Sprite Sprite { get; private set; }
}
