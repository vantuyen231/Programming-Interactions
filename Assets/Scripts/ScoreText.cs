using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TMP_Text scoreRemainingText;

    [SerializeField] protected float remainingItem = 0f;

    private void Awake()
    {
        remainingItem = GameObject.FindGameObjectsWithTag("Target").Length;

        UpdateText();
    }

    private void UpdateText()
    {
        scoreRemainingText.text = remainingItem.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Target")) return;
        remainingItem--;
        Destroy(other.gameObject);
        UpdateText();
    }
}
