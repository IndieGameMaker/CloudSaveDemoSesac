using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        AuthenticationService.Instance.SignedIn += () => Debug.Log("로그인 완료");
    }
}
