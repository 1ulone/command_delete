using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlitchBehaviour : MonoBehaviour
{
    public Material glicthMat;
    public LayerMask objectMask;
    public float radiusLen;
    public bool player = false;

    bool onRadius;

    private SpriteRenderer spr;
    private Material defmat, deftmap;
    private GameObject[] tilemap;

    void Start()
    {
        tilemap = GameObject.FindGameObjectsWithTag("Tilemap");
        spr = GetComponent<SpriteRenderer>();
        defmat = spr.material;
        deftmap = tilemap[0].GetComponent<TilemapRenderer>().material;
    }//FIX THIS SHIT ASS FUCKING CODE

    void Update()
    {
        onRadius = Physics2D.OverlapCircle(transform.position, radiusLen, objectMask);

        if (onRadius)
        {
            if (player)
                for (int i = 0; i < tilemap.Length; i++)
                    tilemap[i].GetComponent<TilemapRenderer>().material = glicthMat; 
            spr.material = glicthMat;
        } else 
        if (!onRadius) {
            if (player)
                for (int i = 0; i < tilemap.Length; i++)
                    tilemap[i].GetComponent<TilemapRenderer>().material = deftmap; 
            spr.material = defmat;
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, radiusLen);
    // }
}
