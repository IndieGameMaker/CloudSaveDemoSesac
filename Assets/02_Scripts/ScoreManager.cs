using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => Debug.Log("로그인 완료");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
