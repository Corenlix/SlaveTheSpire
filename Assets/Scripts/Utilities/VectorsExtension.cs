using UnityEngine;

namespace Utilities
{
    public static class VectorsExtension
    {
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
    }
}