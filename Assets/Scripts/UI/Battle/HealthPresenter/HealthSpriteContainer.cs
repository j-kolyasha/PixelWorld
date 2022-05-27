using UnityEngine;

namespace UI.Battle.HealthPresenter
{
    [CreateAssetMenu(fileName = "HealthSpriteContainer", menuName = "ScriptableObjects/HealthSpriteContainer")]
    public class HealthSpriteContainer : ScriptableObject
    {
        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _halfHeart;
        [SerializeField] private Sprite _emptyHeart;

        public Sprite FullHeart => _fullHeart;
        public Sprite HalfHeart => _halfHeart;
        public Sprite EmptyHeart => _emptyHeart;
    }
}