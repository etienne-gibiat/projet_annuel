using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core
{
    public interface ITextTemplateInterface : ITextTemplateBase
    {
        string SUBNAMESPACE();
        string TEMPLATENAME();
        string METHODS();
    }
}
