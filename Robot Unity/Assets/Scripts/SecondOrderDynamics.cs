using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SecondOrderDynamics : MonoBehaviour
    {
        Vector3 xp;
        Vector3 y, yd;
        float k1, k2, k3;

        [SerializeField] float f = 1, z = 1, r = 0; // Frequency, Elasticity, Initial response
        [SerializeField] float m = 1; // Max slope
        Vector3 targetVector = Vector3.zero;

        void Start()
        {
            Vector3 x0 = targetVector;
            // Compute constants

            // Initialize variables
            xp = x0;
            y = x0;
            yd = Vector3.zero;
        }

        public void IterateDynamics() // Hell
        {
            k1 = z / (Mathf.PI * f);
            k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
            k3 = r * z / (2 * Mathf.PI * f);

            float T = Time.fixedDeltaTime;
            Vector3 x = targetVector;
            Vector3 xd = (x - xp) / T;
            xp = x;

            float k2Stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);
            y = y + Vector3.ClampMagnitude(T * yd, m);
            yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;

            if (Vector3.Magnitude(y - x) < 0.001) // Stop if were close
            {
                y = x;
            }
        }

        public void SetTargetVector(Vector3 target)
        {
            targetVector = target;
        }

        public Vector3 GetDynamicVector()
        {
            return y;
        }

        public void SetDynamicVector(Vector3 value)
        {
            y = value;
        }

        public void SetInitialDynamicVector(Vector3 initial)
        {
            targetVector = initial;
        }
}
