using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{
    private ITaggable tagger;
    private BlastImactObjectPooler blastImactObjectPooler;

    private void Awake()
    {
        tagger = GetComponent<ITaggable>();
        blastImactObjectPooler = FindObjectOfType<BlastImactObjectPooler>();
    }

    private void OnDisable()
    {
        if (blastImactObjectPooler == null) return;
        GameObject g = blastImactObjectPooler.GetPool(tagger.Tag).Pool.Get().gameObject;
        g.transform.position = transform.position;
    }
}
