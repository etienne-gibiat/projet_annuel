using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;

namespace PGSauce.Core.PGPhysics
{
    public class ParabolaViewer : MonoBehaviour
    {
        [SerializeField] [Required] private GameObject trajectoryMarkerPrefab;
        [SerializeField] [Required] private GameObject endMarkerPrefab;
        [SerializeField] private float markerStartScale, markerEndScale;
        [SerializeField] private bool showAtStart = true;
        [SerializeField] private bool showFirstMarker;

        private bool _initialized;
        private List<GameObject> _markers;
        private int _count;
        private GameObject _endMarker;

        private bool shown;

        public void Initialize(int resolution, Transform parent = null)
        {
            if (_initialized)
            {
                PGDebug.Message("Already Initialized !!").LogWarning();
                return;
            }

            _initialized = true;
            _markers = new List<GameObject>();
            for (var i = 0; i < resolution; i++)
            {
                var go = Instantiate(trajectoryMarkerPrefab, parent, true);
                _markers.Add(go);
                var t = (float) i / (resolution - 1);
                go.transform.localScale = Vector3.one * Mathf.Lerp(markerStartScale, markerEndScale, t);
            }

            _endMarker = Instantiate(endMarkerPrefab, parent, true);
            _endMarker.transform.localScale = Vector3.one * markerEndScale;
            
            Show(showAtStart);

            _count = resolution;
        }

        public void UpdatePositions(List<Vector3> positions)
        {
            if (!_initialized)
            {
                throw new UnityException("You must initialize the trajectory !");
            }

            if (positions.Count > _count)
            {
                throw new UnityException("Not the same number of points !! (More ...)");
            }

            for (var i = showFirstMarker ? 0 : 1; i < _count; i++)
            {
                _markers[i].SetActive(i < positions.Count && shown);
            }

            for (var i = 0; i < positions.Count; i++)
            {
                _markers[i].transform.position = positions[i];
            }

            var lastRot = Quaternion.identity;

            for (var i = 0; i < positions.Count - 1; i++)
            {
                var pos1 = positions[i];
                var pos2 = positions[i + 1];
                var dir = (pos2 - pos1).normalized;
                if (dir != Vector3.zero)
                {
                    lastRot = Quaternion.LookRotation(dir);
                    _markers[i].transform.rotation = lastRot;
                }
            }

            _markers[positions.Count - 1].transform.rotation = lastRot;

            _endMarker.transform.position = positions[positions.Count - 1];
        }
        
        public void Show(bool value)
        {
            shown = value;
            
            foreach (var marker in _markers)
            {
                marker.SetActive(value);
            }
            
            _markers[0].SetActive(showFirstMarker && value);
            
            _endMarker.SetActive(value);
        }
    }
}
