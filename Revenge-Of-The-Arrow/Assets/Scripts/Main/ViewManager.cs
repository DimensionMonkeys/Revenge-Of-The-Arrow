using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{

    private static ViewManager v_instance;
    [SerializeField] private View _startingView;
    [SerializeField] private View[] _views;
    private View _currentView;
    private readonly Stack<View> _history = new Stack<View>();

    public static T GetView<T>() where T : View
    {
        for(int i = 0; i < v_instance._views.Length; i++)
        {
            if(v_instance._views[i] is T tView)
            {
                return tView;
            }
        }

        return null;
    }

    public static void Show<T>(bool remember = true) where T : View
    {
        for(int i = 0; i< v_instance._views.Length; i++)
        {
            if (v_instance._views[i] is T)
            {
                if(v_instance._currentView != null)
                {
                    if (remember)
                    {
                        v_instance._history.Push(v_instance._currentView);
                    }
                    v_instance._currentView.Hide();
                }
                v_instance._views[i].Show();
                v_instance._currentView = v_instance._views[i];
            }
        }
    }

    public static void Show(View view, bool remember = true)
    {
        if(v_instance._currentView != null)
        {
            if (remember)
            {
                v_instance._history.Push(v_instance._currentView);
            }
            v_instance._currentView.Hide();
        }

        view.Show();
        v_instance._currentView = view;
    }

    public static void Show(View viewTab, View view)
    {
        viewTab.Hide();
        view.Show();
    }

    // stack remove view and return it
    public static void showLast()
    {
        if(v_instance._history.Count != 0)
        {
            Show(v_instance._history.Pop(), false);
        }
    }

    public static void showBeforeLast()
    {
        v_instance._history.Pop();
        if (v_instance._history.Count != 0)
        {
            Show(v_instance._history.Pop(), false);
        }
    }

    private void Awake()
    {
        v_instance = this;
    }

    private void Start()
    {
        for(int i = 0; i< _views.Length; i++)
        {
            _views[i].Initialize();
            _views[i].Hide();
        }
        
        if(_startingView != null)
        {
            Show(_startingView, true);
        }
    }
}
