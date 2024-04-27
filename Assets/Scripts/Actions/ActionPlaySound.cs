using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Play Sound")]
public class ActionPlaySound: Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private AudioClip           sound;
    [SerializeField]
    private SoundManager.Type   type;
    [SerializeField, MinMaxSlider(0.1f, 2.0f)]
    private Vector2             volume = Vector2.one;
    [SerializeField, MinMaxSlider(0.1f, 2.0f)]
    private Vector2             pitch = Vector2.one;

    private AudioSource playingAudio;

    private void Update()
    {
        if (playingAudio != null)
        {
            if ((playingAudio.clip != sound) || (!playingAudio.isPlaying))
            {
                playingAudio = null;
            }
        }
    }

    protected override bool OnRun()
    {
        if (playingAudio != null) return true;

        playingAudio = SoundManager.PlaySound(type, sound, Random.Range(volume.x, volume.y), Random.Range(pitch.x, pitch.y));
        
        return true;
    }
}
