using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotatioSpeed = 10f;
    public Transform cameraTransform;

    private Animator anim;

    void Start()
    {
        // Mengambil komponen child animator 3D Karakter
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Ambil input keyboard WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        if (GameManager.Instance.currentState != GameManager.GameState.Exploration)
        {
            anim.SetFloat("Speed", 0f); // balik ke idle
            return;
        }

        // Cek apakah Player menekan tombol jalan
        if (inputDir.magnitude >= 0.1f)
        {
            // kalkulasi arah jalan agar mengikuti arah kamera
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            // kunci sumbu Y karakter agar karakter tidak terbang saat kamera ditundukan
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // menentukan arah pergerakan final
            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

            // menggerakan posisi karakter
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            // Putar badan Karakter agar menghadap kearah berjalan
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotatioSpeed * Time.deltaTime);

            // kirim sinyal ke animator untuk play animasi run
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            // else jika tidak ada tombol ditekan, maka speed = 0f, dan kembali Idle
            anim.SetFloat("Speed", 0f);
        }
    }
}
