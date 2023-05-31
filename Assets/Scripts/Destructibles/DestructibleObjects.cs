using ObjectPooling.ObjectPooler;
using TagScripts;
using UnityEngine;

namespace Destructibles
{
    public class DestructibleObjects : MonoBehaviour
    {
        private ITaggable tagger;
        private BlastImpactObjectPooler blastImpactObjectPooler;

        private void Awake()
        {
            tagger = GetComponent<ITaggable>();
            blastImpactObjectPooler = FindObjectOfType<BlastImpactObjectPooler>();
        }
        
        private void OnDisable()
        {
            if (blastImpactObjectPooler == null) return;
            GameObject g = blastImpactObjectPooler.GetPool(tagger.Tag).Pool.Get().gameObject;
            g.transform.position = transform.position;
        }
    }
}
