using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BlockStackManager : MonoBehaviour
{
    [SerializeField] private Transform _gameEssences;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Block _mainBlockPrefab;
    [SerializeField] private GameObject _collectText;
    [SerializeField] private GameObject _collectParticles;
    private Animator _stickManAnimator;
    private List<Block> _blocksList = new List<Block>();
    private Block _lastBlock;
    private Block _topBlockWithStickMan;
    private PlayerMovement _playerMovement;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        SpawnMainBlock();
        UpdateLastBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMainBlock()
    {
        Block newBlock = Instantiate(_mainBlockPrefab);
        newBlock.transform.SetParent(transform);
        newBlock.transform.localPosition = Vector3.zero;
        newBlock.InitBlock(this);
        _blocksList.Add(newBlock);
        _stickManAnimator = newBlock.GetComponentInChildren<Animator>();
        _topBlockWithStickMan = newBlock;
        UpdateLastBlock();
        
    }

    public void AddBlock()
    {
        Block newBlock = Instantiate(_blockPrefab);
        newBlock.transform.position = _lastBlock.transform.position;
        foreach (Transform child in transform)
        {
            child.transform.localPosition = new Vector3(child.transform.localPosition.x, child.transform.localPosition.y + 1f, child.transform.localPosition.z);
        }
        newBlock.transform.SetParent(transform);
        newBlock.InitBlock(this);
        _blocksList.Add(newBlock);
        UpdateLastBlock();
        _stickManAnimator.SetTrigger("Jump");


        GameObject text = Instantiate(_collectText);
        text.transform.position = transform.position;
        text.transform.localPosition = new Vector3(text.transform.localPosition.x,text.transform.localPosition.y + _blocksList.Count,text.transform.localPosition.z);
        text.transform.DOMoveY(text.transform.localPosition.y + 4f, 1.5f);
        text.transform.SetParent(_gameEssences);


        GameObject particles = Instantiate(_collectParticles,newBlock.transform.position,quaternion.identity,_gameEssences);
        text.transform.SetParent(_gameEssences);
    }

    public void RemoveBlock(Block blockToRemove)
    {
        _playerMovement.ShakeCamera();
        _blocksList.Remove(blockToRemove);
        blockToRemove.transform.parent = _gameEssences.transform;
        if (_blocksList.Count == 0)
        {
            GamePlayManager.Instance.GameStateUpdater(GameState.LoseGame);
            _playerMovement.SetOffTrail();
        }
        else
        {
            UpdateLastBlock();
        }

        Block mainBlock = _blocksList.Find(b => b == _topBlockWithStickMan);
        if (mainBlock == null)
        {
            GamePlayManager.Instance.GameStateUpdater(GameState.LoseGame);
            _playerMovement.SetOffTrail();
        }
    }

    private void UpdateLastBlock()
    {
        _lastBlock = _blocksList[_blocksList.Count - 1];
    }

    public void RestartBlocks()
    {
        if (_blocksList.Count > 0)
        {
            foreach (var block in _blocksList)
            {
                if (block != null)
                {
                    Destroy(block.gameObject);  
                }
                
            } 
        }
        foreach (Transform child in _gameEssences.transform)
        {
            GameObject.Destroy(child.gameObject);
        } 
        
        SpawnMainBlock();
    }
    
}
