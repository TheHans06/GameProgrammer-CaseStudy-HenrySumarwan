using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryPuzzleManager : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Transform cardGridTransform;
    public GameObject cardPrefab;
    public List<Sprite> cardIcons;

    private List<Card> cards = new List<Card>();
    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private int matchesFound = 0;
    public event System.Action OnPuzzleSolved;

    private void OnEnable()
    {
        SetupPuzzle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ClosePuzzle();
    }

    public void SetupPuzzle()
    {
        StopAllCoroutines();
        CancelInvoke();
        firstSelectedCard = null;
        secondSelectedCard = null;

        // Bersihkan kartu lama
        foreach (Transform child in cardGridTransform)
        {
            Destroy(child.gameObject);
        }
        cards.Clear();
        matchesFound = 0;

        // Bikin pasangan gambar
        List<Sprite> puzzleList = new List<Sprite>();
        foreach (Sprite icon in cardIcons)
        {
            puzzleList.Add(icon);
            puzzleList.Add(icon); // tambah sepasang
        }

        // Fisher-Yates Shuffle
        for (int i = 0; i < puzzleList.Count; i++)
        {
            Sprite temp = puzzleList[i];
            int randomIndex = Random.Range(i, puzzleList.Count);
            puzzleList[i] = puzzleList[randomIndex];
            puzzleList[randomIndex] = temp;
        }
        
        foreach (Sprite icon in puzzleList)
        {
            GameObject newCard = Instantiate(cardPrefab, cardGridTransform);
            Card cardScript = newCard.GetComponent<Card>();
            // Urutan parameter disesuaikan (faceSprite, manager)
            cardScript.SetupCard(icon, this);
            cards.Add(cardScript);
        }
    }

    // METHOD: Dipanggil oleh Card.cs untuk mengecek apakah kartu boleh dibuka
    public bool CanReveal()
    {
        // Kartu hanya boleh dibuka jika kartu kedua belum dipilih (maksimal buka 2 kartu sekaligus)
        // Dan kita sedang tidak dalam jeda menunggu pengecekan (Coroutine CheckMatch)
        if (firstSelectedCard == null || secondSelectedCard == null)
        {
            return true;
        }
        return false;
    }

    // METHOD: Dipanggil oleh Card.cs saat kartu diklik & sukses terbuka
    public void CardRevealed(Card card)
    {
        if (firstSelectedCard == null)
        {
            firstSelectedCard = card;
        }
        else if (secondSelectedCard == null && card != firstSelectedCard)
        {
            secondSelectedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.6f);

        if (firstSelectedCard.cardFace == secondSelectedCard.cardFace)
        {
            firstSelectedCard.SetMatched();
            secondSelectedCard.SetMatched();
            matchesFound++;

            firstSelectedCard = null;
            secondSelectedCard = null;

            if (matchesFound >= cardIcons.Count)
            {
                yield return new WaitForSeconds(0.8f);  // kasih waktu lihat efek terakhir
                OnPuzzleSolved?.Invoke();
                ClosePuzzle();
            }
            yield break;
        }

        firstSelectedCard.CloseCard();
        secondSelectedCard.CloseCard();
        firstSelectedCard = null;
        secondSelectedCard = null;
    }

    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GameState.Exploration);
    }
}