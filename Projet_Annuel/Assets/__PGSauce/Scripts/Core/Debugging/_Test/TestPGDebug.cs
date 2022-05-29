
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.PGDebugging
{
    [HelpURL(DocsPaths.PgLoggerDocumentation)]
    public class TestPGDebug : MonoBehaviour
    {
        public GameObject target;
        [InfoBox("Met l'éditeur en pause au start")]
        public bool pauseEditor = false;
        private void Start()
        {
            PGDebug.Message("Un log normal").Log();
            PGDebug.Message("Un log normal").Message("Un log coloré, plus gros, italique et gras").SetColor(PgColors.White).SetSize(22).SetBold().SetItalic().Log();

            PGDebug.SetContext(target).Message($"Si tu cliques sur ce texte, Unity mettra en valeur l'objet {target.name}").Log();
            
            PGDebug.Message("Un log de warning").LogWarning();
            PGDebug.Message("Un log d'erreur").LogError();

            PGDebug.Message("Todo example").LogTodo();
        }

        private void Update()
        {
            PGDebug.SetCondition(target.transform.position.x > 0).Message($"Ce texte n'apparait que si la position x de {target.name} est positive").Log();
            PGDebug.SetContext(gameObject).SetCondition(pauseEditor).Break();
        }
    }
}
