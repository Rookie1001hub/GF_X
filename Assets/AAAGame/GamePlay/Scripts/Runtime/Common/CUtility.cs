#region Comment Head
//------------------------------------------------------------
// Author:LiuXiYuan
// Date:2025/3/21 23:01:36
// Email:854327817@qq.com
//------------------------------------------------------------
#endregion

using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CUtility
{
    public static class AssetPath
    {
        /// <summary>
        /// 获取背景音乐的路径
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetGPMusicPath(string s)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.wav", Const.SoundGroup.Music, s);
        }
        /// <summary>
        /// 获取背景音乐的路径
        /// </summary>
        /// <param name="s"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetGPMusicPath(string s, string suffix)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.{2}", Const.SoundGroup.Music, s, suffix);
        }
        public static string GetGPAudioPathByGroup(Const.SoundGroup group, string s)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.wav", group, s);
        }
        /// <summary>
        /// 获取音乐路径
        /// </summary>
        /// <param name="group">音乐组</param>
        /// <param name="s">音乐名称</param>
        /// <param name="suffix">音乐文件后缀</param>
        /// <returns></returns>
        public static string GetGPAudioPathByGroup(Const.SoundGroup group, string s, string suffix)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.{2}", group, s, suffix);
        }
        /// <summary>
        /// 获取音乐路径
        /// </summary>
        /// <param name="group"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetGPAudioPathByGroup(string group, string s)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.wav", group, s);
        }
        public static string GetGPAudioPathByGroup(string group, string s, string suffix)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}/{1}.{2}", group, s, suffix);
        }
    }
}