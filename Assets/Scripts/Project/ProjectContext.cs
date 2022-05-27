using Common.MonoBehaviour;
using UnityEngine;

namespace Project
{
    public class ProjectContext : GenericSingleton<ProjectContext>
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private SoundPlayer _soundPlayer; 
        [SerializeField] private GameLoop _gameLoop; 
        
        public MainCamera MainCamera => _mainCamera;
        public SoundPlayer SoundPlayer => _soundPlayer;
        public GameLoop GameLoop => _gameLoop;
    }
}