using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Services.CloudSave;

[System.Serializable]
public struct PlayerData
{
    public string name;
    public int level;
    public int xp;
    public int gold;

    public List<ItemData> items;
}

[Serializable]
public struct ItemData
{
    public string name;
    public int value;
    public int count;
    public string icon;
}

public class CloudSaveManager : MonoBehaviour
{
    [Header("UI Button")]
    public Button loginButton;
    public Button saveSingleDataButton;
    public Button saveMultiDataButton;

    [Header("Player Data")]
    public PlayerData playerData;

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

        // 로그인 버튼 클릭 이벤트 연결
        loginButton.onClick.AddListener(async () =>
        {
            await SignIn();
        });
        // 싱글 데이터 버튼 클릭 이벤트 연결
        saveSingleDataButton.onClick.AddListener(async () =>
        {
            await SaveSingleData();
        });
        // 멀티 데이터 버튼 클릭
        saveMultiDataButton.onClick.AddListener(async () =>
        {
            await SaveMultiData<PlayerData>("PlayerData", playerData);
        });

    }

    private async Task SignIn()
    {
        if (AuthenticationService.Instance.IsSignedIn) return;

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    // 단일 데이터 저장
    private async Task SaveSingleData()
    {
        // 저장할 데이터 선언
        var data = new Dictionary<string, object>
        {
            {"player_name", "Zackiller"},
            {"level", 30},
            {"xp", 2000},
            {"gold", 100}
        };

        // Cloud 저장
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

        Debug.Log("싱글 데이터 저장 완료");
    }


    // 복수 데이터 저장
    private async Task SaveMultiData<T>(string key, T saveData)
    {
        // 딕셔너리 타입의 자료형으로 저장
        var data = new Dictionary<string, object>
        {
            {key, saveData}
        };

        // 저장 메소드
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("복수 데이터 저장 완료");
    }
}
