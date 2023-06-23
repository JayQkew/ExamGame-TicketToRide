using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Route", menuName = "Route")]
public class SO_Routes : ScriptableObject
{
    [SerializeField] public int trainPieces;
    [SerializeField] public int points;
    [SerializeField] public string colour;
}
