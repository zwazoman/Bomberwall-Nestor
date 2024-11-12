using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Move : MonoBehaviour
{
    public async Task StartMoving(Vector2 targetPos, float moveSpeed)
    {
        Vector2 offset = targetPos - (Vector2)transform.position;
        Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        transform.rotation = targetRotation;
        while ((Vector2)transform.position != targetPos)
        {
            Vector2 offset2 = targetPos - (Vector2)transform.position;
            offset2 = Vector2.ClampMagnitude(offset2, Time.deltaTime * moveSpeed);
            transform.Translate(offset2, Space.World);
            await Task.Yield();
        }
    }
}
