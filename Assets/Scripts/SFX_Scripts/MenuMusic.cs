using UnityEngine;
// https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html
public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance = null;
    public static MenuMusic Instance
    {
        get { return instance; }
    }
    private AudioSource _audioSource;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}