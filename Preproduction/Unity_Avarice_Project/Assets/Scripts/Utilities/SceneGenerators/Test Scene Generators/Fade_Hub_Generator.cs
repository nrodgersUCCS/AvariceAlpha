using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Hub_Generator : MonoBehaviour
{
    private GameObject player;
    private GameObject worldBounds;
    private GameObject fade;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        player = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        fade = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/FadeCanvas");

        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(player, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(fade, Vector3.zero, Quaternion.identity);
    }
}
