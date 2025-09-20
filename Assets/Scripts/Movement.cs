using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

  // input binding untuk roket
  [SerializeField] InputAction terbang;

  [SerializeField] InputAction putar;

  [SerializeField] float kekuatanTerbang = 1000.0f;
  [SerializeField] float kekuatanPutar = 100.0f;


  // rigid body si player
  Rigidbody rb;


  private void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void OnEnable()
  {
    terbang.Enable();
    putar.Enable();
  }

  private void FixedUpdate()
  {
    ProsesTerbang();
    ProsesPutar();
  }

  private void ProsesTerbang()
  {
    if (terbang.IsPressed())
    {
      rb.AddRelativeForce(Vector3.up * kekuatanTerbang * Time.deltaTime);
    }
  }

  private void ProsesPutar()
  {
    if (putar.IsPressed())
    {
      float nilaiPutar = putar.ReadValue<float>(); // isinya -1 dan 1

      // belok kiri
      if (nilaiPutar > 0) // jika 1 (kanan)
      {
        ApplyPutar(-kekuatanPutar);
      }

      // belok kanan
      if (nilaiPutar < 0) // jika -1 (kiri)
      {
        ApplyPutar(kekuatanPutar);
      }
    }
  }

  private void ApplyPutar(float nilaiKekuatan)
  {
    rb.freezeRotation = true; // stop rotasi

    // lanjut rotasi
    transform.Rotate(Vector3.forward * nilaiKekuatan * Time.deltaTime);
    rb.freezeRotation = false;
  }
}
