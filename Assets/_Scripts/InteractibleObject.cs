using System.Collections;
using TMPro;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    [Header("Puzzle")]
    public GameObject puzzleUI;
    public MemoryPuzzleManager puzzleManager;

    [Header("UI")]
    public GameObject interactPrompt;   // teks "Tekan [E] untuk bermain kartu"
    public TMP_Text rewardMessage;      // opsional

    [Header("Reward")]
    public GameObject rewardObject;     // opsional: peti/koin yang muncul saat menang

    private bool isPlayerInRange;
    private bool isSolved;

    private void Start()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (rewardObject != null) rewardObject.SetActive(false);
        if (rewardMessage != null) rewardMessage.gameObject.SetActive(false);
        if (puzzleManager != null) puzzleManager.OnPuzzleSolved += HandleSolved;
    }

    private void OnDestroy()
    {
        if (puzzleManager != null) puzzleManager.OnPuzzleSolved -= HandleSolved;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInRange = false;
    }

    private void Update()
    {
        bool canInteract = isPlayerInRange && !isSolved &&
            GameManager.Instance.currentState == GameManager.GameState.Exploration;

        if (interactPrompt != null && interactPrompt.activeSelf != canInteract)
            interactPrompt.SetActive(canInteract);

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Puzzle);
            if (puzzleUI != null) puzzleUI.SetActive(true);
        }
    }

    private void HandleSolved()
    {
        isSolved = true;
        if (rewardObject != null) rewardObject.SetActive(true);
        if (rewardMessage != null) StartCoroutine(ShowMessage("Puzzle selesai! Sesuatu muncul di dekatmu..."));
    }

    private IEnumerator ShowMessage(string msg)
    {
        rewardMessage.text = msg;
        rewardMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        rewardMessage.gameObject.SetActive(false);
    }
}