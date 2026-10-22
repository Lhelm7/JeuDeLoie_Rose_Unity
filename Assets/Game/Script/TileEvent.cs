using UnityEngine;

public class TileEvent : MonoBehaviour

{
    [SerializeField] int steps = 2;

    public void Apply(PlayerPawn player) => player.MoveBy(steps);
}