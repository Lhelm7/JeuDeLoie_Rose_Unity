using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;

    public UnityEvent<int> onRolled;

    // appelée par le bouton
    public void Roll()
    {
        int result = Random.Range(1, 7); // 1 à 6
        resultText.text = result.ToString();
        onRolled.Invoke(result);
    }
}