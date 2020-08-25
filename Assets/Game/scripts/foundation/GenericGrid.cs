using UnityEngine;

public abstract class GenericGrid<T> : MonoBehaviour
{
    public Vector2Int gridSize;
    public T defaultNode;

    protected T[][] grid;

    public virtual void Init()
    {
        // allocate grid
        grid = new T[gridSize.x][];

        // Go through all the cell arrays...
        for (int i = 0; i < grid.Length; i++)
        {
            // ... and set each array is the correct dim.
            grid[i] = new T[gridSize.y];
        }
    }

    public virtual void Add(int i, int j, T node)
    {
        // within array bounds
        if (i >= 0 && j >=0 && i < gridSize.x && j < gridSize.y)
            grid[i][j] = node;
    }

    public virtual void Remove(T node)
    {
    }

    public abstract T Evaluate(Vector2 pos);
}
