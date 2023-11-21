﻿using UnityEngine;

namespace Runtime.Commands.Level
{
      class OnLevelLoaderCommand
    {
        private Transform _levelHolder;
        public OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level{levelIndex}"), _levelHolder, true);
        }
    }
}