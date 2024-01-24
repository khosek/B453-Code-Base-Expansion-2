using UnityEngine;

namespace Utils
{
    public static class ExtensionMethods
    {
        [SerializeField] private static float _skewedDegree = 225;

        public static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, _skewedDegree, 0));

        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
}