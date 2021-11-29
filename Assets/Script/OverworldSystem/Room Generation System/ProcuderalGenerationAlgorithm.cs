using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProcuderalGenerationAlgorithm
{
    public static HashSet<Vector2Int> simpleWalk(Vector2Int startPos, int len)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        var prevPos = startPos;

        for (int i = 0; i < len; i++)
        {
            var newPos = prevPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> randomWalkCorridor(Vector2Int startPos, int len)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var dir = Direction2D.GetRandomCardinalDirection();
        var CurrentPos = startPos;
        corridor.Add(CurrentPos);

        for (int i = 0; i < len; i++)
        {
            CurrentPos += dir;
            corridor.Add(CurrentPos);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomQueue.Enqueue(spaceToSplit);
        while(roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2) { SplitHorizontal(minHeight, roomQueue, room); } else 
                    if (room.size.x >= minWidth * 2) { SplitVertical(minWidth, roomQueue, room); } else 
                    if (room.size.x >= minWidth && room.size.y >= minHeight)
                    { roomList.Add(room); }
                }
                else 
                {
                    if (room.size.x >= minWidth * 2) { SplitVertical(minWidth, roomQueue, room); } else 
                    if (room.size.y >= minHeight * 2) { SplitHorizontal(minHeight, roomQueue, room); } else 
                    if (room.size.x >= minWidth && room.size.y >= minHeight) 
                    { roomList.Add(room); }
                }
            }
        }
        return roomList;
    }

    private static void SplitVertical(int minWidth, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.x, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x -xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontal(int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y -ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // UP /\
        new Vector2Int(1, 0), // RIGHT -->
        new Vector2Int(0,-1), // DOWN \/
        new Vector2Int(-1,0), // LEFT <--
    };

    public static List<Vector2Int> diagonalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(1, 1), //UPRIGHT 
        new Vector2Int(1,-1), //DOWNRIGHT
        new Vector2Int(-1,-1), //DOWNLEFT
        new Vector2Int(-1,1) //UPLEFT
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 1), //UPRIGHT
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(1,-1), //DOWNRIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWNLEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1) //UPLEFT
    };

    public static Vector2Int GetRandomCardinalDirection() 
    { return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)]; }
}
