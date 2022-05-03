using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : UnitEntity
{
    public bool IsFalling { get; private set; }

    public event System.Action OnFallingFinished;

    private Animation_Script Anim;

    public PlayerEntity(SessionEntity session) : base(session)
    {
        
        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo)
        {
            View = playerGo.GetComponent<UnitView>();
            View.SetEntity(this);
            Anim = View.GetComponentInChildren<Animation_Script>();
        }
    }

    public void StartFalling()
    {
		Anim.StartFalling();
		if (Session.Portal.IsEndgamePortal)
		       {
		           View.StartCoroutine(UpToNextLevel());
		       }
		       else
		       {
		           View.StartCoroutine(FallingToNextLevel());
		       }
    }

    IEnumerator FallingToNextLevel()
    {
        var startPos = Position;
        var finishPos = Position;
        finishPos.y = -F.Settings.LevelDistance * Session.LevelNumber + 1f;
        var blockStartFalling = false;
        IsFalling = true;
        if (Session.Follower != null)
        {
            Session.Follower.SetPosition(Position);
        }

        for (float t = 0f; t < 1f; t += Time.deltaTime / F.Settings.FallingTime)
        {
            var pos = Vector3.Lerp(startPos, finishPos, t);
            View.SetPosition(pos);
            if (t > 0.5f && !blockStartFalling)
            {
                blockStartFalling = true;
                Session.Clear();
                Session.Cube.StartFallCube(startPos + (finishPos - startPos) / 3f, finishPos);
            }
            yield return null;
        }
        Anim.StopFalling();
        Session.Clear();
        OnFallingFinished?.Invoke();
        IsFalling = false;
        
    }

    IEnumerator UpToNextLevel()
    {
        var startPos = Position;
        var finishPos = Position;
        finishPos.y = F.Settings.LevelDistance * Session.LevelNumber;
        var blockStartFalling = false;
        var gameFinished = false;
        IsFalling = true;
        Session.Follower.SetPosition(Position);
        var offset = Vector3.zero;
        var time = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime / F.Settings.FlightUpTime)
        {
            time += Time.deltaTime;
            if (t < 0.1f)
            {
                offset.x -= 3f * t;
            }
            if (time > 1f && !gameFinished)
            {
                gameFinished = true;
                Session.Win();
            }
            var pos = Vector3.Lerp(startPos + offset, finishPos, t);
            View.SetPosition(pos);
            if (t > 0.7f && !blockStartFalling)
            {
                blockStartFalling = true;
                Session.Clear();
                Session.Cube.StartFallCube(startPos + (finishPos - startPos) / 3f, finishPos);
            }
            yield return null;
        }
        Session.Clear();
        OnFallingFinished?.Invoke();
        IsFalling = false;
    }
}
