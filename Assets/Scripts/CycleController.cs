using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CycleController : MonoBehaviour {
    private readonly HashSet<KeyCode> upKeys = new HashSet<KeyCode> {
        KeyCode.W,
        KeyCode.UpArrow,
    };

    private readonly HashSet<KeyCode> rightKeys = new HashSet<KeyCode> {
        KeyCode.D,
        KeyCode.RightArrow,
    };

    private readonly HashSet<KeyCode> downKeys = new HashSet<KeyCode> {
        KeyCode.S,
        KeyCode.DownArrow,
    };

    private readonly HashSet<KeyCode> leftKeys = new HashSet<KeyCode> {
        KeyCode.A,
        KeyCode.LeftArrow,
    };

    [SerializeField] private float moveSpeedX = 5.0f;
    [SerializeField] private float moveSpeedZ = 5.0f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        var keyboardTrigger = gameObject.GetComponent<KeyboardTrigger>();

        keyboardTrigger.OnKeyPressAsObservable()
            .Where(key => upKeys.Contains(key))
            .Subscribe(_ => velocity.z = moveSpeedZ);

        keyboardTrigger.OnKeyPressAsObservable()
            .Where(key => rightKeys.Contains(key))
            .Subscribe(_ => velocity.x = moveSpeedX);

        keyboardTrigger.OnKeyPressAsObservable()
            .Where(key => downKeys.Contains(key))
            .Subscribe(_ => velocity.z = -moveSpeedZ);

        keyboardTrigger.OnKeyPressAsObservable()
            .Where(key => leftKeys.Contains(key))
            .Subscribe(_ => velocity.x = -moveSpeedX);

        keyboardTrigger.OnKeyReleaseAsObservable()
            .Where(key => upKeys.Contains(key))
            .Subscribe(_ => velocity.z = Mathf.Min(0.0f, velocity.z));

        keyboardTrigger.OnKeyReleaseAsObservable()
            .Where(key => rightKeys.Contains(key))
            .Subscribe(_ => velocity.x = Mathf.Min(0.0f, velocity.x));

        keyboardTrigger.OnKeyReleaseAsObservable()
            .Where(key => downKeys.Contains(key))
            .Subscribe(_ => velocity.z = Mathf.Max(0.0f, velocity.z));

        keyboardTrigger.OnKeyReleaseAsObservable()
            .Where(key => leftKeys.Contains(key))
            .Subscribe(_ => velocity.x = Mathf.Max(0.0f, velocity.x));
    }

    private void Update()
    {
        Vector3 position = this.transform.position;
        position += velocity * Time.deltaTime;
        this.transform.position = position;
    }
}
