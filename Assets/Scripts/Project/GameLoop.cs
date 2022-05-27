using System.Collections;
using Common.MonoBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class GameLoop : CashedMonoBehaviour
    {
        [SerializeField] private Character.Character _character;

        protected override void InheritStart()
        {
            base.InheritStart();

            _character.Death += RestartLevel;
        }

        protected override void InheritOnDestroy()
        {
            base.InheritOnDestroy();
            
            _character.Death -= RestartLevel;
        }

        private void RestartLevel(Character.Character character)
        {
            StartCoroutine(Restart());
        }

        private IEnumerator Restart()
        {
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}