using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider volume;
    void Start()
    {
        volume.onValueChanged.AddListener((value) => {
            PlayerPrefs.SetFloat(AudioController.VOLUME, value);
        });
    }

   

    
}
