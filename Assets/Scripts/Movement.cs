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

  // particle system
  [SerializeField] ParticleSystem thrustParticle;
  [SerializeField] ParticleSystem rightBoosterParticle;
  [SerializeField] ParticleSystem leftBoosterParticle;

  AudioSource audioSource;
  [SerializeField] AudioClip boostAudio;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  private void OnEnable()
  {
    terbang.Enable();
    putar.Enable();
  }

  private void LateUpdate()
  {
    ProsesTerbang();
    ProsesPutar();
  }

  private void ProsesTerbang()
  {
    if (terbang.IsPressed())
    {
      rb.AddRelativeForce(Vector3.up * kekuatanTerbang * Time.deltaTime);
      thrustParticle.Play();
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(boostAudio);
      }
    }
    else
    {
      thrustParticle.Stop();
      audioSource.Stop();
    }
  }

  private void ProsesPutar()
  {
    float nilaiPutar = putar.ReadValue<float>(); // isinya -1 dan 1

    // belok kiri
    if (nilaiPutar > 0) // jika 1 (kanan)
    {
      Debug.Log("input pusing kanan");
      ApplyPutar(-kekuatanPutar);
      if (!leftBoosterParticle.isPlaying)
      {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Play();
      }
    }

    // belok kanan
    else if (nilaiPutar < 0) // jika -1 (kiri)
    {
      Debug.Log("input pusing kiri");
      ApplyPutar(kekuatanPutar);
      if (!rightBoosterParticle.isPlaying)
      {
        leftBoosterParticle.Stop();
        rightBoosterParticle.Play();
      }
    }

    else
    {
      Debug.Log("Tidak ada input putar");
      rightBoosterParticle.Stop();
      leftBoosterParticle.Stop();
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
