using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Card Components")]
    public Image cardImage;
    public Button cardButton;

    [Header("Card Data")]
    public Sprite cardBack; // Gambar belakang kartu
    public Sprite cardFace; // Gambar depan kartu

    [Header("Effect")]
    public float flipDuration = 0.15f;   // waktu per separuh flip

    public bool isMatched = false;
    private bool isFaceUp = false;
    private bool isAnimating = false;
    private Coroutine flipRoutine;

    private MemoryPuzzleManager puzzleManager;

    void Awake()
    {
        cardImage = GetComponent<Image>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(OnCardClicked);
    }

    public void SetupCard(Sprite faceSprite, MemoryPuzzleManager manager)
    {
        cardFace = faceSprite;
        puzzleManager = manager;
        isMatched = false;
        isFaceUp = false;
        isAnimating = false;
        transform.localScale = Vector3.one;
        cardImage.color = Color.white;
        cardImage.sprite = cardBack;   // langsung, tanpa animasi
    }

    public void OnCardClicked()
    {
        if (isFaceUp || isMatched || isAnimating || !puzzleManager.CanReveal()) return;

        OpenCard();
        puzzleManager.CardRevealed(this);
    }

    public void OpenCard()
    {
        isFaceUp = true;
        StartFlip(cardFace);
    }

    public void CloseCard()
    {
        isFaceUp = false;
        StartFlip(cardBack);
    }

    public void SetMatched()
    {
        isMatched = true;
        StartCoroutine(MatchEffect());
    }

    private void StartFlip(Sprite target)
    {
        if (flipRoutine != null) StopCoroutine(flipRoutine);
        flipRoutine = StartCoroutine(Flip(target));
    }

    private IEnumerator Flip(Sprite target)
    {
        isAnimating = true;

        // tutup separuh: scale X 1 -> 0
        for (float t = 0f; t < flipDuration; t += Time.deltaTime)
        {
            SetScaleX(1f - t / flipDuration);
            yield return null;
        }
        SetScaleX(0f);
        cardImage.sprite = target;

        // buka separuh: scale X 0 -> 1
        for (float t = 0f; t < flipDuration; t += Time.deltaTime)
        {
            SetScaleX(t / flipDuration);
            yield return null;
        }
        SetScaleX(1f);

        isAnimating = false;
    }

    private IEnumerator MatchEffect()
    {
        // "pop": membesar sebentar lalu balik, lalu kartu sedikit meredup
        const float duration = 0.3f;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float s = 1f + 0.15f * Mathf.Sin(t / duration * Mathf.PI);
            transform.localScale = new Vector3(s, s, 1f);
            yield return null;
        }
        transform.localScale = Vector3.one;
        cardImage.color = new Color(0.75f, 0.75f, 0.75f, 1f);
    }

    private void SetScaleX(float x)
    {
        Vector3 s = transform.localScale;
        s.x = x;
        transform.localScale = s;
    }
}