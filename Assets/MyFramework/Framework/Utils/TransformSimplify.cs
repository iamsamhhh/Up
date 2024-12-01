using UnityEngine;

namespace MyFramework
{
    public partial class TransformSimplify
    {
        /// <summary>
        /// 重置操作
        /// </summary>
        /// <param name="trans">Trans.</param>
        public static void Identity(Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.identity;
        }

        public static void SetLocalPosX(Transform transform, float x)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosY(Transform transform, float y)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosZ(Transform transform, float z)
        {
            var localPos = transform.localPosition;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXY(Transform transform, float x, float y)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosXZ(Transform transform, float x, float z)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetLocalPosYZ(Transform transform, float y, float z)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            localPos.z = z;
            transform.localPosition = localPos;
        }

        public static void SetPosX(Transform transform, float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetPosY(Transform transform, float y)
        {
            var localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }

        public static void SetPosZ(Transform transform, float z)
        {
            var position = transform.position;
            position.z = z;
            transform.position = position;
        }

        public static void SetPosXY(Transform transform, float x, float y)
        {
            var position = transform.position;
            position.x = x;
            position.y = y;
            transform.position = position;
        }

        public static void SetPosXZ(Transform transform, float x, float z)
        {
            var position = transform.position;
            position.x = x;
            position.z = z;
            transform.position = position;
        }

        public static void SetPosYZ(Transform transform, float y, float z)
        {
            var position = transform.position;
            position.y = y;
            position.z = z;
            transform.position = position;
        }
        
        public static void AddChild(Transform transform, Transform childTrans)
        {
            childTrans.SetParent(transform);
        }
    }
}