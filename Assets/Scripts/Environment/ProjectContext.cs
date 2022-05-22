using Common.MonoBehaviour;
using UnityEngine;

namespace Environment
{
    public class ProjectContext : GenericSingleton<ProjectContext>
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private SoundPlayer _soundPlayer; 
        
        public MainCamera MainCamera => _mainCamera;
        public SoundPlayer SoundPlayer => _soundPlayer;
    }
}