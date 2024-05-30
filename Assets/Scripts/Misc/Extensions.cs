using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ExtensionMethods
{
    public static class DebugExtensions
    {
        public static void DrawCrossOnPoint(Vector3 point, float size, Color color, float duration)
        {
            float halfSize = size / 2;
            Debug.DrawRay(point, Vector2.up * size, color, duration);
            Debug.DrawRay(point, Vector2.down * size, color, duration);
            Debug.DrawRay(point, Vector2.right * size, color, duration);
            Debug.DrawRay(point, Vector2.left * size, color, duration);
        }
    }

    public static class Copier
    {
        public static void CopyComponentSerializedValues<T>(T sourceComp, T targetComp)
        {
            string json = JsonUtility.ToJson(sourceComp);
            JsonUtility.FromJsonOverwrite(json, targetComp);
        }

        public static void CopyComponentValues<T>(T sourceComp, T targetComp)
        {
            FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.Public |
                                                             BindingFlags.NonPublic |
                                                             BindingFlags.Instance);
            int i = 0;
            for (i = 0; i < sourceFields.Length; i++)
            {
                var value = sourceFields[i].GetValue(sourceComp);
                sourceFields[i].SetValue(targetComp, value);
            }

            string json = JsonUtility.ToJson(sourceComp);
            Debug.Log(json);
            //JsonUtility.FromJsonOverwrite(json, to);

        }

    }

    public static class ArrayExtensions
    {
        public static T GetRandom<T>(T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }

    public static class LayerMaskExtensions
    {
        public static bool IsInLayerMask(this LayerMask mask, int layer)
        {
            return ((mask.value & (1 << layer)) > 0);
        }

        public static bool IsInLayerMask(this LayerMask mask, GameObject obj)
        {
            return ((mask.value & (1 << obj.layer)) > 0);
        }
    }

    public static class MathExtensions
    {

        public static float RoundToMultiple(float number, float multiple)
        {
            return Mathf.Round(number / multiple) * multiple;
        }

        public static float FloorToMultiple(float number, float multiple)
        {
            return Mathf.Floor(number / multiple) * multiple;
        }


        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            float t = Mathf.InverseLerp(fromMin, fromMax, value);
            return Mathf.Lerp(toMin, toMax, t);
        }
    }

    public static class VectorExtensions
    {

        public static Vector2 Rotate(this Vector2 vector, float angle)
        {
            Vector2 rotatedVector = Quaternion.AngleAxis(angle, Vector3.forward) * vector;
            return rotatedVector;
        }

        public static Vector3 RotateAround(this Vector3 vector, float angle, Vector3 axis)
        {
            float oldAngle = vector.GetAngle();
            float newAngle = oldAngle + angle;
            Vector2 rotatedVector = Quaternion.AngleAxis(newAngle, axis) * vector;
            return rotatedVector;
        }

        public static Vector2 PolarToVector(float radius, float angle)
        {
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            return new Vector2(x, y);
        }

        public static void ToPolar(this Vector3 vector, out float radius, out float angle)
        {
            radius = vector.magnitude;
            angle = vector.GetAngle();
        }

        public static float GetAngle(this Vector3 vector) // Returns an angle between 0 and 360 degrees
        {
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                return 360 + angle;
            }

            return angle;
        }

        public static float GetAngle(this Vector2 vector) // Returns an angle between 0 and 360 degrees
        {
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                return 360 + angle;
            }

            return angle;
        }
    }

    public static class LineRendererExtensions
    {
        public static void SetColor(this LineRenderer lineRenderer, Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;

        }

    }

    public static class TransformExtensions
    {
        public static Vector3[] TransformsToVectors(this Transform[] transforms)
        {
            Vector3[] positions = new Vector3[transforms.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = transforms[i].position;
            }

            return positions;
        }

    }

    public static class AnimatorExtensions
    {
        public static IEnumerator WaitForCurrentAnimatorState(this Animator animator, int layerIndex)
        {
            yield return null;
            yield return new WaitWhile(() =>
            {
                return animator.IsInTransition(layerIndex);
            });
            int statePathHash = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            yield return new WaitWhile(() =>
            {
                AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return animatorStateInfo.normalizedTime < 1 && animatorStateInfo.fullPathHash == statePathHash;
            });

        }

    }

    public static class Randomizer
    {
        public static T[] Randomize<T>(T[] items)
        {
            // For each spot in the array, pick
            // a random item to swap into that spot.
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = Random.Range(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }

            return items;
        }
    }
}
