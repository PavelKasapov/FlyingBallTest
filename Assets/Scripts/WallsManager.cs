using System.Collections;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    private const float Speed = 15f;
    private const int WallPartWigth = 18;
    private const int TotalWallParts = 3;
    private const int TotalObstacles = 6;

    private readonly float[] obstacleVerticalLimits = { -3.5f, 8.5f };

    private GameObject _wallPartPrefab;
    private GameObject _obstaclePrefab;

    private GameObject[] _wallPartsPool = new GameObject[TotalWallParts];
    private GameObject[] _obstaclePool = new GameObject[TotalObstacles];

    private void Awake()
    {
        _wallPartPrefab = Resources.Load<GameObject>("Prefabs/WallPart");
        _obstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle");
    }

    public void SetupWalls()
    {
        for (int i = 0; i < TotalWallParts; i++)
        {
            _wallPartsPool[i] = Instantiate(_wallPartPrefab, new Vector2(WallPartWigth * i, 0), Quaternion.identity, transform);
        }
        for (int i = 0; i < TotalObstacles; i++)
        {
            _obstaclePool[i] = Instantiate(_obstaclePrefab, new Vector2(WallPartWigth * (i + 3) * TotalWallParts / TotalObstacles, 0), Quaternion.identity, transform);
            RandomizeObstaclePlace(_obstaclePool[i].transform);
        }
    }

    public void StartMoving()
    {
        for (int i = 0; i < TotalWallParts; i++)
        {
            StartCoroutine(LevelPartMove(_wallPartsPool[i]));
        }
        for (int i = 0; i < TotalObstacles; i++)
        {
            StartCoroutine(LevelPartMove(_obstaclePool[i], true));
        }
    }

    public void StopMoving()
    {
        StopAllCoroutines();
    }

    IEnumerator LevelPartMove(GameObject levelPart, bool needVerticalRandom = false)
    {
        var levelPartTransform = levelPart.transform;
        while (true)
        {
            levelPartTransform.Translate(Vector2.left * Speed * Time.fixedDeltaTime);
            if (levelPartTransform.position.x <= -WallPartWigth * 1)
            {
                ReplaceTowards(levelPartTransform);
                if (needVerticalRandom)
                {
                    RandomizeObstaclePlace(levelPartTransform);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void ReplaceTowards(Transform wallPartTransform)
    {
        wallPartTransform.position = new Vector3(WallPartWigth * (TotalWallParts - 1), wallPartTransform.position.y);
    }

    private void RandomizeObstaclePlace(Transform obstacleTransform)
    {
        obstacleTransform.position = new Vector3(obstacleTransform.position.x, Random.Range(obstacleVerticalLimits[0], obstacleVerticalLimits[1]));
    }
}
