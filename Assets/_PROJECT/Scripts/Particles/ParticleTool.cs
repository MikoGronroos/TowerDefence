using System.Collections;
using UnityEngine;

public class ParticleTool : MonoBehaviour
{

	[SerializeField] private GameObject particle;

    [Header("Settings")]
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool playOnCollisionEnter;
    [SerializeField] private bool playOnTriggerEnter;

    [SerializeField] private string neededTag;

    [SerializeField] private float playDelay;

    [SerializeField] private Vector3 positionOffset;

    private void Start()
    {
        if (playOnStart) PlayParticle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playOnTriggerEnter) ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(playOnCollisionEnter) ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject collision)
    {
        if (collision.tag == neededTag)
        {
            PlayParticle();
        }
    }

    private void PlayParticle()
    {
        StartCoroutine(ParticleCoroutine());
    }

    private IEnumerator ParticleCoroutine()
    {
        yield return new WaitForSeconds(playDelay);

        GameObject clone = Instantiate(particle);
        clone.transform.position = transform.position + positionOffset;
        clone.GetComponent<ParticleSystem>().Play();
    }

}
