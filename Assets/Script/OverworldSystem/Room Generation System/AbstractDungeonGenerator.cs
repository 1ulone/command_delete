using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    public static AbstractDungeonGenerator instance;

    [SerializeField]
    protected TilemapVisualizer tmapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    void Awake() => instance = this;

    public void GenerateDungeon()
    {
        tmapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
