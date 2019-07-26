using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static GameState instance = new GameState();

    private int score;
    private int level;
    private IPlayer player;
    private int enemyDamage;

    public enum State {
        Started,
        Paused,
        Stopped
    };
    public State state;

    private GameState() {
        this.restart();
    }

    public static GameState getInstance() {
        return GameState.instance;
    }

    public void restart() {
        this.score = 0;
        this.level = 1;
    }

    public void incrementScore(int score) {
        if (score < 0) {
            score = -score;
        }
        this.score += score;
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

    public void setPlayer(IPlayer player) {
        this.player = player;
    }

    public IPlayer getPlayer() {
        return this.player;
    }

    public void setEnemyDamage(int damage) {
        this.enemyDamage = damage;
    }

    public int getEnemyDamage() {
        return this.enemyDamage;
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
