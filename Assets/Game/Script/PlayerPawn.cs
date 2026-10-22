using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] Vector3 offset = new Vector3(0, 0.6f, 0); // pion posé au-dessus de la case
    [SerializeField] float moveSpeed = 5f;

    public UnityEvent onMoveStarted;
    public UnityEvent onMoveEnded;

    public int CurrentIndex { get; private set; }
    public bool IsMoving { get; private set; }

    [SerializeField] float delayBeforeMove = 1f; // délai en secondes
    
    void Start()
    {
        CurrentIndex = 0;
        transform.position = board.tiles[0].transform.position + offset;
    }

    // appelée par le dé (via UnityEvent) ou par une case
    public void MoveBy(int steps)
    {
        if (IsMoving) return;
        StartCoroutine(MoveRoutine(steps));
    }

    IEnumerator MoveRoutine(int steps)
    {
        IsMoving = true;
        onMoveStarted.Invoke();
        yield return new WaitForSeconds(delayBeforeMove);
        for (int i = 0; i < steps; i++)
        {
            CurrentIndex = board.NextIndex(CurrentIndex);
            Vector3 target = board.tiles[CurrentIndex].transform.position + offset;

            while ((transform.position - target).sqrMagnitude > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;
        }

        IsMoving = false;
        board.tiles[CurrentIndex].Land(this);   // la case réagit (peut relancer un déplacement)

        if (!IsMoving) onMoveEnded.Invoke();    // fin de tour seulement si aucun event ne l'a relancé
    }
}