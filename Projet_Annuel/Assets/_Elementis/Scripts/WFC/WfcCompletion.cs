using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class WfcCompletion
    {
        /// <summary>
        /// True if all tiles were successfully found.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// The list of tiles to create.
        /// </summary>
        public IList<TileRequest> tileInstances { get; set; }

        /// <summary>
        /// The number of times the generation process was restarted.
        /// </summary>
        public int retries { get; set; }

        /// <summary>
        /// The number of times the generation process backtracked.
        /// </summary>
        public int backtrackCount { get; set; }

        /// <summary>
        /// If success is false, indicates where the generation failed.
        /// </summary>
        public Vector3Int? contradictionLocation { get; set; }

        /// <summary>
        /// Indicates these instances should be added to the previous set of instances.
        /// </summary>
        public bool isIncremental { get; set; }

        /// <summary>
        /// Gives details about the cells.
        /// </summary>
        public IWfcGrid grid { get; set; }

        /// <summary>
        /// Writes error information to Unity's log.
        /// </summary>
        public void LogErrror()
        {
            if (!success)
            {
                if (contradictionLocation != null)
                {
                    var loc = contradictionLocation;
                    PGDebug.Message($"Failed to complete generation, issue at tile {loc}").LogError();
                }
                else
                {
                    PGDebug.Message("Failed to complete generation").LogError();

                }
            }
        }
    }
}