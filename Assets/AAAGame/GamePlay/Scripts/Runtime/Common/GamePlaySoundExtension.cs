using GameFramework;
using GameFramework.Resource;
using GameFramework.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public static class GamePlaySoundExtension
{
    /// <summary>
    /// 播放bgm
    /// </summary>
    /// <param name="soundCom"></param>
    /// <param name="audioPath"></param>
    /// <param name="group"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public static int? PlayGamePlayBGM(this SoundComponent soundCom, string audioPath, string group, float volume = 1)
    {
        if (GFBuiltin.Resource.HasAsset(audioPath) == HasAssetResult.NotExist) return null;
        var parms = PlaySoundParams.Create();
        parms.Priority = 64;
        parms.Loop = true;
        parms.VolumeInSoundGroup = volume;
        return soundCom.PlaySound(audioPath, group, 0, parms, Vector3.zero);
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="soundCom"></param>
    /// <param name="audioPath"></param>
    /// <returns></returns>
    public static int? PlayGamePlaySound(this SoundComponent soundCom, string audioPath, string group, bool isLoop = false, float volume = 1)
    {
        if (GFBuiltin.Resource.HasAsset(audioPath) == HasAssetResult.NotExist) return null;
        var parms = PlaySoundParams.Create();
        parms.VolumeInSoundGroup = volume;
        parms.Loop = isLoop;
        return soundCom.PlaySound(audioPath, group, 0, parms, Vector3.zero);
    }
}
