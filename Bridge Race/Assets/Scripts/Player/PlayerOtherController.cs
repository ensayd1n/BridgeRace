using UnityEngine;

public class PlayerOtherController : MonoBehaviour
{
   private GameController _gameController;
   
   public GameObject[] BrickBag;

   private Color _currentPlayerColor;

   private void Awake()
   {
      _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
      _currentPlayerColor = gameObject.GetComponent<Material>().color;
   }


   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Ground"))
      {
         if (collision.gameObject.CompareTag("Player"))
         {
            GameObject rival = collision.gameObject;

            if (rival.GetComponent<BotOtherController>().BrickBag.Length > BrickBag.Length)
            {
               DroppBrick();
            }
         }
         else return;
      }

      else if (collision.gameObject.CompareTag("Bridge"))
      {
         GameObject bridgeStep = collision.gameObject;
         
         if (bridgeStep.GetComponent<Material>().color.a != 255)
         {
            BuildBridge(bridgeStep);
         }
         else return;
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
   private void BuildBridge(GameObject bridgeStep)
   {
      if (BrickBag.Length != 0)
      {
         bridgeStep.GetComponent<Material>().color =
            new Color(_currentPlayerColor.r, _currentPlayerColor.g, _currentPlayerColor.b, 255);

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
