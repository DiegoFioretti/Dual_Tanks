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
        float minWheelAcceleration;
        float maxWheelAcceleration;
        float wheelAccelerationMod;
        // Right/Left differenciators
        float startRightWheelSpeed;
        float currentRightWheelAccel;
        float currentRightWheelSpeed;
        float startLeftWheelSpeed;
        float currentLeftWheelAccel;
        float currentLeftWheelSpeed;
        float defaultFriction;
        //--------------------------------
        float inputTime;
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
        bool goingDown;
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
        public void SetStartingCircleConstants(float radius, float minspeed, float maxspeed, float minAccel, float maxAccel, float accelerationModifier)
        {
            localWheelRadius = radius;
            minWheelSpeed = minspeed;
            maxWheelSpeed = maxspeed;
            minWheelAcceleration = minAccel;
            maxWheelAcceleration = maxAccel;
            defaultFriction = maxAccel * (1.0f / 3.0f);
            wheelAccelerationMod = accelerationModifier;
            startRightWheelSpeed = 0.0f;
            currentRightWheelAccel = 0.0f;
            currentRightWheelSpeed = 0.0f;
            startLeftWheelSpeed = 0.0f;
            currentLeftWheelAccel = 0.0f;
            currentLeftWheelSpeed = 0.0f;
        }

        public void RightInput(KeyCode right, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(right) && goingRight != true)
            {
                inputTime = gameTime;
                goingRight = true;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
            else if (Input.GetKeyUp(right) && goingRight == true)
            {
                inputTime = gameTime;
                goingRight = false;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
        }

        public void LeftInput(KeyCode left, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(left) && goingLeft != true)
            {
                inputTime = gameTime;
                goingLeft = true;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
            else if (Input.GetKeyUp(left) && goingLeft == true)
            {
                inputTime = gameTime;
                goingLeft = false;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
        }

        public void DownInput(KeyCode down, float currentXposition, float currentYposition, float gameTime)
        {
            if (Input.GetKeyDown(down) && goingDown != true)
            {
                inputTime = gameTime;
                goingDown = true;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
            else if (Input.GetKeyUp(down) && goingDown == true)
            {
                inputTime = gameTime;
                goingDown = false;
                startRightWheelSpeed = currentRightWheelSpeed;
                startLeftWheelSpeed = currentLeftWheelSpeed;
                startCircXPosition = currentXposition;
                startCircYPosition = currentYposition;
            }
        }

        public void CalculateSpeed(float currentTime) {
            // Use clamps for setting max and min speeds
            // use the numbers signs (pos or neg) to know which direction the friction goes

            // Current Right Acceleration---------------------------------------------------------------------------------------
            if (currentRightWheelAccel < maxWheelAcceleration && goingRight == true)
            {
                currentRightWheelAccel += wheelAccelerationMod;
                if (currentRightWheelAccel > maxWheelAcceleration)
                {
                    currentRightWheelAccel = maxWheelAcceleration;
                }
            }
            if (currentRightWheelAccel > minWheelAcceleration && goingDown == true)
            {
                currentRightWheelAccel -= wheelAccelerationMod;
                if (currentRightWheelAccel < minWheelAcceleration)
                {
                    currentRightWheelAccel = minWheelAcceleration;
                }
            }
            if (goingRight == false && goingDown == false && currentRightWheelAccel != 0.0f)
            {
                if (currentRightWheelAccel < 0.0f)
                {
                    currentRightWheelAccel += wheelAccelerationMod;
                    if (currentRightWheelAccel > 0.0f)
                    {
                        currentRightWheelAccel = 0.0f;
                    }
                }
                else if (currentRightWheelAccel > 0.0f)
                {
                    currentRightWheelAccel -= wheelAccelerationMod;
                    if (currentRightWheelAccel < 0.0f)
                    {
                        currentRightWheelAccel = 0.0f;
                    }
                }
            }

            // Current Left Acceleration---------------------------------------------------------------------------------------
            if (currentLeftWheelAccel < maxWheelAcceleration && goingLeft == true)
            {
                currentLeftWheelAccel += wheelAccelerationMod;
                if (currentLeftWheelAccel > maxWheelAcceleration)
                {
                    currentLeftWheelAccel = maxWheelAcceleration;
                }
            }
            if (currentLeftWheelAccel > minWheelAcceleration && goingDown == true)
            {
                currentLeftWheelAccel -= wheelAccelerationMod;
                if (currentLeftWheelAccel < minWheelAcceleration)
                {
                    currentLeftWheelAccel = minWheelAcceleration;
                }
            }
            if (goingLeft == false && goingDown == false && currentLeftWheelAccel != 0.0f)
            {
                if (currentLeftWheelAccel < 0.0f)
                {
                    currentLeftWheelAccel += wheelAccelerationMod;
                    if (currentLeftWheelAccel > 0.0f)
                    {
                        currentLeftWheelAccel = 0.0f;
                    }
                }
                else if (currentLeftWheelAccel > 0.0f)
                {
                    currentLeftWheelAccel -= wheelAccelerationMod;
                    if (currentLeftWheelAccel < 0.0f)
                    {
                        currentLeftWheelAccel = 0.0f;
                    }
                }
            }
            
            // Current Speeds--------------------------------------------------------------------------------------------------
            if ((currentRightWheelSpeed < maxWheelSpeed && goingRight == true) || (currentRightWheelSpeed > minWheelSpeed && goingDown == true) || (goingRight == false && goingDown == false))
            {
                if (goingRight == false && goingDown == false && currentRightWheelSpeed != 0.0f)
                {
                    if (currentRightWheelSpeed < 0.0f)
                    {
                        currentRightWheelSpeed = startRightWheelSpeed + defaultFriction * (currentTime - inputTime);
                        if (currentRightWheelSpeed > 0.0f)
                        {
                            currentRightWheelSpeed = 0.0f;
                        }
                    }
                    else if (currentRightWheelSpeed > 0.0f)
                    {
                        currentRightWheelSpeed = startRightWheelSpeed - defaultFriction * (currentTime - inputTime);
                        if (currentRightWheelSpeed < 0.0f)
                        {
                            currentRightWheelSpeed = 0.0f;
                        }
                    }
                }
                else if (currentRightWheelAccel != 0.0f)
                {
                    currentRightWheelSpeed = startRightWheelSpeed + currentRightWheelAccel * (currentTime - inputTime);
                    if (currentRightWheelSpeed > maxWheelSpeed)
                    {
                        currentRightWheelSpeed = maxWheelSpeed;
                    }
                    else if (currentRightWheelSpeed < minWheelSpeed)
                    {
                        currentRightWheelSpeed = minWheelSpeed;
                    }
                }
            }

            if ((currentLeftWheelSpeed < maxWheelSpeed && goingLeft == true) || (currentLeftWheelSpeed > minWheelSpeed && goingDown == true) || (goingLeft == false && goingDown == false))
            {
                if (goingLeft == false && goingDown == false && currentLeftWheelSpeed != 0.0f)
                {
                    if (currentLeftWheelSpeed < 0.0f)
                    {
                        currentLeftWheelSpeed = startLeftWheelSpeed + defaultFriction * (currentTime - inputTime);
                        if (currentLeftWheelSpeed > 0.0f)
                        {
                            currentLeftWheelSpeed = 0.0f;
                        }
                    }
                    else if (currentLeftWheelSpeed > 0.0f)
                    {
                        currentLeftWheelSpeed = startLeftWheelSpeed - defaultFriction * (currentTime - inputTime);
                        if (currentLeftWheelSpeed < 0.0f)
                        {
                            currentLeftWheelSpeed = 0.0f;
                        }
                    }
                }
                else if (currentLeftWheelAccel != 0.0f)
                {
                    currentLeftWheelSpeed = startLeftWheelSpeed + currentLeftWheelAccel * (currentTime - inputTime);
                    if (currentLeftWheelSpeed > maxWheelSpeed)
                    {
                        currentLeftWheelSpeed = maxWheelSpeed;
                    }
                    else if (currentLeftWheelSpeed < minWheelSpeed)
                    {
                        currentLeftWheelSpeed = minWheelSpeed;
                    }
                }
            }
            
            Debug.Log("Current Right Wheel Speed: " + currentRightWheelSpeed);
            //-----------------------------------------------------------------------------------------------------------------
            rightXAccel = currentRightWheelAccel * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
            rightYAccel = currentRightWheelAccel * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);
            leftXAccel = currentLeftWheelAccel * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
            leftYAccel = currentLeftWheelAccel * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);

            rightXSpeed = (currentRightWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 1.0f / 4.0f);
            rightYSpeed = (currentRightWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 1.0f / 4.0f);
            leftXSpeed = (currentLeftWheelSpeed * localWheelRadius) * Mathf.Cos(Mathf.PI * 3.0f / 4.0f);
            leftYSpeed = (currentLeftWheelSpeed * localWheelRadius) * Mathf.Sin(Mathf.PI * 3.0f / 4.0f);
        }

        public float GetCircularXMovement(float currentXPosition, float currentTime)
        {
            newCircularXPosition = startCircXPosition + ((rightXSpeed + leftXSpeed) * (currentTime - inputTime)) + ((1.0f / 2.0f) * (rightXAccel + leftXAccel) * ((currentTime - inputTime) * (currentTime - inputTime)));
            
            return newCircularXPosition;
        }

        public float GetCircularYMovement(float currentYPosition, float currentTime)
        {
            newCircularYPosition = startCircYPosition + ((rightYSpeed + leftYSpeed) * (currentTime - inputTime)) + ((1.0f / 2.0f) * (rightYAccel + leftYAccel) * ((currentTime - inputTime) * (currentTime - inputTime)));

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
