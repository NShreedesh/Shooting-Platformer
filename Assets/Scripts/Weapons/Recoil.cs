using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField]
    private bool upRecoil;
    [SerializeField]
    private float recoilValue = 2;

    public void StartRecoil()
    {
        if(upRecoil)
        {
            StopRecoil();
            transform.localEulerAngles += new Vector3(0, 0, Random.Range(0f, 1f) * recoilValue);
            upRecoil = false;
        }
        else
        {
            StopRecoil();
            transform.localEulerAngles -= new Vector3(0, 0, Random.Range(0f, 1f) * recoilValue);
            upRecoil = true;
        }
    }

    public void StopRecoil()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
