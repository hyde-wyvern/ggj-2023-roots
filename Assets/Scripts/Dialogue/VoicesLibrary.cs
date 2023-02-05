using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Voices Library", menuName = "Scriptable Objectes/Voices Library", order = 1)]
public class VoicesLibrary : ScriptableObject
{
	public List<DialogueAudioInfoSO> voices = new List<DialogueAudioInfoSO>();
}

