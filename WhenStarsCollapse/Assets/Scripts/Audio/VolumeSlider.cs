using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum VolumeType { MUSIC, SFX };

/// <summary>
/// A class attached to the UI sliders in the Audio section. 
/// </summary>
public class VolumeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
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
    private void Update()
    {
        SetVolumeOnStart();
    }
    private void SetVolumeOnStart()
    {
        volumeSlider.value = AudioManager.Instance.GetVolume(type);
    }
    public void OnSliderValueChange()
    {
        AudioManager.Instance.SetVolume(type, volumeSlider.value);
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
