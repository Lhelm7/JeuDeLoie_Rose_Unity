using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] Vector3 offset = new Vector3(0, 0.6f, 0);
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float delayBeforeMove = 1f;     // pause entre le résultat du dé et le 1er pas
    [SerializeField] float pauseBetweenSteps = 0.2f; // pause après chaque case

    public UnityEvent onMoveStarted;
    public UnityEvent onMoveEnded;

    const string SaveKey = "PlayerIndex";

    public int CurrentIndex { get; private set; }
    public bool IsMoving { get; private set; }

    void Start()
    {
        // charge la case sauvegardée (0 si aucune sauvegarde)
        CurrentIndex = PlayerPrefs.GetInt(SaveKey, 0);
        CurrentIndex = Mathf.Clamp(CurrentIndex, 0, board.tiles.Length - 1);
        transform.position = board.tiles[CurrentIndex].transform.position + offset;
    }

    // positif = avance, négatif = recule
    public void MoveBy(int steps)
    {
        if (IsMoving || steps == 0) return;
        StartCoroutine(MoveRoutine(steps));
    }

    IEnumerator MoveRoutine(int steps)
    {
        IsMoving = true;
        onMoveStarted.Invoke();

        int direction = steps > 0 ? 1 : -1;
        int count = Mathf.Abs(steps);

        yield return new WaitForSeconds(delayBeforeMove);

        for (int i = 0; i < count; i++)
        {
            CurrentIndex = board.StepIndex(CurrentIndex, direction);
            Vector3 target = board.tiles[CurrentIndex].transform.position + offset;

            while ((transform.position - target).sqrMagnitude > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;

            Save();
            yield return new WaitForSeconds(pauseBetweenSteps);
        }

        IsMoving = false;
        board.tiles[CurrentIndex].Land(this);

        if (!IsMoving) onMoveEnded.Invoke();
    }

    void Save()
    {
        PlayerPrefs.SetInt(SaveKey, CurrentIndex);
        PlayerPrefs.Save();
    }

    // à brancher sur un bouton "Nouvelle partie"
    public void ResetProgress()
    {
        if (IsMoving) return;
        PlayerPrefs.DeleteKey(SaveKey);
        CurrentIndex = 0;
        transform.position = board.tiles[0].transform.position + offset;
    }
}