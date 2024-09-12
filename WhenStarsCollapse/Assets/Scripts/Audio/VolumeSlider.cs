using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private enum VolumeType { MUSIC, SFX };
    [Header("Type")]
    [SerializeField] private VolumeType type;
    private Slider volumeSlider;
    private void Awake()
    {
        volumeSlider = this.GetComponent<Slider>();
        SetVolumeOnStart();
    }
    private void Start()
    {
        SetVolumeOnStart();
    }
    private void SetVolumeOnStart()
    {
        switch (type)
        {
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        SetVolumeOnStart();
    }
    public void OnSliderValueChange()
    {
        switch (type)
        {
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                AudioManager.instance.musicBus.setVolume(volumeSlider.value);
                break;
            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = volumeSlider.value;
                AudioManager.instance.sfxBus.setVolume(volumeSlider.value);
                break;
            default:
                break;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.TriggerEvent("SFX_ButtonClick", 1);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.TriggerEvent("SFX_ButtonClick", 1);
    }
}
