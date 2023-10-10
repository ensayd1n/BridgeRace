using System;
using UnityEngine;

public class BotOtherController : MonoBehaviour
{
    private BotMovementController _botMovementController;
    private GameController _gameController;
    
    public GameObject[] BrickBag;
    
    private Color _currentBotColor;
    private string _currentBrickColor;
    
    private void Awake()
    {
        _botMovementController = GetComponent<BotMovementController>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
        _currentBotColor = gameObject.GetComponent<Material>().color;
        _currentBrickColor = Convert.ToString(_currentBotColor);
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameObject rival = collision.gameObject;

                if (rival.GetComponent<BotOtherController>())
                {
                    if (rival.GetComponent<BotOtherController>().BrickBag.Length > BrickBag.Length)
                    {
                        DroppBrick();
                        _botMovementController.CurrentBrickIndex = 0;
                    }
                }
                else if (!rival.GetComponent<BotOtherController>())
                {
                    if (rival.GetComponent<PlayerOtherController>().BrickBag.Length > BrickBag.Length)
                    {
                        DroppBrick();
                        _botMovementController.CurrentBrickIndex = 0;
                    }
                }
            }
            else return;
        }

        else if (collision.gameObject.CompareTag("Bridge"))
        {
            GameObject bridgeStep = collision.gameObject;

            if (bridgeStep.GetComponent<Material>().color.a != 100)
            {
                BuildBridge(bridgeStep);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_currentBrickColor))
        {
            AddBrickBag(other.gameObject);
        }
    }

    private void DroppBrick()
    {
        for (int i = 0; i < BrickBag.Length; i++)
        {
            Destroy(BrickBag[i]);
            GameObject lastBrick = BrickBag[BrickBag.Length];

            lastBrick.GetComponent<Rigidbody>().useGravity = true;

            Destroy(BrickBag[BrickBag.Length]);
         
        }
    }

    private void AddBrickBag(GameObject brick)
    {
        BrickBag[BrickBag.Length+1] = brick;
        _botMovementController.CurrentBrickIndex ++;
    }
    
    private void BuildBridge(GameObject bridgeStep)
    {
        if (BrickBag.Length != 0)
        {
            bridgeStep.GetComponent<Material>().color =
                new Color(_currentBotColor.r, _currentBotColor.g, _currentBotColor.b, 100);

            PlaceBrick();
            Destroy(BrickBag[BrickBag.Length]);
        }
        else
        {
            return;
        }
    }
    private void PlaceBrick()
    {
        _gameController.PlaceBrick(BrickBag[BrickBag.Length]);
    }
}
