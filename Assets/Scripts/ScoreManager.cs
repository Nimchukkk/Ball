using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private int _score;
    
    public void StartAddScore() => StartCoroutine(nameof(AddScore));

    private IEnumerator AddScore()
    {
        const int addScore = 50;
        for (var i = 0; i < addScore; i++)
        {
            _score ++;
            _textMesh.text = _score.ToString();
            
            _textMesh.fontSize += 10;
            
            yield return new WaitForSeconds(0.01f);
            
            _textMesh.fontSize -= 10;
        }
    }
}
