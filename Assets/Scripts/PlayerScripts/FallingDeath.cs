using TagScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class FallingDeath : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!other.transform.TryGetComponent<ITaggable>(out var taggable)) return;
            if(taggable.Tag != Tag.FallDeath) return;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
