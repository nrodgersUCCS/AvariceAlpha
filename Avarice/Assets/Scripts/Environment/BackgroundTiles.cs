using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiles : MonoBehaviour
{
    // Array of prefabs for the tiles
    public GameObject[] tile;

    // Start position of the tiles
    public Vector3 tileStartPos;

    // Tile spacing
    Vector2 tileSpacing;

    // Width of the grid
    public int gridWidth;

    // Height of the grid
    public int gridHeight;

    //public void GenerateBackground(float bottomLeftX, float bottomLeftY)
    void Start()
    {
        //tileStartPos = new Vector3(bottomLeftX, bottomLeftY, 0.0f);

        // Get the exact size of our tiles
        tileSpacing = tile[0].GetComponent<Renderer>().bounds.size;

        // Loop the number of rows height
        for (int i = 0; i < gridHeight; i++)
        {
            // Loop the number of columns width
            for (int j = 0; j < gridHeight; j++)
            {
                // Grab a random number between 0 and however many tiles there are
                int randomTile = Random.Range(0, tile.Length);

                // Spawn new game object based on the tile chosen using a random number,
                // starting at our start pos.x plus the tile spacing.x width times the count of the grid width
                // by our start pos.y plus the tile spacing.y height times the count of the grid height
                // using quaternion.identity so that there is no rotation in the vwector3
                GameObject go = Instantiate(tile[randomTile], new Vector3(tileStartPos.x + (j * tileSpacing.x), tileStartPos.y + (i * tileSpacing.y)), Quaternion.identity) as GameObject;

                // Add all the game objects as a child of BGTiles
                go.transform.parent = GameObject.Find("BGTiles").transform;
            }
        }
    }
}
