using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Test_Generator : MonoBehaviour
{
    private GameObject player;
    private GameObject worldBounds;
    private GameObject skeleton;
    private GameObject fade;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        player = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        skeleton = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        fade = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/FadeCanvas");

        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(player, Vector3.zero, Quaternion.identity);
        GameObject Skeleton = Instantiate<GameObject>(skeleton, Vector3.zero, Quaternion.identity);
        Skeleton.transform.position = new Vector3(-8.25f, 0f, 0f);
        Instantiate<GameObject>(fade, Vector3.zero, Quaternion.identity);
    }
}
