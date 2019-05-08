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
        float deltaX;
        float deltaY;
        // Variables for uniformed circular motion
        float localRadius;
        float localRadiusTime;
        float angularSpeed;
        float angularAcceleration;
        float angularDisplacement;
        bool rotGoingRight;
        Vector3 radialCenter;
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

        public void SetStartingRadiusConstants(float radius, float time, bool goingRight)
        {
            localRadius = radius;
            localRadiusTime = time;
            rotGoingRight = goingRight;
            angularSpeed = (2 * Mathf.PI * localRadius) / localRadiusTime;
            if (rotGoingRight)
            {
                angularAcceleration = angularSpeed / (localRadiusTime * localRadiusTime);
            }
            else
            {
                angularAcceleration = -angularSpeed / (localRadiusTime * localRadiusTime);
            }
        }

        public Vector3 SetStartingCenter(Vector3 center)
        {
            radialCenter = center;
            return radialCenter;
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

        public bool CheckCollisionSqCc(Sprite square, Sprite circle, float circleRadius)
        {
            deltaX = circle.pivot.x - ReturnMaximum(square.pivot.x, ReturnMinumum(circle.pivot.x, square.pivot.x + square.rect.width));
            deltaY = circle.pivot.y - ReturnMaximum(square.pivot.y, ReturnMinumum(circle.pivot.y, square.pivot.y + square.rect.height));
            return (deltaX * deltaX + deltaY * deltaY) < (circleRadius * circleRadius);
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

        private float ReturnMinumum(float valueA, float valueB)
        {
            if (valueA < valueB)
            {
                return valueA;
            }
            else if (valueB < valueA)
            {
                return valueB;
            }
            else
            {
                return valueA;
            }
        }

        private float ReturnMaximum(float valueA, float valueB)
        {
            if (valueA > valueB)
            {
                return valueA;
            }
            else if (valueB > valueA)
            {
                return valueB;
            }
            else
            {
                return valueA;
            }
        }
    }
}
