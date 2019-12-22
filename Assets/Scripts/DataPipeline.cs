using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 전환 간의 데이터 유지를 위한 스크립트
/// </summary>
public class DataPipeline : Singleton<DataPipeline>
{
    
    [HideInInspector]
    public int winner { get; private set; }

    private GameManager gm;


    private void Start()
    {
        SceneManager.sceneLoaded += SubScribe_onGameEnd;
    }

    private void SubScribe_onGameEnd(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("GameScene"))
        {
            gm = GameManager.Instance;
            if (gm != null)
            {
                gm.onGameEnd += SetWinner;
            }
        }
        
    }

    private void SetWinner()
    {
        Debug.Log("승자 :" + winner);
        winner = gm.winner;
    }

    public void Clear()
    {
        winner = -1;

    }

    


}
