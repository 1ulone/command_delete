using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Random Generation Data", menuName = "Data/ RoomRandom Dta")]
public class RoomGenerationData : ScriptableObject
{
    public int iterations = 10;
    public int walkLen = 10;
    public bool startRandomIteration = true;

    public GameObject glitchedobject, bosspoint;
    public GameObject[] item;
}
