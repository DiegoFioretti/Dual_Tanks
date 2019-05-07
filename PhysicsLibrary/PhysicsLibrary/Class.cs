using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PhysicsLibrary
{
    public class PhysicsClass
    {
        float ySpeed;
        float xSpeed;
        float localGravity;
        Vector3 auxMovement;
        float deltaX;
        float deltaY;

        public float GetStartingXSpeed(float startSpeed, float shootAngle)
        {
            return xSpeed = startSpeed * Mathf.Cos(shootAngle);
        }

        public float GetStartingYSpeed(float startSpeed, float shootAngle, float gravity)
        {
            localGravity = gravity;
            return ySpeed = startSpeed * Mathf.Sin(shootAngle) - gravity;
        }

        public Vector3 Get2DMovement()
        {
            auxMovement = new Vector3(xSpeed, ySpeed, 0);
            UpdateAngledYSpeed();
            return auxMovement;
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
