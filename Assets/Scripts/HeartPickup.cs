using UnityEngine;
using DG.Tweening;

public class HeartPickup : MonoBehaviour
{
    public AudioClip collectSound;
    public float rotateSpeed = 180f; // degrees per second

    private void Start()
    {
        // Animación de flotación con DOTween
        transform.DOMoveY(transform.position.y + 0.5f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        // rotate around Y axis
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Animación al recoger con DOTween
            transform.DOScale(Vector3.one * 1.5f, 0.2f).SetEase(Ease.OutBounce);
            transform.DORotate(new Vector3(360, 360, 0), 0.2f, RotateMode.FastBeyond360);

            // Vibración (Sensor móvil)
            Handheld.Vibrate();

            GameManager.instance.GainHeart();

            Destroy(gameObject, 0.2f);
        }
    }
}
