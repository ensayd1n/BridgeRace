using UnityEngine;
using DG.Tweening;

public class FinishGroundController : MonoBehaviour
{
    public GameObject LevelComplatedPanel;
    public GameData GameData;
    public GameManager GameManager;

    public Transform BricksMoveTransform;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player= other.gameObject;


            PlayerOtherController _playerOtherController = player.GetComponent<PlayerOtherController>();

            if (_playerOtherController)
            {
                for (int i = 0; i < _playerOtherController.BrickBag.Length; i++)
                {
                    _playerOtherController.BrickBag[i].transform.DOMove(BricksMoveTransform.position, 0.1F);

                    if (i + 1 == _playerOtherController.BrickBag.Length)
                    {
                        LevelComplatedPanel.SetActive(true);
                        
                        GameData.Level++;
                        GameData.TakenBrick += _playerOtherController.BrickBag.Length;
                        GameManager.Save();
                    }
                }
            }
        }
    }
}
