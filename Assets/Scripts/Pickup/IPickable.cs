using UnityEngine;

public interface IPickable
{
    public bool IsPicked { get; }

    public void Pick();
}
