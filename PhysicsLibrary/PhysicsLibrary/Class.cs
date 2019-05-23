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
        float circularAngle;
        float circularPeriod;
        bool rotGoingRight;
        Vector3 circleCenter;
        Vector3 auxRadMovement;

        public void SetStartingAngledVariables(Vector3 position,float Speed, float shootAngle, float gravity)
        {
            xSpeed = Speed * Mathf.Cos(shootAngle);
            ySpeed = Speed * Mathf.Sin(shootAngle);
            localGravity = gravity;
            startPosition = position;
        }

        public void ResetAngledVariables()
        {
            localGravity = 0;
            xSpeed = 0;
            ySpeed = 0;
            newXPosition = 0;
            newYPosition = 0;
        }

        public void SetStartingRadiusConstants(float radius, float speed, Vector3 center ,bool goingRight)
        {
            localRadius = radius;
            circularSpeed = speed;
            circleCenter = center;
            circularPeriod = (2 * Mathf.PI) / speed;
            circularSpeed = (2 * Mathf.PI) / circularPeriod;
            rotGoingRight = goingRight;
            circularAngle = 0;
        }

        public float Get2DMovementX(float time)
        {
            newXPosition = xSpeed * time;
            return newXPosition;
        }

        public float Get2DMovementY(float time)
        {
            newYPosition = startPosition.y + (ySpeed * time) + ((1.0f / 2.0f) * localGravity * (time * time));
            return newYPosition;
        }

        public Vector3 GetRadialMovement()
        {
            if (rotGoingRight)
            {
                circularAngle += circularSpeed * Time.deltaTime;
            }
            else
            {
                circularAngle -= circularSpeed * Time.deltaTime;
            }

            auxRadMovement.x = Mathf.Cos(circularAngle * Mathf.Deg2Rad) * localRadius;
            auxRadMovement.y = Mathf.Sin(circularAngle * Mathf.Deg2Rad) * localRadius;
            auxRadMovement.z = 0;

            auxRadMovement += circleCenter;

            return auxRadMovement;
        }

        public bool CheckCollisionSqSq(Sprite firstSq, Sprite secondSq)
        {
            if (firstSq.rect.x <= secondSq.rect.x + secondSq.rect.width ||
                firstSq.rect.x + firstSq.rect.width >= secondSq.rect.x ||
                firstSq.rect.y <= secondSq.rect.y + secondSq.rect.height ||
                firstSq.rect.y + firstSq.rect.height >= secondSq.rect.y)
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
    }
}
