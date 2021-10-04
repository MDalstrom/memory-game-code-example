using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : Singleton<MenuNavigation>
{
    [SerializeField] private Openable _carouselMenu;
    [SerializeField] private Openable _forParentsMenu;
    [SerializeField] private Openable _privacyPolicyMenu;
    [SerializeField] private Openable _termsOfUseMenu;
    [SerializeField] private Openable _subscriptionBanner;
    [SerializeField] private Openable _gameMenu;
    [SerializeField] private QuizMenu _quizMenu;
    private Openable _currentMenu;
    private List<Openable> _menuTrace;
    public event Action PressedBack;

    public bool IsPlaying => _currentMenu == _gameMenu;

    private void Start()
    {
        _currentMenu = _carouselMenu;
        _menuTrace = new List<Openable>();
    }
    public void Open(Openable menu)
    {
        _menuTrace.Insert(0, _currentMenu);
        _currentMenu.Close();
        _currentMenu = menu;
        _currentMenu.Open();
    }
    public void Back()
    {
        if (_menuTrace.Count == 0)
            return;
        Open(_menuTrace[0]);
        _menuTrace.RemoveAt(0);
        _menuTrace.RemoveAt(0);
        PressedBack?.Invoke();
    }

    public void SafelyOpenForParents()
    {
        _quizMenu.SetSucceededCallback(() => Open(_forParentsMenu));
        _quizMenu.Open();
    }
    public void SafelyOpenPurchasing()
    {
        _quizMenu.SetSucceededCallback(OpenPurchasing);
        _quizMenu.Open();
    }

    public void OpenPurchasing()
    {
#if UNITY_EDITOR || UNITY_IOS
        StoreListener.Instance.TryRestore(result => {
            if (result)
            {
                StoreListener.Instance.ProcessPurchase();
            }
            else
            {
                OpenBanner();
            }
        });
#elif UNITY_ANDROID
        StoreListener.Instance.Purchase();
#endif
    }
    public void OpenBanner()
    {
        Open(_subscriptionBanner);
    }
    public void OpenRateUs()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.identifier);
#endif
    }
    public void OpenOurApps()
    {
        Application.OpenURL("https://martingrey.app/");
    }
    public void OpenPrivacyPolicy()
    {
        Open(_privacyPolicyMenu);
    }
    public void OpenTermsOfUse()
    {
        Open(_termsOfUseMenu);
    }
    public void OpenGameInterface()
    {
        Open(_gameMenu);
    }
}
