using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeColor : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ChangeTheColor()
    {
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 255f);
    }
}
