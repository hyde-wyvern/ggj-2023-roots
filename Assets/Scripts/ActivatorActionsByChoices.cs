using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ActivatorActionsByChoices : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        string pokemonName = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariable("pokemon_name")).value;

        switch (pokemonName)
        {
            case "":
                spriteRenderer.color = Color.white;
                break;
            case "Charmander":
                spriteRenderer.color = Color.red;
                break;
            case "Bulbasaur":
                spriteRenderer.color = Color.green;
                break;
            case "Squirtle":
                spriteRenderer.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
