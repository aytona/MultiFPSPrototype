﻿using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;

    #endregion Singleton

    #region Variables

    public MatchSettings matchSettings;

    private const string PLAYER_ID_PREFIX = "Player ";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    #endregion Variables

    #region Monobehaviour

    void Awake()
    {
        if (instance != null)
            instance = this;
    }

    #endregion Monobehaviour

    #region Public Methods

    public static void RegisterPlayer (string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void DeregisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer (string _playerID)
    {
        return players[_playerID];
    }

    #endregion Public Methods
}
