using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Texture2D circleTexture;
    [SerializeField] private float circleSize = 100f;

    [SerializeField] private Texture2D arrowTexture;
    [SerializeField] private Texture2D turnArrow;

    private float _arrowSize = 40f;
    private Vector3 target = Vector3.zero;

    public void SetTarget(Vector3 position)
    {
        target = position;
    }
    
    private void OnGUI()
    {
        if (target == Vector3.zero)
            return;

        Vector3 playerPosition = Camera.main.transform.position;
        
        Vector3 direction = (target - playerPosition).normalized;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPosition + direction * 10f);

        Rect turnRect = new Rect((Screen.width - _arrowSize) / 2f, ((Screen.height - _arrowSize) / 2f), _arrowSize,
            _arrowSize);
        Rect leftRect = new Rect(20f, (Screen.height - _arrowSize) / 2f, _arrowSize, _arrowSize);
        Rect rightRect = new Rect(Screen.width - _arrowSize - 20f, (Screen.height - _arrowSize) / 2f, _arrowSize,
            _arrowSize);
        Rect downRect = new Rect((Screen.width - _arrowSize) / 2f, Screen.height - _arrowSize - 20f, _arrowSize,
            _arrowSize);
        Rect upRect = new Rect((Screen.width - _arrowSize) / 2f, 20f, _arrowSize, _arrowSize);

        if (screenPos.z <= 0)
        {
            //Turn Around
            GUIUtility.RotateAroundPivot(90f, turnRect.center);
            GUI.DrawTexture(turnRect, turnArrow);
            return;
        }

        if (screenPos.x < 0)
        {
            //Turn Left
            GUIUtility.RotateAroundPivot(180f, leftRect.center);
            GUI.DrawTexture(leftRect, arrowTexture);
            return;
        }

        if (screenPos.x > Screen.width)
        {
            //Turn Right
            GUIUtility.RotateAroundPivot(0, rightRect.center);
            GUI.DrawTexture(rightRect, arrowTexture);
            return;
        }

        if (screenPos.y < 0)
        {
            //Turn Down
            GUIUtility.RotateAroundPivot(90f, downRect.center);
            GUI.DrawTexture(downRect, arrowTexture);
            return;
        }

        if (screenPos.y > Screen.height)
        {
            //Turn Up
            GUIUtility.RotateAroundPivot(-90f, upRect.center);
            GUI.DrawTexture(upRect, arrowTexture);
            return;
        }

        float x = screenPos.x;
        float y = Screen.height - screenPos.y;
        GUI.DrawTexture(new Rect(x - circleSize / 2f, y - circleSize / 2f, circleSize, circleSize), circleTexture);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}