using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{

    [SerializeField,Range(4,100)] private int _poolCount;    
    [SerializeField] private List<Track> _prefabTrackList;
    [SerializeField] private Track _startTrack;

    private List<Track> _poolTracks = new List<Track>();
    private Track _lastSpawned;

    public float SpawnNewTrackPoint()
    {
        if (_poolTracks.Count > 3)
        {
            return _poolTracks[_poolTracks.Count - 3].transform.position.z;
        }
        else
        {
            return _poolTracks[_poolTracks.Count - 1].transform.position.z;
        }
    }

    void Awake()
    {
        _poolTracks.Add(_startTrack);
        _lastSpawned = _startTrack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitializeTracks()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomNumber = Random.Range(0, _prefabTrackList.Count - 1);
            Track newTrack = Instantiate(_prefabTrackList[randomNumber],
                new Vector3(_lastSpawned.transform.position.x, _lastSpawned.transform.position.y,
                    _lastSpawned.transform.position.z + 30), Quaternion.identity, transform);
            _poolTracks.Add(newTrack);
            _lastSpawned = newTrack;
        }
    }

    public void SpawnNewPart()
    {
        if (_poolTracks.Count < _poolCount)
        {
            int randomNumber = Random.Range(0, _prefabTrackList.Count - 1);
            Track newTrack = Instantiate(_prefabTrackList[randomNumber],
                new Vector3(_lastSpawned.transform.position.x, _lastSpawned.transform.position.y,
                    _lastSpawned.transform.position.z + 30), Quaternion.identity, transform);
            _poolTracks.Add(newTrack);
            _lastSpawned = newTrack;
        }
        else
        {
            int randomNumber = Random.Range(1, _poolTracks.Count - 4);
            Track toReplace = _poolTracks[randomNumber];
            _poolTracks.Remove(toReplace);
            toReplace.ResetTrack();
            toReplace.transform.position = new Vector3(_lastSpawned.transform.position.x,
                _lastSpawned.transform.position.y,
                _lastSpawned.transform.position.z + 30);
            _poolTracks.Add(toReplace);
            _lastSpawned = toReplace;
        }
    }

    public void RestartTracks()
    {
        _poolTracks.Remove(_startTrack);
        
        // for (int i = 1; i < _poolTracks.Count; i++)
        // {
        //     Destroy(_poolTracks[i].transform.gameObject);
        //     _poolTracks.Remove(_poolTracks[i]);
        // }
        foreach (var track in _poolTracks)
        {
            Destroy(track.transform.gameObject);
        }
        _poolTracks.Clear();
        
        _poolTracks.Add(_startTrack);
        _lastSpawned = _startTrack;
        //_poolTracks.Clear();
        InitializeTracks();
    }
}
