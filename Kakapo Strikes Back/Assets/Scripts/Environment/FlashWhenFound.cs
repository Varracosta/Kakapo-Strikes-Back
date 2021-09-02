using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhenFound : MonoBehaviour
{
    [Header("Visual part")]
    [SerializeField] private Material materialWhite;
    private Material materialDefault;
    private SpriteRenderer spriteRenderer;

    [Header("Audio part")]
    [SerializeField] private AudioClip foundSound;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = spriteRenderer.material;
    }
    
    public void PlayFlashAndSound()
    {
        StartCoroutine(WaitAndFlash());
        AudioSource.PlayClipAtPoint(foundSound, Camera.main.transform.position);
    }
    private IEnumerator WaitAndFlash()
    {
        spriteRenderer.material = materialWhite;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = materialDefault;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = materialWhite;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = materialDefault;
    }
}
