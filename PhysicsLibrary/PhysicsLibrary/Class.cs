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
        float ySpeed;
        float xSpeed;
        float localGravity;
        Vector3 auxMovement;
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

        public float SetStartingXSpeed(float startSpeed, float shootAngle)
        {
            return xSpeed = startSpeed * Mathf.Cos(shootAngle);
        }

        public float SetStartingYSpeed(float startSpeed, float shootAngle, float gravity)
        {
            localGravity = gravity;
            return ySpeed = startSpeed * Mathf.Sin(shootAngle) - gravity;
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

        public Vector3 Get2DMovement()
        {
            auxMovement.x = xSpeed;
            auxMovement.y = ySpeed;
            auxMovement.z = 0;
            UpdateAngledYSpeed();
            return auxMovement;
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
        
        void UpdateAngledYSpeed()
        {
            ySpeed = ySpeed - localGravity; 
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
        }

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
