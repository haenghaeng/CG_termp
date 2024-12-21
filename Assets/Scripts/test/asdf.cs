using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 이게 뭐 하는 컴포넌트인지 적어주세용
/// </summary>
public class asdf : MonoBehaviour
{
    private float timer = 0;
    private WaitForSeconds wfs;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI.text = "Timer : " + timer.ToString();
        wfs = new WaitForSeconds(0.01f);
        StartCoroutine(qwer());
    }

    private IEnumerator qwer()
    {
        while (true)
        {
            yield return wfs;
            timer += 0.01f;
            textMeshProUGUI.text = "Timer : " + timer.ToString();
        }        
    }
}