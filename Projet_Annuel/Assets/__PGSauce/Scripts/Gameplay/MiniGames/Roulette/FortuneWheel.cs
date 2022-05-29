using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonKey.Extensions;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace PGSauce.Gameplay.MiniGames.Roulette
{
    public class FortuneWheel : MiniGame<RouletteReward>
    {
        #region Public And Serialized Fields

        [SerializeField, FoldoutGroup(SelectorGroup)]
        private RectTransform selector;
        [SerializeField, FoldoutGroup(SelectorGroup)]
        private SelectorDirection selectorDirection;
        [SerializeField, FoldoutGroup(SelectorGroup)]
        private bool anchorSelectorToCenter;
        [SerializeField, FoldoutGroup(SelectorGroup)]
        private float selectorElasticDuration = 1f;
        [SerializeField, FoldoutGroup(SelectorGroup)]
        private float selectorElasticAmplitude;
        [SerializeField, FoldoutGroup(SelectorGroup)]
        private float selectorElasticPeriod;

        [SerializeField, FoldoutGroup(WheelGroup)]
        private Transform wheel;
        [SerializeField, FoldoutGroup(WheelGroup), Tooltip("Empty space between elements")]
        private float elementSpread;
        [SerializeField, FoldoutGroup(WheelGroup), Tooltip("Space between element and center")]
        private float elementOffset;
        [SerializeField, FoldoutGroup(WheelGroup)]
        private bool equalDistribution;
        
        [SerializeField, FoldoutGroup(KnotsGroup)]
        private Transform knotsParent;
        [SerializeField, FoldoutGroup(KnotsGroup)]
        private float knotsOffset;
        [SerializeField, FoldoutGroup(KnotsGroup)]
        private AudioClip[] soundsKnotsCollision;
        [SerializeField, FoldoutGroup(KnotsGroup)]
        private MinMax<Float01> knotVolumeRange = new MinMax<Float01>() {min = 0.2f, max = 1f};
        [SerializeField, FoldoutGroup(KnotsGroup)]
        private UnityEventAudioClip playKnotSound;
        
        [SerializeField, FoldoutGroup(SimulationGroup)]
        private bool centerOnElement;
        [SerializeField, FoldoutGroup(SimulationGroup), Tooltip("If true, rotates selector instead of wheel (if selector is inside)")]
        private bool rotateSelector;
        [SerializeField, FoldoutGroup(SimulationGroup), Tooltip("How many full laps it does every time it plays")]
        private MinMax<float> rotationCyclesRange = new MinMax<float>() {min = 2, max = 3};
        [SerializeField, FoldoutGroup(SimulationGroup), Tooltip("How long it stays rotating")]
        private MinMax<float> rotationTimeRange = new MinMax<float>() {min = 3, max = 4};
        [SerializeField, FoldoutGroup(SimulationGroup)]
        private float speedUpTime = 0.2f;
        [SerializeField, FoldoutGroup(SimulationGroup)]
        private float friction = 5;
        [SerializeField, FoldoutGroup(SimulationGroup)]
        private UnityEvent onStartSpinning;
        [SerializeField, FoldoutGroup(SimulationGroup)]
        private UnityEvent<int> onFinishSpinning;
        [SerializeField, FoldoutGroup(SimulationGroup), InfoBox("Greater or equals 0 : automatically opens reward after the time, -1 don't get it automatically")]
        private float timeCollectReward;

        [SerializeField, BoxGroup("Debug")] private bool debugLog;

        #endregion
        #region Private Fields
        private const string SelectorGroup = "Selector";
        private const string WheelGroup = "Wheel";
        private const string KnotsGroup = WheelGroup + "/Knots";
        private const string SimulationGroup = "Simulation";

        private List<RectTransform> _knots;
        private bool _isPlaying;
        private float _selectorMaxAngle;
        private float _wheelMaxAngleForKnot;
        private float _selectorElasticStartTime;
        private float _selectorElasticStartAngle;
        private int _forceReward = -1;
        #endregion
        #region Properties
        public float OffsetAngle => 90f * (int) selectorDirection;
        private bool HasBothSelectors => rotateSelector && selector;
        public float ElementOffset => elementOffset;

        #endregion
        #region Unity Functions
        #endregion
        #region Public Methods

        public void StartSpinning()
        {
            StartSpinning(-1);
        }

        protected override void CustomStart()
        {
            base.CustomStart();
            _knots = new List<RectTransform>();
        }

        private void StartSpinning(int forceReward)
        {
            _forceReward = forceReward;
            StartRound();
        }

        protected override void FilterRewards()
        {
            base.FilterRewards();
            Rewards.RemoveAll(r => !r.gameObject.activeInHierarchy);
        }

        protected override void ApplyLayout()
        {
            _forceReward = -1;
            wheel.localRotation = Quaternion.identity;

            var wheelStartAngle = InitRewards();

            InitSelector();
            ApplyLayoutKnots(wheelStartAngle);
            GetSelectorAngles();
        }

        #endregion
        #region Private Methods
        private void StartRound()
        {
            onStartSpinning.Invoke();
            var totalAngle = GetRandomElement();
            StartCoroutine(DoPlay(totalAngle, _forceReward));
            _forceReward = -1;
        }

        private IEnumerator DoPlay(float elementAngle, int targetElementId)
        {
            if (_isPlaying)
            {
                yield break;
            }

            _isPlaying = true;
            
            var initialAngle = 0f;
            if (HasBothSelectors)
            {
                initialAngle = -selector.rotation.eulerAngles.z;
            }
            else
            {
                initialAngle = wheel.rotation.eulerAngles.z;
            }
            

            var totalRotation = Mathf.DeltaAngle(initialAngle, elementAngle);
            if (totalRotation < 0)
            {
                totalRotation += 360f;
            }
            

            var numCycles = rotationCyclesRange.RandomValue();
            totalRotation += Mathf.CeilToInt(numCycles) * 360f;
            

            var totalTime = rotationTimeRange.RandomValue();
            var initialTime = Time.time;

            var rotateBack = false;
            var centerTime = speedUpTime;
            var currentKnot = -1;

            while (true)
            {
                var finished = UpdateRotation(initialAngle, totalRotation, initialTime, totalTime, centerTime);
                if (finished && !rotateBack && selector && _knots.Count > 0)
                {
                    rotateBack = true;
                    var selectorDAngle = Vector3.Angle(selector.localPosition, selector.rotation * Vector3.down);
                    var dAngle = Mathf.Clamp01(selectorDAngle / _selectorMaxAngle);
                    totalTime = 2;
                    initialAngle = elementAngle;
                    totalRotation = -_wheelMaxAngleForKnot * dAngle;

                    initialTime = Time.time;
                    centerTime = totalTime / 2.0f;
                    finished = false;
                }

                var end = UpdateSelector(ref currentKnot, rotateBack);
                if (rotateBack && end)
                {
                    finished = true;
                }

                foreach (var reward in Rewards)
                {
                    reward.SpinningUpdate();
                }

                if (finished)
                {
                    break;
                }

                yield return null;

                if (!_isPlaying)
                {
                    yield break;
                }
            }

            _isPlaying = false;
            onFinishSpinning.Invoke(targetElementId);

            if (timeCollectReward >= 0)
            {
                if (timeCollectReward > 0)
                {
                    yield return new WaitForSeconds(timeCollectReward);
                }

                var reward = Rewards[targetElementId];
                PGDebug.Message($"Selected Reward is {reward.name} ({targetElementId})").Log();
                reward.Execute(this, null);
            }
        }

        private bool UpdateSelector(ref int currentKnot, bool rotateBack)
        {
            if (!selector || _knots.Count <= 0)
            {
                return true;
            }

            if (anchorSelectorToCenter)
            {
                return true;
            }

            var selectorPos = selector.localPosition;
            var knotPos = Vector3.zero;

            var knotRadius = 0f;
            var selectorLength = selector.rect.height * selector.pivot.y;
            var selectorWidth = selector.rect.width * (1 - selector.pivot.x);
            

            var closestKnot = -1;

            for (var i = 0; i < _knots.Count; ++i)
            {
                var knot = _knots[i];
                knotPos = wheel.rotation * knot.localPosition;
                var rect = knot.rect;
                var knotW = rect.width / 2f;
                var knotH = rect.height / 2f;
                knotRadius = Mathf.Sqrt(knotW * knotW + knotH * knotH);
                var wheelToKnot = knotPos;
                var side = Vector3.Cross(wheelToKnot, Vector3.forward).normalized;
                var dir = Vector3.Dot(side, selectorPos);

                if (dir > -selectorWidth - knotRadius &&
                    Vector3.Distance(selectorPos, knotPos) <= selectorLength + knotRadius)
                {
                    closestKnot = i;
                    break;
                }
            }
            

            if (closestKnot >= 0 && closestKnot != currentKnot && !rotateBack)
            {
                AudioClip clip = null;
                if (soundsKnotsCollision.Length > 0)
                {
                    clip = soundsKnotsCollision.GetRandom();
                }

                var volume = knotVolumeRange.RandomValue();
                playKnotSound.Invoke(clip, volume);
            }

            if (!rotateBack)
            {
                currentKnot = closestKnot;
            }
            else if (closestKnot != currentKnot)
            {
                closestKnot = -1;
            }

            float currentSelectorAngle;
            var finished = false;

            if (closestKnot >= 0)
            {
                var side = Vector3.Cross(knotPos - selectorPos, Vector3.forward).normalized;
                knotPos += side * (knotRadius + selectorWidth);

                var selectorAngle = Vector3.Angle (knotPos - selectorPos, Vector3.down);
                var sign = Mathf.Sign(Vector3.Dot(knotPos - selectorPos, Vector3.right));

                currentSelectorAngle = sign * selectorAngle;
                
                _selectorElasticStartTime = Time.time;
                _selectorElasticStartAngle = currentSelectorAngle;
            }
            else
            {
                var baseAngle = Vector3.Angle(-selectorPos, Vector3.down);
                baseAngle *= Mathf.Sign( Vector3.Dot(-selectorPos, Vector3.right ) );

                if (selectorElasticDuration <= 0)
                {
                    selectorElasticDuration = 0.01f;
                }

                var dt = Mathf.Clamp01((Time.time - _selectorElasticStartTime) / selectorElasticDuration);
                currentSelectorAngle = GetElasticOut(dt, _selectorElasticStartAngle, baseAngle,
                    selectorElasticAmplitude, selectorElasticPeriod);
                finished = dt >= 1;
            }

            selector.rotation = Quaternion.Euler(0, 0, currentSelectorAngle);
            return finished;
        }

        private static float GetElasticOut(float t, float a, float b, float amplitude, float period)
        {
            float s;
            if (t <= 0)
            {return a;}

            if (t >= 1)
            {return b;}

            if (period == 0)
            {
                period = 0.3f;
            }

            var value = Mathf.DeltaAngle(a, b);
            if (amplitude == 0 || value > 0 && amplitude < value || value < 0 && amplitude < -value)
            {
                amplitude = value;
                s = period / 4f;
            }
            else
            {
                s = period / (2 * Mathf.PI) * Mathf.Asin(value / amplitude);
            }

            return amplitude * Mathf.Pow(2, -10 * t) * Mathf.Sin((t - s) * (2 * Mathf.PI) / period) + value + a;

        }

        private bool UpdateRotation(float initialAngle, float totalRotation, float initialTime, float totalTime, float centerTime)
        {
            var elapsedTime = Time.time - initialTime;
            var dt = Mathf.Clamp01(elapsedTime / totalTime);
            var easeInToEaseOutRatio = centerTime / totalTime;
            if (dt < easeInToEaseOutRatio)
            {
                //starts slowly and speeds up
                var t = dt / easeInToEaseOutRatio;
                dt = Mathf.Pow(t, friction) * easeInToEaseOutRatio;
            }
            else
            {
                var d = 1 - easeInToEaseOutRatio;
                var t = (dt - easeInToEaseOutRatio) / d;
                dt = 1 - Mathf.Pow(1 - t, friction);
                dt = dt * d + easeInToEaseOutRatio;
            }
            

            var angle = initialAngle + dt * totalRotation;
            var targetAngle = initialAngle + totalRotation;
            

            if (Mathf.Abs(angle - targetAngle) < 0.5f)
            {
                angle = targetAngle;
                dt = 1;
            }

            if (HasBothSelectors)
            {
                var dist = selector.localPosition.magnitude;
                var rot = Quaternion.Euler(0, 0, -angle);
                if (anchorSelectorToCenter)
                {
                    selector.rotation = rot;
                }
                else
                {
                    selector.localPosition = rot * (Vector3.up * dist);
                }
            }
            else
            {
                wheel.rotation = Quaternion.Euler(0, 0, angle);
            }

            var over = dt >= 1;
            

            return over;
        }

        private float GetRandomElement()
        {
            var forceElement = _forceReward;
            _forceReward = 0;
            var angle = 0f;
            var totalPriority = Rewards.Sum(r => r.Probability);
            var randomPos = forceElement >= 0 ? totalPriority + 1 : Random.value * totalPriority;
            if (rotateSelector)
            {
                angle = OffsetAngle + (anchorSelectorToCenter ? 180f : 0f);
            }
            else
            {
                angle = 0f;
            }

            var first = true;
            var totalVisualPriority = equalDistribution ? Rewards.Count : totalPriority;
            for (var i = 0; i < Rewards.Count; ++i)
            {
                var element = Rewards[i];
                var priority = equalDistribution ? 1 : element.Probability;
                var sliceSize = 360f * priority / totalVisualPriority;
                if (first)
                {
                    angle -= sliceSize / 2f;
                    first = false;
                }

                randomPos -= element.Probability;
                if (randomPos < 0 || (forceElement >= 0 && i == forceElement))
                {
                    angle += _wheelMaxAngleForKnot;
                    if (centerOnElement)
                    {
                        angle += 0.5f * sliceSize;
                    }
                    else
                    {
                        angle += Mathf.Lerp(0.2f, 0.8f, Random.value) *
                                 (sliceSize - elementSpread - _wheelMaxAngleForKnot);
                    }
                    _forceReward = i;
                    return angle;
                }
                else
                {
                    angle += sliceSize;
                }
            }

            return angle;
        }

        /// <summary>
        ///  Highest angle the selector reaches because of the knots
        /// </summary>
        private void GetSelectorAngles()
        {
            var wheelToSelector = selector ? selector.localPosition.magnitude : 0;
            if (_knots.Count <= 0 || anchorSelectorToCenter || wheelToSelector <= 0)
            {
                _selectorMaxAngle = 0;
                _wheelMaxAngleForKnot = 0;
                return;
            }

            var selectorPivot = Vector3.up * wheelToSelector;
            var knot = _knots[0];
            var vKnot = knot.localPosition;
            var wheelToKnotSq = vKnot.sqrMagnitude;
            var wheelToSelectorSq = wheelToSelector * wheelToSelector;
            var selectorLength = selector.rect.height * selector.pivot.y;
            var selectorLengthSq = selectorLength * selectorLength;
            var y = (wheelToKnotSq + wheelToSelectorSq - selectorLengthSq) / (2 * selectorPivot.y);
            var x = Mathf.Sqrt(Mathf.Abs(wheelToKnotSq - y * y));
            var farthestPoint = new Vector3(x, y, selectorPivot.z);

            _selectorMaxAngle = Vector3.Angle(-selectorPivot, farthestPoint - selectorPivot);

            var rect = knot.rect;
            var knotHalfWidth = rect.width / 2f;
            var knotHalfHeight = rect.height / 2f;
            var knotRadius = Mathf.Sqrt(knotHalfWidth * knotHalfWidth + knotHalfHeight * knotHalfHeight);
            var selectorWidth = selector.rect.width * (1 - selector.pivot.x);
            _selectorMaxAngle += Mathf.Atan((selectorWidth + knotRadius) / selectorLength) * Mathf.Rad2Deg;
            _wheelMaxAngleForKnot = Mathf.Atan((farthestPoint.x + selectorWidth + knotRadius) / vKnot.magnitude) *
                                    Mathf.Rad2Deg;
        }
        private void ApplyLayoutKnots(float wheelStartAngle)
        {
            _knots.Clear();
            if (!knotsParent)
            {
                return;
            }
            
            PGDebug.Message($"Wheel Start Angle is {wheelStartAngle}").Log();

            foreach (RectTransform knot in knotsParent)
            {
                if (knot.gameObject.activeInHierarchy)
                {
                    _knots.Add(knot);
                }
            }

            for (var i = 0; i < _knots.Count; i++)
            {
                var knot = _knots[i];
                var angle = wheelStartAngle + knotsOffset - i * 360f / (_knots.Count - 1);
                knot.localRotation = Quaternion.Euler(0,0,angle);
                knot.localPosition = Quaternion.Euler(0, 0, angle) * (Vector3.up * knot.localPosition.magnitude);
            }
        }
        private float InitRewards()
        {
            var rewardsCount = Rewards.Count;
            var totalPriority = equalDistribution ? rewardsCount : Rewards.Sum(r => r.Probability);
            var angle = 0f;
            var first = true;
            var wheelStartAngle = 0f;
            foreach (var reward in Rewards)
            {
                if (reward.Separator)
                {
                    reward.Separator.gameObject.SetActive(true);
                }

                var priority = equalDistribution ? 1 : reward.Probability;
                var sliceSize = 360f * priority / totalPriority;

                if (first)
                {
                    angle -= sliceSize / 2f;
                    wheelStartAngle = angle;
                    first = false;
                }

                reward.ApplyLayout(angle, sliceSize - elementSpread, this);
                angle += sliceSize;
            }

            return wheelStartAngle;
        }

        private void InitSelector()
        {
            if (selector)
            {
                var offsetAngle = OffsetAngle;
                if (anchorSelectorToCenter)
                {
                    selector.localRotation = Quaternion.Euler(0, 0, 180 - offsetAngle);
                    selector.localPosition = Vector3.zero;
                }
                else
                {
                    selector.localRotation = Quaternion.Euler(0, 0, -offsetAngle);
                    selector.localPosition = Quaternion.Euler(0, 0, -offsetAngle) *
                                             (Vector3.up * selector.localPosition.magnitude);
                }

                _selectorElasticStartTime = -1000;
                _selectorElasticStartAngle = selector.rotation.eulerAngles.z;
            }
        }
        #endregion

        private enum SelectorDirection
        {
            Top,
            Right,
            Bottom,
            Left
        }
    }
}
