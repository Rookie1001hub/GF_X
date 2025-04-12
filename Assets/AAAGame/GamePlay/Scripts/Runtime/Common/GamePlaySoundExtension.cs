using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public static class GamePlaySoundExtension
{
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="soundCom"></param>
    /// <param name="audioPath"></param>
    /// <returns></returns>
    public static int PlayGamePlaySound(this SoundComponent soundCom, string audioPath, string group, bool isLoop = false)
    {
        if (GFBuiltin.Resource.HasAsset(audioPath) == GameFramework.Resource.HasAssetResult.NotExist) return 0;
        var parms = ReferencePool.Acquire<GameFramework.Sound.PlaySoundParams>();
        parms.Clear();
        parms.Loop = isLoop;
        return soundCom.PlaySound(audioPath, group, 0, parms, Vector3.zero);
    }
}
