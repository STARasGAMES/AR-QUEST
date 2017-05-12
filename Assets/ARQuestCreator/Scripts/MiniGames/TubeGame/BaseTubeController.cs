using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARQuestCreator.MiniGames.TubeGame
{
    public abstract class BaseTubeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public enum Direction
        {
            Up = 1,
            Down = -1,
            Right = 1,
            Left = -1
        }
        private const float _fillDecrease = 0.5f;
        protected bool _isFilled = false;
        protected float _fillAmount;
        protected Transform _child;
        protected IFillController _fillController;
        protected TubeGameManager _tubeGameManager;
        protected List<Vector2> _giveDirections;
        protected List<BaseTubeController> _nearTubes;
        public Vector2 position { get; private set; }
        private bool _isAddedFill = false;

        public delegate void OnFilled();
        public OnFilled onFilledEventHandler;

        protected virtual void Update()
        {
            BaseUpdate();
        }

        public void Init()
        {
            _tubeGameManager = GetComponentInParent<TubeGameManager>();
            _fillController = GetComponentInChildren<IFillController>();
            position = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y);
            _child = transform.GetChild(0);
            _tubeGameManager.AddTube(this);
            _giveDirections = new List<Vector2>();
            ResetGiveDirections();
        }

        protected virtual void BaseUpdate()
        {
            if (!_isAddedFill)
            {
                _fillAmount -= 1 * Time.deltaTime;
            }
            else
            {
                if (_isFilled && onFilledEventHandler != null)
                {
                    onFilledEventHandler();
                }
            
            }
            _fillController.SetFillAmount(_fillAmount);
            _isAddedFill = false;
        }

        public void AddFill(float amount)
        {
            if (_isAddedFill)
                return;

            _isAddedFill = true;
            _fillAmount += amount;
            _fillAmount = Mathf.Clamp(_fillAmount, 0f, 1f);
            _isFilled = _fillAmount == 1;
            if (isFilled() && _isAddedFill)
            {
                //Debug.Log("BASE UPDATE FOR " + name);
                List<Vector2> giveDirections = GetGiveDirections();
                BaseTubeController tube;
                var nearTubes = GetNearTubes();
                for (int i1 = 0; i1 < nearTubes.Count; i1++)
                {
                    tube = nearTubes[i1];
                    Vector2 v1;
                    Vector2 v2;
                    var directions = tube.GetGiveDirections();
                    for (int i2=0; i2<directions.Count; i2++)
                    {
                        v1 = directions[i2];
                        for (int i3=0; i3<giveDirections.Count; i3++)
                        {
                            v2 = giveDirections[i3];
                            bool some1 = IsVectorsInverse(v1, v2);
                            bool some2 = IsSameVectorDirection(tube.position - position, v2);
                            bool someBool = some1 && some2;
                            //Debug.Log("Tube: " + tube.name + "  " + v1 + " " + v2 + " " + (tube.position - position) + " " + someBool.ToString() + " " + some1.ToString() + " " + some2.ToString());
                            if (someBool)
                            {
                                tube.AddFill(amount);
                            }
                        }
                    }
                  
                }
                //foreach (BaseTubeController tube in GetNearTubes())
                //{
                //    foreach (var v1 in tube.GetGiveDirections())
                //    {
                //        foreach (var v2 in giveDirections)
                //        {
                //            bool some1 = IsVectorsInverse(v1, v2);
                //            bool some2 = IsSameVectorDirection(tube.position - position, v2);
                //            bool someBool = some1 && some2;
                //            //Debug.Log("Tube: " + tube.name + "  " + v1 + " " + v2 + " " + (tube.position - position) + " " + someBool.ToString() + " " + some1.ToString() + " " + some2.ToString());
                //            if (someBool)
                //            {
                //                tube.AddFill(amount);
                //            }
                //        }
                //    }
                //}
            }
        }

        public virtual void Rotate(float angle = -90)
        {
            _child.localEulerAngles += Vector3.forward * angle;
            ResetGiveDirections();
        }

        public bool isFilled()
        {
            return _isFilled;
        }

        protected abstract Vector2[] GetDefaultGiveDirections();

        protected virtual void ResetGiveDirections()
        {
            Vector2[] giveDirs = GetDefaultGiveDirections();
            if (_giveDirections.Count != giveDirs.Length)
            {
                _giveDirections = new List<Vector2>(giveDirs);
            }
            float rotation = GetRotation();
            for (int i = 0; i < giveDirs.Length; i++)
            {
                Vector2 last = _giveDirections[i];
                _giveDirections[i] = giveDirs[i];
                _giveDirections[i] = Quaternion.Euler(0, 0, rotation) * _giveDirections[i];
                //Debug.Log("Rotate from " + last + " to " + _giveDirections[i]);
            }
        }

        private readonly Vector2[] _nearTubesAdd = new Vector2[] {
            Vector2.left,
            Vector2.right,
            Vector2.up,
            Vector2.down
        };

        
        private List<BaseTubeController> GetNearTubes(bool reset = false)
        {
            //if (this._nearTubes != null && !reset)
            //    return this._nearTubes;
            _nearTubes = new List<BaseTubeController>();
            BaseTubeController some;
            foreach(var v in _nearTubesAdd)
            {
                some = _tubeGameManager.GetTubeAtPosition(position + v);
                if (some != null && some != this)
                {
                    _nearTubes.Add(some);
                    //Debug.Log("Add to near tubes " + some.name + " to " + name + " at pos " + (position + v));
                }
            }
            return _nearTubes;
        }

        public List<Vector2> GetGiveDirections()
        {
            return _giveDirections;
        }

        public float GetRotation()
        {
            return _child.localEulerAngles.z;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Rotate();
        }

        private static bool IsVectorsInverse(Vector2 a, Vector2 b)
        {
            //if (a.x != 0 && b.x != 0 && IsSameFloats(a.x * -1f, b.x))
            //    return true;
            //if (a.y != 0 && b.y != 0 && IsSameFloats(a.y * -1f, b.y))
            //    return true;
            //return false;
            return Vector2.Angle(a, b) > 170f;
        }

        private static bool IsSameVectorDirection(Vector2 a, Vector2 b)
        {
            return Vector2.Angle(a, b) < 1;
        }

        private static bool IsSameFloats(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.1f;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}
