using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    public LayerMask objectMask;
    public float radiusLen;
    public bool doBoss = false;

    bool onRadius;
    bool bossRadius, enableDetect;

    void Start()
        => ReEnableGlitch();

    void Update()
    {
        bossRadius = Physics2D.OverlapCircle(transform.position, radiusLen, objectMask);

        if (bossRadius && enableDetect)
        {
            Collider2D bs = Physics2D.OverlapCircle(transform.position, radiusLen, objectMask);
            if (bs.GetComponent<OverworldCharacterController>())
            { BattleSystem.instance.StartBattle(doBoss, this.gameObject); }
            enableDetect = false;
        }
    }

    void OnEnable()
        => Invoke("ReEnableGlitch", 8f);
    void ReEnableGlitch()
        => enableDetect = true;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusLen);
    }
}
