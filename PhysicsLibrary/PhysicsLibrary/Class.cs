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
        Vector3 xyAcceleration;
        Vector3 xySpeed;
        Vector3 newPosition;
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
            xySpeed.x = Speed * Mathf.Cos(shootAngle);
            xySpeed.y = Speed * Mathf.Sin(shootAngle);
            xySpeed.z = 0;
            xyAcceleration.x = 0;
            xyAcceleration.y = -gravity;
            xyAcceleration.z = 0;
            startPosition = position;
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
            return newPosition.x = startPosition.x + xySpeed * time;
        }

        public float Get2DMovementY(float time)
        {
            return newPosition.x = startPosition.x + (1 / 2) * xyAcceleration * (time * time) + xySpeed * time;
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
        
        /*void UpdateAngledYSpeed()
        {
            ySpeed = ySpeed - localGravity; 
        }*/

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
        /*
        public float GetLocalYSpeed()
        {
            return ySpeed;
        }

        public float GetLocalXSpeed()
        {
            return xSpeed;
        }

        public float GetLocalGravity()
        {
            return localGravity;
        }*/

        public float GetLocalRadius()
        {
            return localRadius;
        }

        public float GetCircularSpeed() {
            return circularSpeed;
        }
        public float GetCircularAngle() {
            return circularAngle;
        }

        public Vector3 GetCircleCenter() {
            return circleCenter;
        }
        public Vector3 GetAuxRadMovement()
        {
            return auxRadMovement;
        }
    }
}
