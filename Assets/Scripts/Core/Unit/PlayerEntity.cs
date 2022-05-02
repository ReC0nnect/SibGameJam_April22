using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : UnitEntity
{
    public bool IsFalling { get; private set; }

    public event System.Action OnFallingFinished;

    public PlayerEntity(SessionEntity session) : base(session)
    {
        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo)
        {
            View = playerGo.GetComponent<UnitView>();
            View.SetEntity(this);
        }
    }

    public void StartFalling()
    {
        View.StartCoroutine(FallingToNextLevel());
    }

    IEnumerator FallingToNextLevel()
    {
        var startPos = Position;
        var finishPos = Position;
        finishPos.y = -F.Settings.LevelDistance * Session.LevelNumber + 1f;
        var blockStartFalling = false;
        IsFalling = true;

        for (float t = 0f; t < 1f; t += Time.deltaTime / F.Settings.FallingTime)
        {
            var pos = Vector3.Lerp(startPos, finishPos, t);
            View.SetPosition(pos);
            if (t > 0.5f && !blockStartFalling)
            {
                blockStartFalling = true;
                Session.Clear();
                Session.Cube.StartFallCube(startPos, finishPos);
            }
            yield return null;
        }
        Session.Clear();
        OnFallingFinished?.Invoke();
        IsFalling = false;
    }
}
