using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Target -> Player yang akan difollow camera
    public float distance = 8.0f; // Jarak kamera dari karakter player
    public float mouseSensitivity = 3.0f;
    public float minY = 0f; // batas kamera nunduk ke bawah
    public float maxY = 80f; // batas kamera hadap ke atas

    private float currentX = 0.0f;
    private float currentY = 45.0f;

    void LateUpdate() 
    {
        if (target != null)
        {
            // putar kamera hanya saat mode eksplorasi
            if (GameManager.Instance.currentState == GameManager.GameState.Exploration)
            {
                // membaca pergerakan mouse
            currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
            currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            currentY = Mathf.Clamp(currentY, minY, maxY);
            }

            // Kalkulasi posisi & rotasi
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            
            transform.position = target.position + (rotation * dir);
            transform.LookAt(target.position);
        }
    }
}
