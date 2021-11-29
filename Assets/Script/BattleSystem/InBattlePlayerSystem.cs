using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBattlePlayerSystem : MonoBehaviour
{
    
    ///GuilleUCM/ObjectShake.cs from github
    
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0;

    void Update ()
    {
        if (temp_shake_intensity > 0){
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x ,
                originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
                originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .02f,
                originRotation.w );
            temp_shake_intensity -= shake_decay;
        }
	}
	
	public void Shake()
    {
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
	}
}
