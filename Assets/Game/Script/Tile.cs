using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public int number;
    [SerializeField] TMP_Text _Name; // affiche le nom de la case (si l'on veut distinguer les cases spécial par exemple) 

    public UnityEvent<PlayerPawn> onPlayerLanded; //Unity event pour le déclenchement des events liée aux tiles

    //fonction land que le joueur appel quand il a fini ses déplacements 
    public void Land(PlayerPawn player) => onPlayerLanded.Invoke(player);

    public void UpdateLabel()
    {
        if (_Name!= null) _Name.text = number.ToString();
    }
}