using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 시작, 재시작, 종료시 필요한 기능을 관리하는 컴포넌트입니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 플레이어가 Start를 향해 발사한 뒤 호출됩니다.
    /// </summary>
    public void StartGame()
    {
        // 플레이어를 막는 벽 제거

        // 몬스터 스폰 시작(EnemySpawner의 public 함수 이용)
    }

    /// <summary>
    /// 플레이어의 체력이 0이 되어 게임오버 되면 호출됩니다.
    /// </summary>
    public void GameOver()
    {
        // 몬스터 스폰 종료(EnemySpawner의 public 함수 이용)

        // 플레이어를 스테이지와 멀리 떨어진 공간으로 순간이동 시킴(몬스터의 간섭을 받지 않기 위해)
        // 이동시킨 공간은 시작 할때와 마찬가지로 플레이어가 빠져나갈 수 없게 벽으로 둘러쌓인 공간

        // 시작때와 같은 방식으로 재시작 물어보는 화면 띄움(재시작, 게임 종료 버튼 2개 표시)
        // 버튼 아래에는 생존 시간을 같이 표시해주면 됩니다.
        // 재시작 = 현재 Scene을 로드
        // 종료 = Application.Quit()
    }
}
