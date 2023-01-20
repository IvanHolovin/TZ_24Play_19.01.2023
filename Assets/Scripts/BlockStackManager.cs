using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStackManager : MonoBehaviour
{
    [SerializeField] private Animator _stickManAnimator;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Block _mainBlock;
    public List<Block> _blocksList;
    private Block _lastBlock;

    void Start()
    {
        //_blocksList.Add(_mainBlock);
        _mainBlock.InitBlock(this);
        UpdateLastBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBlock()
    {
        Block newBlock = Instantiate(_blockPrefab);
        newBlock.transform.position = _lastBlock.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        newBlock.transform.SetParent(transform);
        newBlock.InitBlock(this);
        _blocksList.Add(newBlock);
        UpdateLastBlock();
        _stickManAnimator.SetTrigger("Jump");
    }

    public void RemoveBlock(Block blockToRemove)
    {
        _blocksList.Remove(blockToRemove);
        blockToRemove.transform.parent = null;
        if (_blocksList.Count == 0)
        {
            Debug.Log("LOSED_GAME");
        }
        else
        {
            UpdateLastBlock();
        }
        
        
    }

    private void UpdateLastBlock()
    {
        _lastBlock = _blocksList[_blocksList.Count - 1];
    }

}
