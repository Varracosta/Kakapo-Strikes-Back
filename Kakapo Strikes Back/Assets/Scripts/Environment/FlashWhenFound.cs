using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhenFound : MonoBehaviour
{
    [Header("Visual part")]
    [SerializeField] private Material materialWhite;
    [SerializeField] private GameObject popUpText;
    private Material materialDefault;
    private SpriteRenderer spriteRenderer;

    [Header("Audio part")]
    [SerializeField] private AudioClip foundSound;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialDefault = spriteRenderer.material;
    }
    
    public void PlayEffects(GameObject player)
    {
        StartCoroutine(WaitAndFlash());
        AudioSource.PlayClipAtPoint(foundSound, Camera.main.transform.position);
        Instantiate(popUpText, player.transform.position, Quaternion.identity);
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
