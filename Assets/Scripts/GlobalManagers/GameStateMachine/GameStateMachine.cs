using System.Collections.Generic;
using UnityEngine;

public static class GameStateMachine
{
    public enum GameState { FREE, PLAY, PAUSE, FINISH }

    public static GameState state { get; private set; } = GameState.FREE;

    private readonly static List<object> listeners = new();

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    public static void AddListener(object listener)
    {

        listeners.Add(listener);
        
    }

    public static void RemoveListener(object listener)
    {

        listeners.Remove(listener);

    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    public static void Play()
    {
        

        if (state != GameState.FREE)
        {
            Debug.LogWarning("����� ��������� ���� ������ �� ��������� 'FREE'!");
            return;
        }

        state = GameState.PLAY;

        foreach (var listener in listeners)
        {
            if (listener is IGamePlayListener playListener)
            {
                playListener.OnPlay();
            }
        }

    }

    public static void Resume()
    {

        if (state != GameState.PAUSE)
        {
            Debug.LogWarning("����� ���������� ���� ������ �� ��������� 'PAUSE'!");
            return;
        }

        state = GameState.PLAY;

        foreach (var listener in listeners)
        {
            if (listener is IGameResumeListener resumeListener)
            {
                resumeListener.OnResume();
            }
        }

    }

    public static void Pause()
    {

        if (state != GameState.PLAY)
        {
            Debug.LogWarning("����� ���������� ���� ������ �� ��������� 'PLAY'!");
            return;
        }

        state = GameState.PAUSE;

        foreach (var listener in listeners)
        {
            if (listener is IGamePauseListener pauseListener)
            {
                pauseListener.OnPause();
            }
        }

    }

    public static void Finish()
    {

        if (state != GameState.PLAY && state != GameState.PAUSE)
        {
            Debug.LogWarning("����� ��������� ���� ������ �� ��������� 'PLAY' � 'PAUSE'!");
            return;
        }

        state = GameState.FINISH;

        foreach (var listener in listeners)
        {
            
            if (listener is IGameFinishListener finishListener)
            {
                finishListener.OnFinish();
            }
        }

    }

    public static void GetInFreeState()
    {
        if (state != GameState.PLAY && state != GameState.PAUSE && state != GameState.FINISH)
        {
            Debug.LogWarning("����� ����� � ��������� ������� ��������� ������ �� ��������� 'FINISH'!");
            return;
        }

        state = GameState.FREE;

        foreach (var listener in listeners)
        {
            if (listener is IGameGetFreeStateListener getFreeStateListener)
            {
                getFreeStateListener.OnGetFreeState();
            }
        }

    }


}
