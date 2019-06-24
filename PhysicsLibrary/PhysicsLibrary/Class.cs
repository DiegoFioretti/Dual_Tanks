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
        float localWheelRadius;
        float minWheelSpeed;
        float maxWheelSpeed;
        float wheelAcceleration;
        float currentWheelAcceleration;
        float startWheelSpeed;
        float currentWheelSpeed;
        float negativeYDrag;
        float lowerYLimit;
        float inputTime;
        float auxTime;
        float rightXAccel;
        float rightYAccel;
        float leftXAccel;
        float leftYAccel;
        float rightXSpeed;
        float rightYSpeed;
        float leftXSpeed;
        float leftYSpeed;
        bool goingLeft;
        bool goingRight;
        float startCircXPosition;
        float startCircYPosition;
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
        public void SetStartingCircleConstants(float radius, float minspeed, float maxspeed, float acceleration, float drag, float ylimit)
        {
            localWheelRadius = radius;
            minWheelSpeed = minspeed;
            maxWheelSpeed = maxspeed;
            wheelAcceleration = acceleration;
            negativeYDrag = drag;
            lowerYLimit = ylimit;
            currentWheelSpeed = 0.0f;
            rightYAccel = 0.0f;
            leftYAccel = 0.0f;
            rightYAccel = 0.0f;
            leftYAccel = 0.0f;
            rightXSpeed = 0.0f;
            rightYSpeed = 0.0f;
            leftXSpeed = 0.0f;
            leftYSpeed = 0.0f;
        }

        public void RightInput(KeyCode right, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(right) && goingRight != true)
            {
                inputTime = gameTime;
                goingRight = true;
                startWheelSpeed = currentWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
            else if (Input.GetKeyUp(right) && goingRight == true)
            {
                inputTime = gameTime;
                goingRight = false;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
                startWheelSpeed = currentWheelSpeed;
                if (!goingLeft)
                {
                    //inputTime = gameTime;
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
                startWheelSpeed = currentWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
            else if (Input.GetKeyUp(left) && goingLeft == true)
            {
                inputTime = gameTime;
                goingLeft = false;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
                startWheelSpeed = currentWheelSpeed;
                if (!goingRight)
                {
                    //inputTime = gameTime;
                    newCircularXPosition = currentXposition;
                    newCircularYPosition = currentYposition;
                }
            }
        }

        private void CalculateSpeed(float calcTime) {
            if (goingLeft == true || goingRight == true)
            {
                currentWheelSpeed = startWheelSpeed + wheelAcceleration * calcTime;
                currentWheelAcceleration = wheelAcceleration;
            }
            if (goingLeft == false && goingRight == false)
            {
                currentWheelSpeed = startWheelSpeed - wheelAcceleration * calcTime;
                currentWheelAcceleration = - wheelAcceleration;
            }

            if (currentWheelSpeed > maxWheelSpeed)
            {
                currentWheelSpeed = maxWheelSpeed;
                currentWheelAcceleration = 0.0f;
            }
            if (currentWheelSpeed < minWheelSpeed)
            {
                currentWheelSpeed = minWheelSpeed;
                currentWheelAcceleration = 0.0f;
            }

            /*rightXAccel = currentWheelAcceleration * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
            rightYAccel = currentWheelAcceleration * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);

            leftXAccel = currentWheelAcceleration * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
            leftYAccel = currentWheelAcceleration * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);

            rightXSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
            rightYSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);

            leftXSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
            leftYSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);*/
            if (goingRight == true)
            {
                rightXSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
                rightYSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);
                if (true)
                {

                }
                rightXAccel = currentWheelAcceleration * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
                rightYAccel = currentWheelAcceleration * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);
            }
            else if (goingRight == false)
            {
                rightXSpeed = 0.0f;
                rightYSpeed = 0.0f;
                rightXAccel = 0.0f;
                rightYAccel = 0.0f;
            }

            if (goingLeft == true)
            {
                leftXSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
                leftYSpeed = (currentWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);
                leftXAccel = wheelAcceleration * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
                leftYAccel = wheelAcceleration * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);
            }
            else if (goingLeft == false)
            {
                leftXSpeed = 0.0f;
                leftYSpeed = 0.0f;
                leftXAccel = 0.0f;
                leftYAccel = 0.0f;
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
            
            CalculateSpeed(currentTime - auxTime);

            newCircularXPosition = startCircXPosition + ((rightXSpeed + leftXSpeed) * (currentTime - auxTime)) + ((1.0f / 2.0f) * (rightXAccel + leftXAccel) * ((currentTime - auxTime) * (currentTime - auxTime)));
            
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
            
            CalculateSpeed(currentTime - auxTime);

            //if (rightYSpeed == 0.0f && leftYSpeed == 0.0f)
            //{
            //    startCircYPosition = currentYPosition;
            //}

            if (currentYPosition >= lowerYLimit)
            {
                leftYAccel -= negativeYDrag;
                rightYAccel -= negativeYDrag;
            }

            newCircularYPosition = startCircYPosition + ((rightYSpeed + leftYSpeed) * (currentTime - auxTime)) + ((1.0f / 2.0f) * (rightYAccel + leftYAccel) * ((currentTime - auxTime) * (currentTime - auxTime)));

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
