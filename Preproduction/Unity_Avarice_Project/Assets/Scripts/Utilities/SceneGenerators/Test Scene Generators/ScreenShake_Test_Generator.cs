using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake_Test_Generator : MonoBehaviour
{
    private GameObject playerPrefab;
    private GameObject worldBounds;
    private GameObject swordPrefab;
    private GameObject dummyPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        swordPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        GameObject player = Instantiate<GameObject>(playerPrefab, Vector3.zero, Quaternion.identity);

        GameObject sword = Instantiate<GameObject>(swordPrefab, player.transform);
        sword.transform.localPosition = new Vector2(1.42f, 0.76f);
        GameObject dummy = Instantiate<GameObject>(dummyPrefab, new Vector2(3.61f, -0.15f), Quaternion.identity);

        dummy.tag = "Enemy";
        dummy.transform.RotateAround(dummy.transform.position, Vector3.forward, 270);
        SpriteRenderer sr = dummy.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Enemies/FleshySkeleton/spr_EnemyFleshySkeleton0");

        BoxCollider2D col = dummy.AddComponent<BoxCollider2D>();
        col.offset = new Vector2(0.05416262f, -0.0464251f);
        col.size = new Vector2(1.708872f, 1.414846f);

        dummy.AddComponent<Rigidbody2D>();
        dummy.GetComponent<Rigidbody2D>().gravityScale = 0;
        dummy.GetComponent<Rigidbody2D>().mass = 1000000;
        dummy.GetComponent<Rigidbody2D>().freezeRotation = true;

        Camera.main.gameObject.AddComponent<ScreenShake>();
    }
}
