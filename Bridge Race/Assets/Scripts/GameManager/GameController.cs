using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private GameObject[] _playerInstantiateTransforms;
    private GameObject[] _brickInstantiateTransfroms;
    
    
    private GameObject[] _bridges;
    
    
    private GameObject[] Bricks;
    private GameObject[] Players;
    
    
    private Material[] _materials;
    
    
    private GameObject _bot;
    private GameObject _player;
    private GameObject _brick;


    private void Awake()
    {
        Assignment();
        Instantiates();
    }

    private void Assignment()
    {
        _brickInstantiateTransfroms = GameObject.FindGameObjectsWithTag("InstantiateTransfroms");
        _playerInstantiateTransforms = GameObject.FindGameObjectsWithTag("PlayerInstantiateTransforms");

        _materials[0].color = new Color(0, 156, 255, 255); //blue
        _materials[1].color = new Color(255, 142, 0, 255); //orange
        _materials[2].color = new Color(255, 0, 181, 255); //pink
        _materials[3].color = new Color(124, 0, 255, 255); //purple
        _materials[4].color = new Color(255, 0, 0, 255);   //red
        _materials[5].color = new Color(255, 232, 0, 255); //yellow

        _bot= Resources.Load<GameObject>("PrefabPlayerFolder/player");
        _player= Resources.Load<GameObject>("PrefabPlayerFolder/player");
        _brick = Resources.Load<GameObject>("PrefabBrickFolder/brick");
    }

    private void Instantiates()
    {
        
        //BOT
        
        for (int i = 0; i < _bridges.Length-1; i++)
        {
            GameObject bot = Instantiate(_bot,
                new Vector3(_playerInstantiateTransforms[i].transform.position.x, 1F, _playerInstantiateTransforms[i].transform.position.z),
                Quaternion.identity);

            Players[Players.Length] = bot;
            
            bot.tag = Convert.ToString(GetComponent<Material>().color);
            _bridges[i].tag = bot.tag;
            bot.GetComponent<Material>().color = _materials[i].color;
        }
        
        //PLAYER

        GameObject player = Instantiate(_player,
            new Vector3(_playerInstantiateTransforms[_playerInstantiateTransforms.Length].transform.position.x, 1F,
                _playerInstantiateTransforms[_playerInstantiateTransforms.Length].transform.position.z), Quaternion.identity);

        _player.GetComponent<Material>().color = _materials[Players.Length - 1].color;
        Players[Players.Length] = player;

        
        //BRICK
        
        for (int i = 0; i < _brickInstantiateTransfroms.Length; i++)
        {
            GameObject brick= Instantiate(_brick,
                new Vector3(_brickInstantiateTransfroms[i].transform.position.x, 0.55F, _brickInstantiateTransfroms[i].transform.position.z),
                Quaternion.identity);

            Bricks[i] = brick;
        }

        for (int i = 0; i < Players.Length; i++)
        {
            for (int j = 0; j < Bricks.Length/Players.Length; j++)
            {
                Bricks[i * Bricks.Length / Players.Length].GetComponent<Material>().color = _materials[i].color;
            }
        }
        
    }

    public void PlaceBrick(GameObject obj)
    {
        for (int i = 0; i < 1; i++)
        {
            int randomPlaceIndex = Random.Range(0, _brickInstantiateTransfroms.Length);

            if (_brickInstantiateTransfroms[randomPlaceIndex].GetComponent<TriggerTransformController>().TransformLock ==
                true)
            {
                i--;
            }
            else if((_brickInstantiateTransfroms[randomPlaceIndex].GetComponent<TriggerTransformController>().TransformLock ==
                     false))
            {
                obj.transform.position = new Vector3(_brickInstantiateTransfroms[randomPlaceIndex].transform.position.x,
                    0.55F, _brickInstantiateTransfroms[randomPlaceIndex].transform.position.z);
            }
        }
        
    }

}
