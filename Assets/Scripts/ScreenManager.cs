using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private Image _cubePrefab;
    [SerializeField] private Canvas _cubeView;
    [SerializeField] private float _spawnInterval = 0.04f;

    [SerializeField] private float _changeColorInterval = 0.2f;
    [SerializeField] private float _timeChangeColorCube = 0.5f;

    private const int COUNT_CUBES_ON_SCENE = 400;
    private List<Image> _cubesOnScene = new ();
    
    private Color _newColor;

    private void GetNewColor()
    {
        _newColor = Random.ColorHSV();
    }

    public void InitializeRecoloringCubes()
    {
        GetNewColor();
        StartCoroutine(RecolorCubes());
    }
    
    private void Awake()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        for (var i = 0; i < COUNT_CUBES_ON_SCENE; i++)
        {
            var cube = Instantiate(_cubePrefab, _cubeView.transform);
            _cubesOnScene.Add(cube);
            
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private IEnumerator RecolorCubes()
    {
        var newColor = _newColor;
        
        for (var i = 0; i <= _cubesOnScene.Count - 1; i++)
        {
            var recoloringCurrentTime = 0f;
            var startColor = _cubesOnScene[i].color;

            while (recoloringCurrentTime <= _timeChangeColorCube)
            {
                _cubesOnScene[i].color =
                    Color.Lerp(startColor, newColor, recoloringCurrentTime / _timeChangeColorCube);

                recoloringCurrentTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(_changeColorInterval);
        }
    }
}
