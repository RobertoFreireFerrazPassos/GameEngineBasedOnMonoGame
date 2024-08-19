using GameEngine.Elements.Managers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine.Elements.Strategies;

public class SimpleMovementStrategy : IMovementStrategy
{
    public SpriteObject ThisObject;

    public int PreviousState { get; set; }

    private int _currentState; // 0- far 1- tilemap obstacle  2- find

    public int State
    {
        get
        {
            return _currentState;
        }
        set
        {
            PreviousState = _currentState;
            _currentState = value;
        }
    }

    public Vector2 _targetPosition;

    public List<Vector2> _positionsToTarget = new List<Vector2>();

    public List<Vector2> _lastPositionsToTarget = new List<Vector2>();

    private Timer _movingTimer;

    private float _minDist;

    private float _maxDist;

    public SimpleMovementStrategy(SpriteObject thisObject, float movingTime, float minDist, float maxDist)
    {
        ThisObject = thisObject;
        _movingTimer = new Timer(movingTime);
        _minDist = minDist;
        _maxDist = maxDist;
        _movingTimer.OnTimerElapsed += OnMovementTimerElapsed;
    }

    public void Update(float elapsedTime, SpriteObject target, List<SpriteObject> allies)
    {
        if (State == 0 || State == 1)
        {
            FindTarget(target);
        }
        else if (State == 2)
        {
            MoveToTarget(elapsedTime, allies);
        }
    }

    public bool IsObstacleBetween(Vector2 start, Vector2 end, int stepSize, Func<Vector2, bool> obstacleCheck)
    {
        _positionsToTarget = PositionUtils.GetPointsAlongLine(start, end, stepSize);
        foreach (var point in _positionsToTarget)
        {
            if (obstacleCheck(point))
            {
                return true;
            }
        }

        return false;
    }

    public void FindTarget(SpriteObject target)
    {
        var targetCenter = target.GetBox().Center.ToVector2();
        var thisObjectCenter = ThisObject.GetBox().Center.ToVector2();
        var distance = Vector2.Distance(targetCenter, thisObjectCenter);

        if (distance < _minDist)
        {
            return;
        }

        if (distance > _maxDist)
        {
            State = 0;
            return;
        }

        if (IsObstacleBetween(thisObjectCenter, targetCenter, 4, CheckForObstacle))
        {
            if (PreviousState == 2)
            {
                _positionsToTarget = _lastPositionsToTarget;
                State = 2;
                return;
            }
            State = 1;
            return;
        }

        _targetPosition = targetCenter;
        State = 2;
    }

    private bool CheckForObstacle(Vector2 position)
    {
        return TileMapManager.IsCollidingWith(position);
    }

    private void MoveToTarget(float elapsedTime, List<SpriteObject> allies)
    {
        _movingTimer.StartIfInactive();
        _movingTimer.Update(elapsedTime);
        UpdatePosition(elapsedTime, allies);
    }

    private void OnMovementTimerElapsed()
    {
        _lastPositionsToTarget = _positionsToTarget;
        State = 0;
    }

    private void UpdatePosition(float elapsedTime, List<SpriteObject> allies)
    {
        var targetPosition = _positionsToTarget[_positionsToTarget.Count - 1];
        var direction = targetPosition - ThisObject.GetBox().Center.ToVector2();
        var distance = direction.Length();

        direction = direction * ThisObject.Speed * elapsedTime;
        direction.Normalize();

        var tempX = ThisObject.Position.X;
        var tempY = ThisObject.Position.Y;

        ThisObject.Position.X += direction.X;

        if (TileMapManager.IsCollidingWith(ThisObject.GetBox()) || DetectCollisionWithAllies(allies))
        {
            ThisObject.Position.X = tempX;
        }

        ThisObject.Position.Y += direction.Y;

        if (TileMapManager.IsCollidingWith(ThisObject.GetBox()) || DetectCollisionWithAllies(allies))
        {
            ThisObject.Position.Y = tempY;
        }
    }

    private bool DetectCollisionWithAllies(List<SpriteObject> allies)
    {
        foreach (var ally in allies)
        {
            if (ally != ThisObject && ThisObject.GetBox().Intersects(ally.GetBox()))
            {
                return true;
            }
        }

        return false;
    }

    public void Draw()
    {

        if (State != 2)
        {
            return;
        }

        var offset = Camera.Position;
        foreach (var position in _positionsToTarget)
        {
            SpriteManager.DrawPixel(
                new Vector2(
                    position.X + (int)offset.X,
                    position.Y + (int)offset.Y
                ), Color.OrangeRed
            );
        }
    }
}
