using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDisableOnEndAnim : MonoBehaviour
{
    public void onEndDestroy() => Destroy(gameObject);
    public void onEndDisable() => gameObject.SetActive(false);
}
