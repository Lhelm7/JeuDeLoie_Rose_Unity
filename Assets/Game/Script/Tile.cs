using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public int number;
    [SerializeField] TMP_Text label; // optionnel : affiche le numéro sur la case

    public UnityEvent<PlayerPawn> onPlayerLanded;

    public void Land(PlayerPawn player) => onPlayerLanded.Invoke(player);

    public void UpdateLabel()
    {
        if (label != null) label.text = number.ToString();
    }
}