using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tiles;

    public int StepIndex(int index, int direction)
    {
        return (index + direction + tiles.Length) % tiles.Length;
    }
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