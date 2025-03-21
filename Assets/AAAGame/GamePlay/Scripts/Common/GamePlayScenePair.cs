using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum GamePlaySceneType
{
    GamePlay,
    Tool,
    Test,
}
[Serializable]
public class GamePlayScenePair
{
    public string scenePath;
    public string sceneKeyword;
    public GamePlaySceneType sceneTypeTag;
    public GamePlayScenePair(string s1, string s2, GamePlaySceneType sceneType)
    {
        scenePath = s1; sceneKeyword = s2; sceneTypeTag = sceneType;
    }
}