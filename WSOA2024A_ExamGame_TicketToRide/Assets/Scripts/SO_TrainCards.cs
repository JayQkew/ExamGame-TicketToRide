using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "New TrainCard", menuName = "TrainCard")]
public class SO_TrainCards : ScriptableObject
{
    [SerializeField] public string colour;
}
