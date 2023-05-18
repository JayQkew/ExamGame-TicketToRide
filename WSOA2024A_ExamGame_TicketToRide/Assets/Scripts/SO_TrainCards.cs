using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "New TrainCard", menuName = "TrainCard")]
public class SO_TrainCards : ScriptableObject
{
    [SerializeField] public string colour;
    [SerializeField] public bool isLocomotive;

    [SerializeField] public bool faceUp;


}
/*
[SerializeField] GameObject BostonMiami;
[SerializeField] GameObject CalgaryPhoenix;
[SerializeField] GameObject CalgarySaltLakeCity;
[SerializeField] GameObject ChicagoNewOrleans;
[SerializeField] GameObject ChicagoSantaFe;
[SerializeField] GameObject DallasNewYork;
[SerializeField] GameObject DenverElPaso;
[SerializeField] GameObject DenverPittsburgh;
[SerializeField] GameObject DuluthElPaso;
[SerializeField] GameObject DuluthHouston;
[SerializeField] GameObject HelenaLosAngeles;
[SerializeField] GameObject KansasCityHouston;
[SerializeField] GameObject LosAngelesChicago;
[SerializeField] GameObject LosAngeleseMiami;
[SerializeField] GameObject LosAngelesNewYork;
[SerializeField] GameObject MontrealAtlanta;
[SerializeField] GameObject MontrealNewOrleans;
[SerializeField] GameObject NeewYorkAtlanta;
[SerializeField] GameObject PortlandNashville;
[SerializeField] GameObject PortlandPhoenix;
[SerializeField] GameObject SanFranciscoAtlanta;
[SerializeField] GameObject SaultStMarieNashville;
[SerializeField] GameObject SaultStMarieOklahomaCity;
[SerializeField] GameObject SeattleLosAngeles;
[SerializeField] GameObject SeattleNewYork;
[SerializeField] GameObject TorontoMiami;
[SerializeField] GameObject VancouverMontreal;
[SerializeField] GameObject VancouverSantaFe;
[SerializeField] GameObject WinnipegHouston;
[SerializeField] GameObject WinnipegLittleRock;
*/