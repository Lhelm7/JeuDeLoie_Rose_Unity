using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public int number;
    [SerializeField] TMP_Text _Name; // affiche le nom de la case (ex si est une case spécial) 

    public UnityEvent<PlayerPawn> onPlayerLanded;

    public void Land(PlayerPawn player) => onPlayerLanded.Invoke(player);

    public void UpdateLabel()
    {
        if (_Name!= null) _Name.text = number.ToString();
    }
}