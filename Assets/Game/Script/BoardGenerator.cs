using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tiles;

    public int NextIndex(int index) => (index + 1) % tiles.Length; // le plateau boucle

    // clic droit sur le composant → "Remplir depuis les enfants"
    [ContextMenu("Remplir depuis les enfants")]
    void FillFromChildren()
    {
        tiles = GetComponentsInChildren<Tile>();
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].number = i;
            tiles[i].name = "Tile_" + i;
            tiles[i].UpdateLabel();
        }
    }
}