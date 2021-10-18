using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace View_Component
{
    public class Stats : MonoBehaviour
    {
        public int this[StatTypes type]
        {
            get => _data[(int)type];
            set => SetValue(type, value);
        }

        [SerializeField]
        private int[] _data = new int[(int)StatTypes.Count];

        private static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

        public void SetValue(StatTypes type, int value)
        {
            var oldValue = _data[(int)type];

            if (value == oldValue) return;

            _data[(int)type] = value;
            this.PostNotification(DidChangeNotification(type), oldValue);
        }

        public static string DidChangeNotification(StatTypes type)
        {
            if (!_didChangeNotifications.ContainsKey(type))
                _didChangeNotifications.Add(type, $"Stats.{type}DidChange");
            return _didChangeNotifications[type];
        }
    }
}