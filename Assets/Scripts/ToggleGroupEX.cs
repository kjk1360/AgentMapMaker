﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Toggle Group EX")]
    [DisallowMultipleComponent]
    public class ToggleGroupEX : UIBehaviour
    {
        [SerializeField] private bool m_AllowSwitchOff = false;
        public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }

        private List<ToggleEX> m_Toggles = new List<ToggleEX>();
        public List<ToggleEX> togglesByIndex = new List<ToggleEX>();
        protected ToggleGroupEX()
        { }

        protected override void OnEnable()
        {
            for(int i = 0;i < togglesByIndex.Count; i++)
            {
                togglesByIndex[i].SetToggleKey(i);
            }
        }

        private void ValidateToggleIsInGroup(ToggleEX toggle)
        {
            if (toggle == null || !m_Toggles.Contains(toggle))
                throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
        }

        public void NotifyToggleOn(ToggleEX toggle)
        {
            ValidateToggleIsInGroup(toggle);

            // disable all toggles in the group
            for (int i = 0; i < m_Toggles.Count; i++)
            {
                if (m_Toggles[i] == toggle)
                    continue;

                m_Toggles[i].isOn = false;
            }
        }

        public void IndexToggleOn(int index)
        {
            ToggleEX target = togglesByIndex[index];
            target.isOn = true;
            // disable all toggles in the group
            for (int i = 0; i < togglesByIndex.Count; i++)
            {
                if (togglesByIndex[i] == target)
                    continue;

                togglesByIndex[i].isOn = false;
            }
        }

        public List<ToggleEX> GetAllToggles()
        {
            return m_Toggles;
        }

        public ToggleEX GetToggleByIndex(int index)
        {
            return m_Toggles[index];
        }

        public void UnregisterToggle(ToggleEX toggle)
        {
            if (m_Toggles.Contains(toggle))
                m_Toggles.Remove(toggle);
        }

        public void RegisterToggle(ToggleEX toggle)
        {
            if (!m_Toggles.Contains(toggle))
                m_Toggles.Add(toggle);
        }

        public bool AnyTogglesOn()
        {
            return m_Toggles.Find(x => x.isOn) != null;
        }

        public IEnumerable<ToggleEX> ActiveToggles()
        {
            return m_Toggles.Where(x => x.isOn);
        }

        public void SetAllTogglesOff()
        {
            bool oldAllowSwitchOff = m_AllowSwitchOff;
            m_AllowSwitchOff = true;

            for (int i = 0; i < m_Toggles.Count; i++)
                m_Toggles[i].isOn = false;

            m_AllowSwitchOff = oldAllowSwitchOff;
        }
        public void SetAllIndexOff()
        {
            bool oldAllowSwitchOff = m_AllowSwitchOff;
            m_AllowSwitchOff = true;

            for (int i = 0; i < togglesByIndex.Count; i++)
                togglesByIndex[i].isOn = false;

            m_AllowSwitchOff = oldAllowSwitchOff;
        }
    }
}
