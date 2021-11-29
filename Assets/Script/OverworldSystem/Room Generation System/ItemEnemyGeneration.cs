using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemyGeneration : MonoBehaviour
{
    private static GameObject parName;
    private static GameObject[] ts;

    public static void ClearObject()
    {
        ts = GameObject.FindGameObjectsWithTag("glitch");
        if (ts != null) 
        { for (int i = 0; i < ts.Length; i++) { DestroyImmediate(ts[i]); } } else { return; }
    } 

    public static void CreateObject(List<Vector2Int> pos, RoomGenerationData data)
    {
        parName = GameObject.Find("GlitchedPar");
        for (int i = 0; i < pos.Count; i++)
        {
            if (i < (pos.Count+1)/2) 
            { Instantiate(data.glitchedobject, new Vector3(pos[i].x, pos[i].y, 0), Quaternion.identity, parName.transform); } else
            if (i > (pos.Count+1)/2 && i < pos.Count-1)
            { Instantiate(data.item[Random.Range(0, data.item.Length)], new Vector3(pos[i].x, pos[i].y, 0), Quaternion.identity, parName.transform); } else 
            if (i == pos.Count-1)
            { Instantiate(data.bosspoint, new Vector3(pos[i].x, pos[i].y, 0), Quaternion.identity, parName.transform); }
        }
    }
}