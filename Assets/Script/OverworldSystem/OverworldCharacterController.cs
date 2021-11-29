using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCharacterController : MonoBehaviour
{
    public static int step;

    [SerializeField]
    private float mspd = 2f;
    [SerializeField]    
    private Transform movePoint;
    [SerializeField]
    private LayerMask wallLayer;

    private Rigidbody2D rb;
    private Animator anim;
    int dir;
    bool isMovingHorizontal, isMovingVertical, onWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movePoint.parent = null;
    }

    void Update()
    {
        if (!LevelController.resetedlevel)
        {   
            float xx = Input.GetAxisRaw("Horizontal");
            isMovingHorizontal = Mathf.Abs(xx) > 0.5f;
            float yy = Input.GetAxisRaw("Vertical");
            isMovingVertical = Mathf.Abs(yy) > 0.5f;
            Vector2 inputt = new Vector2(xx, yy);

            if (!BattleSystem.instance.encounter)
                move(inputt);
            if (BattleSystem.instance.encounter) { rb.velocity = Vector2.zero; }
            if (!isMovingHorizontal && !isMovingVertical) { anim.speed = 0; }
        } 
    }

    bool CheckWall(Vector3 pos)
    {
        bool w = Physics2D.OverlapCircle(movePoint.position + pos, .2f, wallLayer);

        return w;
    }

    void move(Vector2 xy)
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, mspd * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f) 
        {
            if (isMovingHorizontal)
            {
                if (!CheckWall(new Vector3(xy.x, 0, 0)))
                {
                    anim.speed = 1;
                    movePoint.position += new Vector3(xy.x, 0, 0);
                }
            } 

            if (isMovingVertical)
            {
                if (!CheckWall(new Vector3(0, xy.y, 0)))
                {
                    anim.speed = 1;
                    movePoint.position += new Vector3(0, xy.y, 0);
                }
            } 
        }
    }
}
