using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// A class for procedurally generating levels
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    /// An enumeration of exit types from cells
    /// </summary>
    private enum ExitType { NORTH, SOUTH, EAST, WEST }

    /// <summary>
    /// The combined enums of exit types
    /// </summary>
    private enum ExitCombos
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8,
        NS = N + S,
        NE = N + E,
        NW = N + W,
        SE = S + E,
        SW = S + W,
        EW = E + W,
        NSE = NS + E,
        NSW = NS + W,
        NEW = NE + W,
        SEW = SE + W,
        NSEW = NSE + W,
        B = 16,                             //Boss
        BRN = B + N,
        BRS = B + S,
        BRE = B + E,
        BRW = B + W
    }

    /// <summary>
    /// A cell that holds one module
    /// </summary>
    private class Cell
    {
        public int row;                     // The cell's row in the matrix
        public int column;                  // This cell's column in the matrix
        public List<Cell> neighbors;        // All neighboring cells
        public bool visited;                // Whether the cells has been visited yet during generation
        public List<ExitType> exits;        // The list of exits from the cell
        public bool isBossRoom = false;     // A bool checking if the cell is the boss room


        // Constructor
        public Cell(int row, int col)
        {
            this.row = row;
            this.column = col;
            this.visited = false;
            this.neighbors = new List<Cell>();
            this.exits = new List<ExitType>();
        }

        /// <summary>
        /// Takes the list of ExitTypes and converts it into a value of exitCombos
        /// </summary>
        /// <returns></returns>
        public ExitCombos ExitCombo()
        {
            ExitCombos exitCombo = 0;

            if (exits.Contains(ExitType.NORTH))
                exitCombo += (int)ExitCombos.N;
            if (exits.Contains(ExitType.SOUTH))
                exitCombo += (int)ExitCombos.S;
            if (exits.Contains(ExitType.EAST))
                exitCombo += (int)ExitCombos.E;
            if (exits.Contains(ExitType.WEST))
                exitCombo += (int)ExitCombos.W;
            if (isBossRoom)
                exitCombo += (int)ExitCombos.B;

            return exitCombo;
        }
    }

    [SerializeField]
    Vector2Int gridSize;                                            // The size of the matrix of cells
    Dictionary<ExitCombos, List<GameObject>> modulePrefabs;         // The dictionary of module prefabs
    private int maxCount;                                           // Max number of iterations in recursion
    private Cell[,] matrix;                                         // The matrix of cells on the grid
    private bool useBackupStartRoom;                                // Whether the backup start cell will be used
    private GameObject startModule = null;                          // The spawn module
    [SerializeField]
    private LayerMask enemyLayer = 0;                               // The layer enemies are on
    [SerializeField]
    private GameObject backgroundPrefab = null;                     // Prefab for the background tiles
    private Vector2Int backgroundOffset = new Vector2Int(-17, -10); // Offset position for the background tiles

    // Constants
    private int X_OFFSET;                                           // Width of a module
    private int Y_OFFSET;                                           // Height of a module

    // Values for the modulePrefab dictionary
    private List<GameObject> V_NORTH, V_SOUTH, V_EAST, V_WEST,      //Lists for cells with one exit
                           V_NS, V_NE, V_NW, V_SE, V_SW, V_EW,      //Lists for cells with two exits
                           V_NSE, V_NEW, V_NSW, V_SEW,              //Lists for cells with three exits
                           V_NSEW,                                  //List for cells with four exits
                           V_BRN, V_BRS, V_BRE, V_BRW;              //List for cells that are boss rooms (All have just one exit)

    // List of all the rooms with just one exit
    private List<Cell> potentialBossRooms;

    /// <summary>
    /// Called while loading scene
    /// </summary>
    private void Awake()
    {
        //Setting up the constants
        MapValues container = (MapValues)Constants.Values(ContainerType.MAP);
        X_OFFSET = container.ModuleXOffset;
        Y_OFFSET = container.ModuleYOffset;
        maxCount = Mathf.Max(gridSize.x, gridSize.y) * 2;
        useBackupStartRoom = false;
        
        //Initalize the dictionary of modules
        SetUpDictionary();

        // Create cell matrix
        matrix = CreateGraph();

        // Create cell exits starting with a random cell
        Cell startCell = matrix[Random.Range(0, gridSize.y), Random.Range(0, gridSize.x)];
        CreateExits(startCell, 0);
        
        //This guy is used in the event the startCell is picked for the boss room
        Cell backupStartCell = matrix[Random.Range(0, gridSize.y), Random.Range(0, gridSize.x)];

        //Make sure the backup isnt the same as the starting one
        while (backupStartCell == startCell)
            backupStartCell = matrix[Random.Range(0, gridSize.y), Random.Range(0, gridSize.x)];

        // Create the boss room
        FindBossRoom(startCell);

        // Determine which start room we need to use
        Cell startRoomToUse = useBackupStartRoom ? backupStartCell : startCell;

        // Instantiate modules
        for (int row = 0; row < gridSize.y; row++)
        {
            for (int col = 0; col < gridSize.x; col++)
            {
                if (!modulePrefabs.ContainsKey(matrix[row, col].ExitCombo()))
                {
                    Debug.Log(matrix[row, col].ExitCombo());
                }
                else
                {
                    List<GameObject> potentialPrefabs = modulePrefabs[matrix[row, col].ExitCombo()];
                    GameObject module = potentialPrefabs[Random.Range(0, potentialPrefabs.Count)];

                    //Instantiate the module at the right position relative to the start module
                    GameObject moduleInstance = Instantiate(module, new Vector2((matrix[row, col].column - startRoomToUse.column) * X_OFFSET, (startRoomToUse.row - matrix[row, col].row) * Y_OFFSET), Quaternion.identity);

                    // Set start module
                    if (row == startRoomToUse.row && col == startRoomToUse.column)
                        startModule = moduleInstance;
                }
            }
        }

        // Instantiate player in the scene
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = Instantiate(player, startModule.GetComponent<ModuleProperties>().PlayerStartPosition, Quaternion.identity);

        // Remove any enemies that are in the start module
        DestroyNearbyEnemies(startModule, playerInstance);

        // Get center of the map
        Vector2 center = LevelCenter(startRoomToUse);

        // Instantiate background tile texture
        GameObject backgroundTiles = Instantiate(backgroundPrefab);
        backgroundTiles.transform.position = BottomLeftCorner(startRoomToUse) + backgroundOffset;

        // Move minimap camera to map center
        GameObject.Find("MinmapCamera").gameObject.transform.position = new Vector3(center.x, center.y, -10f);
    }

    private void DestroyNearbyEnemies(GameObject startModule, GameObject player)
    {
        // Destroy enemies in the starting module
        Enemy[] enemiesInModule = startModule.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemiesInModule)
        {
            Destroy(enemy.gameObject);
        }

        // Destroy enemies near the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, X_OFFSET / 2f, enemyLayer);
        foreach (Collider2D col in colliders)
        {
            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// Creates a graph of cells with eighbors
    /// </summary>
    private Cell[,] CreateGraph()
    {
        // Create a matrix of Cells
        Cell[,] cells = new Cell[gridSize.y, gridSize.x];
        for (int row = 0; row < gridSize.y; row++)
            for (int col = 0; col < gridSize.x; col++)
                cells[row, col] = new Cell(row, col);

        // Organize into a graph
        for (int row = 0; row < gridSize.y; row++)
        {
            for (int col = 0; col < gridSize.x; col++)
            {
                // Add north neighbor
                if (row > 0)
                {
                    cells[row, col].neighbors.Add(cells[row - 1, col]);
                }

                // Add south neighbor
                if (row < gridSize.y - 1)
                {
                    cells[row, col].neighbors.Add(cells[row + 1, col]);
                }

                // Add east neighbor
                if (col < gridSize.x - 1)
                {
                    cells[row, col].neighbors.Add(cells[row, col + 1]);
                }

                // Add west neighbor
                if (col > 0)
                {
                    cells[row, col].neighbors.Add(cells[row, col - 1]);
                }
            }
        }

        return cells;
    }

    /// <summary>
    /// Recursively creates exits in a graph of cells
    /// </summary>
    /// <param name="currentCell">Starting cell</param>
    private void CreateExits(Cell currentCell, int count)
    {
        // If max count has not been reached
        if (count < maxCount)
        {
            // Set visited to true
            currentCell.visited = true;

            // While there are remaining neighbors
            while (currentCell.neighbors.Count > 0)
            {
                // Pick a random neighbor
                Cell neighbor = currentCell.neighbors[Random.Range(0, currentCell.neighbors.Count)];

                // If the neighbor has not been visited
                if (!neighbor.visited)
                {
                    // Remove the wall between these cells
                    if (neighbor.column > currentCell.column)
                    {
                        currentCell.exits.Add(ExitType.EAST);
                        neighbor.exits.Add(ExitType.WEST);
                    }
                    else if (neighbor.column < currentCell.column)
                    {
                        currentCell.exits.Add(ExitType.WEST);
                        neighbor.exits.Add(ExitType.EAST);
                    }
                    else if (neighbor.row > currentCell.row)
                    {
                        currentCell.exits.Add(ExitType.SOUTH);
                        neighbor.exits.Add(ExitType.NORTH);
                    }
                    else if (neighbor.row < currentCell.row)
                    {
                        currentCell.exits.Add(ExitType.NORTH);
                        neighbor.exits.Add(ExitType.SOUTH);
                    }

                    // Recurse into neighbor cell
                    CreateExits(neighbor, ++count);
                }

                // Remove neighbor
                currentCell.neighbors.Remove(neighbor);
            }
        }

        // Check for any remaining empty cells
        if (count == 0)
        {
            for (int row = 0; row < gridSize.y; row++)
            {
                for (int col = 0; col < gridSize.x; col++)
                {
                    // If a cell has not been visited
                    Cell cell = matrix[row, col];
                    if (!cell.visited)
                    {
                        // Restart recursion from nearest visited cell
                        CreateExits(NearestVisitedCell(cell), 0);
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method goes through the matrix to find all the potential boss rooms then picks one at random.
    /// </summary>
    /// <param name="startCell"></param>
    private void FindBossRoom(Cell startCell)
    {
        // Find all the rooms with just one exit
        for (int row = 0; row < gridSize.y; row++)
        {
            for (int col = 0; col < gridSize.x; col++)
            {
                //If we find one, add it to the list of potential boss rooms
                Cell cell = matrix[row, col];
                if (cell.exits.Count == 1)
                    potentialBossRooms.Add(cell);
            }
        }

        //Check if we have any rooms with 1 exit. If we dont make one
        if (potentialBossRooms.Count < 1)
        {
            //Pick a cell and get its exits, then pick an exit to keep
            Cell cellToChange = matrix[Random.Range(0, gridSize.x), Random.Range(0, gridSize.y)];
            ExitType exitToKeep = cellToChange.exits[Random.Range(0, cellToChange.exits.Count)];

            //Go through the neighbors to update their exits
            for (int i = 0; i < cellToChange.exits.Count; i++)
            {
                ExitType anExit = cellToChange.exits[i];
                if (anExit != exitToKeep)
                {
                    switch (anExit)
                    {
                        case ExitType.NORTH:
                            matrix[cellToChange.row - 1, cellToChange.column].exits.Remove(ExitType.SOUTH);
                            break;
                        case ExitType.SOUTH:
                            matrix[cellToChange.row + 1, cellToChange.column].exits.Remove(ExitType.NORTH);
                            break;
                        case ExitType.EAST:
                            matrix[cellToChange.row, cellToChange.column + 1].exits.Remove(ExitType.WEST);
                            break;
                        case ExitType.WEST:
                            matrix[cellToChange.row, cellToChange.column - 1].exits.Remove(ExitType.EAST);
                            break;
                    }
                }
            }

            //Now remove the exits from the cell we dont want
            for (int i = 0; i < cellToChange.exits.Count; i++)
            {
                ExitType anExit = cellToChange.exits[i];
                if (anExit != exitToKeep)
                    cellToChange.exits.Remove(anExit);
            }

            //Add this cell into potentialBossRooms
            potentialBossRooms.Add(cellToChange);
        }

        // Pick one of the rooms at random and make it the boss room 
        Cell theBossRoom = potentialBossRooms[Random.Range(0, potentialBossRooms.Count)];
        theBossRoom.isBossRoom = true;

        //If we pick the startCell, then switch the room
        if (theBossRoom.row == startCell.row && theBossRoom.column == startCell.column)
            useBackupStartRoom = true;
    }

    /// <summary>
    /// Recursively finds the nearest cell that has already been visited
    /// </summary>
    /// <param name="currentCell">Starting cell</param>
    /// <returns></returns>
    private Cell NearestVisitedCell(Cell currentCell)
    {
        Cell nearestVisitedCell = null;

        // Add start cell to visited and neighbors to toVisit
        List<Cell> visited = new List<Cell>();
        List<Cell> toVisit = new List<Cell>();
        visited.Add(currentCell);
        toVisit.AddRange(currentCell.neighbors);

        // While there are cells to visit
        int matrixCount = gridSize.x * gridSize.y;
        while (visited.Count < matrixCount && toVisit.Count > 0)
        {
            // Update lists
            currentCell = toVisit[0];
            toVisit.RemoveAt(0);
            visited.Add(currentCell);

            // If the cell has been visited, return it
            if (currentCell.visited)
                return currentCell;

            // Otherwise, keep searching
            else
            {
                foreach (Cell neighbor in currentCell.neighbors)
                    if (!visited.Contains(neighbor))
                        toVisit.Add(neighbor);
            }
        }

        return nearestVisitedCell;
    }

    /// <summary>
    /// This is a helper method to set up the lists needed to initialize the module dictionary and to initialize the module dictionary.
    /// </summary>
    private void SetUpDictionary()
    {
        // VALUES
        // One Exit
        V_NORTH = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/N-1"),
                                         Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/N-2") };
        V_SOUTH = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/S-1"),
                                         Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/S-2") };
        V_EAST = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/E-1"),
                                        Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/E-2") };
        V_WEST = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/W-1"),
                                        Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/1DoorModules/W-2") };

        // Two Exits
        V_NS = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NS-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NS-2") };
        V_NE = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NE-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NE-2"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NE-3")};
        V_NW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NW-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/NW-2") };
        V_SE = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SE-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SE-2"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SE-3")};
        V_SW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SW-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SW-2"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/SW-3")};
        V_EW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/EW-1"),
                                      Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/2DoorModules/EW-2") };

        // Three Exits
        V_NSE = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NSE-1"),
                                       Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NSE-2") };
        V_NEW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NEW-1"),
                                       Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NEW-2") };
        V_NSW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NSW-1"),
                                       Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/NSW-2") };
        V_SEW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/SEW-1"),
                                       Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/3DoorModules/SEW-2") };

        // Four Exits
        V_NSEW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/4DoorModules/NSEW-1"),
                                        Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/4DoorModules/NSEW-2") };

        // Boss Rooms
        V_BRN = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/BossModules/BRN-1") };

        V_BRS = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/BossModules/BRS-1") };

        V_BRE = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/BossModules/BRE-1") };

        V_BRW = new List<GameObject> { Resources.Load<GameObject>("Prefabs/Environment/Terrain/Modules/BossModules/BRW-1") };

        // Initialize the dictionary
        modulePrefabs = new Dictionary<ExitCombos, List<GameObject>>();
        modulePrefabs.Add(ExitCombos.N, V_NORTH);
        modulePrefabs.Add(ExitCombos.S, V_SOUTH);
        modulePrefabs.Add(ExitCombos.E, V_EAST);
        modulePrefabs.Add(ExitCombos.W, V_WEST);
        modulePrefabs.Add(ExitCombos.NS, V_NS);
        modulePrefabs.Add(ExitCombos.NE, V_NE);
        modulePrefabs.Add(ExitCombos.NW, V_NW);
        modulePrefabs.Add(ExitCombos.SE, V_SE);
        modulePrefabs.Add(ExitCombos.SW, V_SW);
        modulePrefabs.Add(ExitCombos.EW, V_EW);
        modulePrefabs.Add(ExitCombos.NSE, V_NSE);
        modulePrefabs.Add(ExitCombos.NEW, V_NEW);
        modulePrefabs.Add(ExitCombos.NSW, V_NSW);
        modulePrefabs.Add(ExitCombos.SEW, V_SEW);
        modulePrefabs.Add(ExitCombos.NSEW, V_NSEW);
        modulePrefabs.Add(ExitCombos.BRN, V_BRN);
        modulePrefabs.Add(ExitCombos.BRS, V_BRS);
        modulePrefabs.Add(ExitCombos.BRE, V_BRE);
        modulePrefabs.Add(ExitCombos.BRW, V_BRW);

        // Set up potentialBossRooms list
        potentialBossRooms = new List<Cell>();
    }

    /// <summary>
    /// Returns the center point of the generated level
    /// </summary>
    /// <param name="startCell">The cell the player spawns in (module located at 0,0)</param>
    /// <returns>Center of the generated level</returns>
    private Vector2 LevelCenter(Cell startCell)
    {
        // Get the bottom left corner of the map
        Vector2 bottomLeftCorner = BottomLeftCorner(startCell);

        // Add half of the map's width and height
        float halfWidth = (gridSize.x * X_OFFSET) / 2f;
        float halfHeight = (gridSize.y * Y_OFFSET) / 2f;
        return bottomLeftCorner + new Vector2(halfWidth, halfHeight);
    }

    /// <summary>
    /// Returns the bottom left point of the generated level
    /// </summary>
    /// <param name="startCell">The cell the player spawns in (module located at 0,0)</param>
    /// <returns>Bottom left corner of the generated level</returns>
    private Vector2 BottomLeftCorner(Cell startCell)
    {
        float leftBound = startCell.column * -X_OFFSET;
        float bottomBound = (gridSize.y - 1 - startCell.row) * -Y_OFFSET;
        return new Vector2(leftBound, bottomBound);
    }
}
