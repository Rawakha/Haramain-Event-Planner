using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectDisplay : MonoBehaviour
{
    private RectTransform rectTransform;

    private void OnDrawGizmosSelected()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        if (rectTransform != null)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Gizmos.color = Color.green;
            for (int i = 0; i < corners.Length; i++)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % corners.Length]);
            }
        }
    }
}