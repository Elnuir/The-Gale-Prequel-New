using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

namespace CorgiWorks.Scripts
{
    public class InputManagerX : InputManager
    {
        public Dictionary<string, KeyCode> KeyButtonMap;
        private const string prefsPrefix = "key-";

        public MMInput.IMButton HolyWaterButton;
        public MMInput.IMButton DownButton;

        protected override void Start()
        {
            base.Start();

            KeyButtonMap = new Dictionary<string, KeyCode>()
            {
                {JumpButton.ButtonID, KeyCode.Space},
                {DashButton.ButtonID, KeyCode.LeftShift},
                {RunButton.ButtonID, KeyCode.E},
                {ThrowButton.ButtonID, KeyCode.Alpha1},
                {HolyWaterButton.ButtonID, KeyCode.Alpha2},
                {DownButton.ButtonID, KeyCode.S}
            };

            Load();
        }

        protected override void InitializeButtons()
        {
            base.InitializeButtons();
            ButtonList.Add(HolyWaterButton =
                new MMInput.IMButton(PlayerID, "HolyWater", HolyWaterButtonDown, HolyWaterButtonPressed,
                    HolyWaterButtonUp));
            ButtonList.Add(DownButton =
                new MMInput.IMButton(PlayerID, "DownButton", DownButtonDown, DownButtonPressed,
                    DownButtonUp));
        }

        private void DownButtonUp()
        {
            DownButton.State.ChangeState(MMInput.ButtonStates.ButtonUp);
        }

        private void DownButtonPressed()
        {
            DownButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed);
        }

        private void DownButtonDown()
        {
            DownButton.State.ChangeState(MMInput.ButtonStates.ButtonDown);
        }

        public void Save()
        {
            foreach (var pair in KeyButtonMap)
                PlayerPrefs.SetInt(prefsPrefix + pair.Key, (int)pair.Value);
        }

        private void Load()
        {
            foreach (var key in KeyButtonMap.Keys.ToArray())
            {
                string k = prefsPrefix + key;
                if (PlayerPrefs.HasKey(k))
                    KeyButtonMap[key] = (KeyCode)PlayerPrefs.GetInt(k);
            }
        }

        protected override void GetInputButtons()
        {
            foreach (MMInput.IMButton button in ButtonList)
            {
                if (KeyButtonMap.Keys.Contains(button.ButtonID))
                {
                    var keyCode = KeyButtonMap[button.ButtonID];

                    if (Input.GetKey(keyCode))
                        button.TriggerButtonPressed();

                    if (Input.GetKeyDown(keyCode))
                        button.TriggerButtonDown();

                    if (Input.GetKeyUp(keyCode))
                        button.TriggerButtonUp();

                    continue;
                }

                if (Input.GetButton(button.ButtonID))
                    button.TriggerButtonPressed();

                if (Input.GetButtonDown(button.ButtonID))
                    button.TriggerButtonDown();

                if (Input.GetButtonUp(button.ButtonID))
                    button.TriggerButtonUp();
            }
        }

        public virtual void HolyWaterButtonDown()
        {
            HolyWaterButton.State.ChangeState(MMInput.ButtonStates.ButtonDown);
        }

        public virtual void HolyWaterButtonPressed()
        {
            HolyWaterButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed);
        }

        public virtual void HolyWaterButtonUp()
        {
            HolyWaterButton.State.ChangeState(MMInput.ButtonStates.ButtonUp);
        }
    }
}