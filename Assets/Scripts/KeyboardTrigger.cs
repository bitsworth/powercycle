using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class KeyboardTrigger : ObservableTriggerBase {
    private Subject<KeyCode> onKeyPress = new Subject<KeyCode>();
    private Subject<KeyCode> onKeyRelease = new Subject<KeyCode>();

    [SerializeField] private KeyCode[] keys = new KeyCode[] {};

    private void Start()
    {
        this.UpdateAsObservable()
            .SelectMany(_ => keys.ToObservable())
            .Where(key => Input.GetKeyDown(key))
            .Do(key => Debug.Log(key + "pressed!"))
            .Subscribe(onKeyPress);

        this.UpdateAsObservable()
            .SelectMany(_ => keys.ToObservable())
            .Where(key => Input.GetKeyUp(key))
            .Do(key => Debug.Log(key + "released!"))
            .Subscribe(onKeyRelease);
    }

    public IObservable<KeyCode> OnKeyPressAsObservable()
    {
        return onKeyPress;
    }

    public IObservable<KeyCode> OnKeyReleaseAsObservable()
    {
        return onKeyRelease;
    }

    protected override void RaiseOnCompletedOnDestroy()
    {
        onKeyPress.OnCompleted();
        onKeyRelease.OnCompleted();
    }
}
