using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button saveScoreButton;
    [SerializeField] private TMP_InputField scoreIf;

    private const string leaderboardId = "Ranking";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => Debug.Log("로그인 완료");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        // Add Score 버튼 클릭 이벤트 연결
        saveScoreButton.onClick.AddListener(async () => await AddScore(int.Parse(scoreIf.text)));
    }

    private async Task AddScore(int score)
    {
        var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);

        Debug.Log(JsonConvert.SerializeObject(response));
    }
}
