using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using UnityEngine.UI;

public class CloudSaveManager : MonoBehaviour
{
    public Button loginButton;

    private async void Awake()
    {
        // UGS 초기화 성공했을 때 호출되는 콜백
        UnityServices.Initialized += () =>
        {
            Debug.Log("UGS 초기화 성공");
        };

        UnityServices.InitializeFailed += (ex) =>
        {
            Debug.Log($"UGS 초기화 실패 : {ex.Message}");
        };

        // UGS 초기화
        await UnityServices.InitializeAsync();

        // 익명 사용자로 로그인 성공했을 때 호출되는 콜백
        AuthenticationService.Instance.SignedIn += () =>
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"익명 로그인 성공\nPlayerId: <color=#00ff00>{playerId}</color>");
        };

        // 익명 로그인
        await SignIn();
    }

    private async Task SignIn()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
