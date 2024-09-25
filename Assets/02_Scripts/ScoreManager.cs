using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button saveScoreButton;
    [SerializeField] private TMP_InputField scoreIf;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => Debug.Log("로그인 완료");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
