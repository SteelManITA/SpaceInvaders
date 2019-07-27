using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static GameState instance = new GameState();

    private int score;
    private int level;
    private IPlayer player;
    private float time;
    private int enemyDamage;

    public enum State {
        Started,
        Paused,
        Stopped
    };
    public State state;

    private GameState() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        this.restart();
    }

    public static GameState getInstance() {
        return GameState.instance;
    }

    public void restart() {
        this.score = 0;
        this.level = 1;
        this.time = 0f;
    }

    public void incrementScore(int score) {
        if (score < 0) {
            score = -score;
        }
        this.score += score;
    }

    public void incrementTime(float time) {
        this.time += time;
    }

    public int getScore() {
        return this.score;
    }

    public void nextLevel() {
        ++this.level;
    }

    public int getLevel() {
        return this.level;
    }

    public float getTime() {
        return this.time;
    }

    public void setPlayer(IPlayer player) {
        this.player = player;
    }

    public IPlayer getPlayer() {
        return this.player;
    }

    public State getState() {
        return this.state;
    }

    public void start() {
        this.state = State.Started;
    }

    public void pause() {
        this.state = State.Paused;
    }

    public void stop() {
        this.state = State.Stopped;
    }
}
