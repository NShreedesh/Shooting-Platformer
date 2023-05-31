namespace PickupScripts
{
    public interface IPickable
    {
        public bool IsPicked { get; }

        public void Pick();
    }
}
