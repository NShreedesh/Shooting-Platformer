using UnityEngine;

public class Tagger : MonoBehaviour, ITaggable
{

    [field: SerializeField]
    public Tag Tag { get; private set; }
}
