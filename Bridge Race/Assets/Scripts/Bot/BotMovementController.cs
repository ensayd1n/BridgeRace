using UnityEngine;
using Random = UnityEngine.Random;

public class BotMovementController : MonoBehaviour
{
    private BotOtherController _botOtherController;

    private GameObject[] _brickPool;

    private GameObject _closeBrick;
    private GameObject _currentBridge;
    
    
    private int _bagBrickLimitIndex=0;
    [HideInInspector]
    public int CurrentBrickIndex = 0;
    private int moveBrickMaxIndex = 10;

    public float MoveSpeed;

    private string brickTag;

    public float ClosestDistance = Mathf.Infinity;
    private void Awake()
    {
        _currentBridge = GameObject.FindGameObjectWithTag(brickTag);
        _botOtherController = GetComponent<BotOtherController>();
    }

    private void SearchBricksSetCloseBrick()
    {
        
        _brickPool=GameObject.FindGameObjectsWithTag(brickTag);
        
        if (_brickPool.Length is not 0)
        {
            for (int i = 0; i < _brickPool.Length; i++)
            {
                float _distance = Vector3.Distance(transform.position, _brickPool[i].transform.position);

                if (_brickPool[i] != gameObject)
                {
                    if (_closeBrick == null)
                    {
                        ClosestDistance = _distance;
                        _closeBrick = _brickPool[i];
                    }
                    else if (_closeBrick != null)
                    {
                        if (_distance < ClosestDistance)
                        {
                            ClosestDistance = _distance;
                            _closeBrick = _brickPool[i];
                        }
                    }   
                }
            }
        }
        else return;
    }

    private void SetBrickLimit()
    {
        if (_botOtherController.BrickBag.Length == 0)
        {
            _bagBrickLimitIndex = Random.Range(0, moveBrickMaxIndex);
            CurrentBrickIndex = 0;
        }
        else if(_botOtherController.BrickBag.Length!=0) return;
    }

    private void Move()
    {
        SearchBricksSetCloseBrick();
        SetBrickLimit();

        if (CurrentBrickIndex<_bagBrickLimitIndex)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _closeBrick.transform.position,
                MoveSpeed * Time.deltaTime);
        }
        
        else if (CurrentBrickIndex >= _bagBrickLimitIndex)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _currentBridge.GetComponent<BridgeController>().TargetMoveTransform.transform.position,
                MoveSpeed * Time.deltaTime);
        }
        
        else if (_brickPool.Length == 0)
        {
            SetBrickLimit();
        }
    }

    private void Update()
    {
        Move();
    }
}
