using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PhysicsLibrary
{
    public class PhysicsClass
    {
        // Variables for angled shot
        Vector3 startPosition;
        float localGravity;
        float xSpeed;
        float ySpeed;
        float newXPosition;
        float newYPosition;
        // Variables for collisions
        float deltaX;
        float deltaY;
        float circleDistanceSquare;
        // Variables for uniformed circular motion
        float localRadius;
        float circularSpeed;
        float negativeYDrag;
        float lowerYLimit;
        float inputTime;
        float auxTime;
        bool goingLeft;
        bool goingRight;
        float newCircularXPosition;
        float newCircularYPosition;

        #region ANGLED MOTION FUNCTIONS
        public void SetStartingAngledVariables(Vector3 position,float Speed, float shootAngle, float gravity)
        {
            xSpeed = Speed * Mathf.Cos(shootAngle);
            ySpeed = Speed * Mathf.Sin(shootAngle);
            localGravity = gravity;
            startPosition = position;
        }

        public float Get2DMovementX(float time)
        {
            newXPosition = startPosition.x + xSpeed * time;
            return newXPosition;
        }

        public float Get2DMovementY(float time)
        {
            newYPosition = startPosition.y + (ySpeed * time) + ((1.0f / 2.0f) * localGravity * (time * time));
            return newYPosition;
        }
        #endregion

        #region CIRCLE MOTION FUNCTIONS
        public void SetStartingCircleConstants(float radius, float speed, float drag, float ylimit)
        {
            localRadius = radius;
            circularSpeed = speed;
            negativeYDrag = drag;
            lowerYLimit = ylimit;
        }

        public void RightInput(KeyCode right, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(right) && goingRight != true)
            {
                inputTime = gameTime;
                goingRight = true;
            }
            else if (Input.GetKeyUp(right) && goingRight == true)
            {
                goingRight = false;
                if (!goingLeft)
                {
                    inputTime = -1;
                    newCircularXPosition = currentXposition;
                    newCircularYPosition = currentYposition;
                }
            }
        }

        public void LeftInput(KeyCode left, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(left) && goingLeft != true)
            {
                inputTime = gameTime;
                goingLeft = true;
            }
            else if (Input.GetKeyUp(left) && goingLeft == true)
            {
                goingLeft = false;
                if (!goingRight)
                {
                    inputTime = -1;
                    newCircularXPosition = currentXposition;
                    newCircularYPosition = currentYposition;
                }
            }
        }

        public float GetCircularXMovement(float currentXPosition, float currentTime)
        {
            if (inputTime < 0){
                auxTime = currentTime;
            }
            else
            {
                auxTime = inputTime;
            }

            newCircularXPosition = currentXPosition;

            if (goingRight && !goingLeft)
            {
                newCircularXPosition = localRadius * Mathf.Cos(circularSpeed * currentTime - auxTime) + (currentXPosition + localRadius);
            }
            else if (goingLeft && !goingRight)
            {
                newCircularXPosition = localRadius * - (Mathf.Cos(circularSpeed * currentTime - auxTime)) + (currentXPosition - localRadius);
            }
            else if (goingRight && goingLeft)
            {
                newCircularXPosition = localRadius * Mathf.Cos(circularSpeed * currentTime - auxTime) + (currentXPosition + localRadius);
                newCircularXPosition -= localRadius * - (Mathf.Cos(circularSpeed * currentTime - auxTime)) + (currentXPosition - localRadius);
            }

            return newCircularXPosition;
        }

        public float GetCircularYMovement(float currentYPosition, float currentTime)
        {
            if (inputTime < 0)
            {
                auxTime = currentTime;
            }
            else
            {
                auxTime = inputTime;
            }

            newCircularYPosition = currentYPosition;

            if (goingRight && goingLeft)
            {
                newCircularYPosition = localRadius * Mathf.Sin(circularSpeed * currentTime - auxTime) + currentYPosition;
                newCircularYPosition += localRadius * Mathf.Sin(circularSpeed * currentTime - auxTime) + currentYPosition;
            }
            else if (goingRight || goingLeft)
            {
                newCircularYPosition = localRadius * Mathf.Sin(circularSpeed * currentTime - auxTime) + currentYPosition;
            }

            if (currentYPosition >  lowerYLimit)
            {
                newCircularYPosition -= negativeYDrag;
            }

            return newCircularYPosition;
        }
        #endregion

        #region COLISION DETECTION FUNCTIONS
        public bool CheckTopCollisionSqSq(Vector2 firstSqCenter, Sprite firstSq, Vector2 secondSqCenter, Sprite secondSq)
        {
            if (firstSqCenter.y + (firstSq.bounds.size.y/2) >= secondSqCenter.y - (secondSq.bounds.size.y/2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckBottomCollisionSqSq(Vector2 firstSqCenter, Sprite firstSq, Vector2 secondSqCenter, Sprite secondSq)
        {
            if (firstSqCenter.y - (firstSq.bounds.size.y / 2) <= secondSqCenter.y + (secondSq.bounds.size.y / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckLeftlCollisionSqSq(Vector2 firstSqCenter, Sprite firstSq, Vector2 secondSqCenter, Sprite secondSq)
        {
            if (firstSqCenter.x - (firstSq.bounds.size.x / 2) <= secondSqCenter.x + (secondSq.bounds.size.x / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckRightCollisionSqSq(Vector2 firstSqCenter, Sprite firstSq, Vector2 secondSqCenter, Sprite secondSq)
        {
            if (firstSqCenter.x + (firstSq.bounds.size.x / 2) >= secondSqCenter.x - (secondSq.bounds.size.x / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckGeneralCollisionSqSq(Vector2 firstSqCenter, Sprite firstSq, Vector2 secondSqCenter, Sprite secondSq)
        {
            if (firstSqCenter.y + (firstSq.bounds.size.y / 2) >= secondSqCenter.y - (secondSq.bounds.size.y / 2) ||
                firstSqCenter.y - (firstSq.bounds.size.y / 2) <= secondSqCenter.y + (secondSq.bounds.size.y / 2) ||
                firstSqCenter.x - (firstSq.bounds.size.x / 2) <= secondSqCenter.x + (secondSq.bounds.size.x / 2) ||
                firstSqCenter.x + (firstSq.bounds.size.x / 2) >= secondSqCenter.x - (secondSq.bounds.size.x / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckCollisionSqCc(Vector2 squareCenter, Sprite square, Vector2 circleCenter, Sprite circle, float circleRadius)
        {
            deltaX = Mathf.Abs(circleCenter.x - squareCenter.x);
            deltaY = Mathf.Abs(circleCenter.y - squareCenter.y);
            if (deltaX > (square.bounds.size.x / 2 + circleRadius)) { return false; }
            if (deltaY > (square.bounds.size.y / 2 + circleRadius)) { return false; }
            if (deltaX <= (square.bounds.size.x / 2)) { return true; }
            if (deltaY <= (square.bounds.size.y / 2)) { return true; }
            circleDistanceSquare = ((deltaX - square.bounds.size.x / 2) * (deltaX - square.bounds.size.x / 2)) + ((deltaY - square.bounds.size.y / 2) * (deltaY - square.bounds.size.y / 2));

            return (circleDistanceSquare <= (circleRadius * circleRadius));
        }
        #endregion
    }
}
