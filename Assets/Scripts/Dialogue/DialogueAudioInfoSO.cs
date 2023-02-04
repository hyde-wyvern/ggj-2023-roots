using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Audio Info", menuName = "Scriptable Objectes/Dialogue Audio Info SO", order = 1)]
public class DialogueAudioInfoSO : ScriptableObject
{
    public string id;
    public AudioClip[] dialogueTypingSoundClips;
    [Range(1, 5)] public int frequencyLevel = 2;
    [Range(-3, 3)] public float minPitch = 0.5f;
    [Range(-3, 3)] public float maxPitch = 3f;
    public bool stopAudioSource;
}
