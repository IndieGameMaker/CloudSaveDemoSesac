using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Core;

public class CloudSaveManager : MonoBehaviour
{
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

    }
}
