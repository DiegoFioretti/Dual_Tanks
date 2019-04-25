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
    }
}
