using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace CSi.InputV2
{
    public class InputV2 : MonoBehaviour
    {
        public static Gamepad gamepad;
        public static Keyboard keyboard;
        public static Mouse mouse;

        public static float globalHorizontal;
        public static float globalVertical;

        public static Vector2 dpadInput;

        void Awake()
        {
            
        }

        void Update()
        {
            gamepad = Gamepad.current;
            if(gamepad == null) {return;}
            else {}
            keyboard = Keyboard.current;
            if(keyboard == null) {return;}
            else {}
            mouse = Mouse.current;
            if(mouse == null) {return;}
            else {}

            globalHorizontal = Mathf.Clamp(GetJoystickVectorRaw(gamepad.leftStick).x + (GetAxis(keyboard.dKey) - GetAxis(keyboard.aKey)), -1, 1);
            globalVertical = Mathf.Clamp(GetJoystickVectorRaw(gamepad.leftStick).y + (GetAxis(keyboard.wKey) - GetAxis(keyboard.sKey)), -1, 1);

            dpadInput = gamepad.dpad.ReadValue();
        }

        public static Vector2 GetJoystickVector(StickControl control) //1 to 1 read of stick input
        {
            return control.ReadValue();
        }

        public static Vector2 GetJoystickVectorRaw(StickControl control) //clamped to whole numbers
        {
            Vector2 vec2 = control.ReadValue();
            switch (vec2.x)
            {
                case > 0: {vec2.x = MathF.Ceiling(vec2.x); break;}
                case < 0: {vec2.x = MathF.Floor(vec2.x); break;}
                default: return vec2;
            }
            switch (vec2.y)
            {
                case > 0: {vec2.y = MathF.Ceiling(vec2.y); break;}
                case < 0: {vec2.y = MathF.Floor(vec2.y); break;}
                default: return vec2;
            }
            return vec2;
        }

        public static float GetAxis(AxisControl control)
        {
            return control.ReadValue();
        }

        public static ButtonControl GetKeyControl(KeyCode keyCode, InputFormat inputFormat)
        {
            switch (inputFormat)
            {
                case InputFormat.Keyboard:
                {
                    switch (keyCode)
                    {
                        case KeyCode.Backspace: {return keyboard.backspaceKey;}
                        case KeyCode.Tab: {return keyboard.tabKey;}
                        case KeyCode.Return: {return keyboard.enterKey;}
                        case KeyCode.Escape: {return keyboard.escapeKey;}
                        case KeyCode.Space: {return keyboard.spaceKey;}
                        case KeyCode.Quote: {return keyboard.quoteKey;}
                        case KeyCode.Comma: {return keyboard.commaKey;}
                        case KeyCode.Minus: {return keyboard.minusKey;}
                        case KeyCode.Period: {return keyboard.periodKey;}
                        case KeyCode.Slash: {return keyboard.slashKey;}
                        #region numbers 0 to 9
                            case KeyCode.Alpha0: {return keyboard.digit0Key;}
                            case KeyCode.Alpha1: {return keyboard.digit1Key;}
                            case KeyCode.Alpha2: {return keyboard.digit2Key;}
                            case KeyCode.Alpha3: {return keyboard.digit3Key;}
                            case KeyCode.Alpha4: {return keyboard.digit4Key;}
                            case KeyCode.Alpha5: {return keyboard.digit5Key;}
                            case KeyCode.Alpha6: {return keyboard.digit6Key;}
                            case KeyCode.Alpha7: {return keyboard.digit7Key;}
                            case KeyCode.Alpha8: {return keyboard.digit8Key;}
                            case KeyCode.Alpha9: {return keyboard.digit9Key;}
                            case KeyCode.Keypad0: {return keyboard.digit0Key;}
                            case KeyCode.Keypad1: {return keyboard.digit1Key;}
                            case KeyCode.Keypad2: {return keyboard.digit2Key;}
                            case KeyCode.Keypad3: {return keyboard.digit3Key;}
                            case KeyCode.Keypad4: {return keyboard.digit4Key;}
                            case KeyCode.Keypad5: {return keyboard.digit5Key;}
                            case KeyCode.Keypad6: {return keyboard.digit6Key;}
                            case KeyCode.Keypad7: {return keyboard.digit7Key;}
                            case KeyCode.Keypad8: {return keyboard.digit8Key;}
                            case KeyCode.Keypad9: {return keyboard.digit9Key;}
                        #endregion
                        case KeyCode.Semicolon: {return keyboard.semicolonKey;}
                        case KeyCode.Equals: {return keyboard.equalsKey;}
                        case KeyCode.LeftBracket: {return keyboard.leftBracketKey;}
                        case KeyCode.Backslash: {return keyboard.backslashKey;}
                        case KeyCode.RightBracket: {return keyboard.rightBracketKey;}
                        case KeyCode.BackQuote: {return keyboard.backquoteKey;}
                        #region letters A to Z
                            case KeyCode.A: {return keyboard.aKey;}
                            case KeyCode.B: {return keyboard.bKey;}
                            case KeyCode.C: {return keyboard.cKey;}
                            case KeyCode.D: {return keyboard.dKey;}
                            case KeyCode.E: {return keyboard.eKey;}
                            case KeyCode.F: {return keyboard.fKey;}
                            case KeyCode.G: {return keyboard.gKey;}
                            case KeyCode.H: {return keyboard.hKey;}
                            case KeyCode.I: {return keyboard.iKey;}
                            case KeyCode.J: {return keyboard.jKey;}
                            case KeyCode.K: {return keyboard.kKey;}
                            case KeyCode.L: {return keyboard.lKey;}
                            case KeyCode.M: {return keyboard.mKey;}
                            case KeyCode.N: {return keyboard.nKey;}
                            case KeyCode.O: {return keyboard.oKey;}
                            case KeyCode.P: {return keyboard.pKey;}
                            case KeyCode.Q: {return keyboard.qKey;}
                            case KeyCode.R: {return keyboard.rKey;}
                            case KeyCode.S: {return keyboard.sKey;}
                            case KeyCode.T: {return keyboard.tKey;}
                            case KeyCode.U: {return keyboard.uKey;}
                            case KeyCode.V: {return keyboard.vKey;}
                            case KeyCode.W: {return keyboard.wKey;}
                            case KeyCode.X: {return keyboard.xKey;}
                            case KeyCode.Y: {return keyboard.yKey;}
                            case KeyCode.Z: {return keyboard.zKey;}
                        #endregion
                        case KeyCode.Delete: {return keyboard.deleteKey;}
                        case KeyCode.UpArrow: {return keyboard.upArrowKey;}
                        case KeyCode.DownArrow: {return keyboard.downArrowKey;}
                        case KeyCode.RightArrow: {return keyboard.rightArrowKey;}
                        case KeyCode.LeftArrow: {return keyboard.leftArrowKey;}
                        case KeyCode.Insert: {return keyboard.insertKey;}
                        case KeyCode.Home: {return keyboard.homeKey;}
                        case KeyCode.End: {return keyboard.endKey;}
                        case KeyCode.PageUp: {return keyboard.pageUpKey;}
                        case KeyCode.PageDown: {return keyboard.pageDownKey;}
                        #region function keys f1 to f12
                            case KeyCode.F1: {return keyboard.f1Key;}
                            case KeyCode.F2: {return keyboard.f2Key;}
                            case KeyCode.F3: {return keyboard.f3Key;}
                            case KeyCode.F4: {return keyboard.f4Key;}
                            case KeyCode.F5: {return keyboard.f5Key;}
                            case KeyCode.F6: {return keyboard.f6Key;}
                            case KeyCode.F7: {return keyboard.f7Key;}
                            case KeyCode.F8: {return keyboard.f8Key;}
                            case KeyCode.F9: {return keyboard.f9Key;}
                            case KeyCode.F10: {return keyboard.f10Key;}
                            case KeyCode.F11: {return keyboard.f11Key;}
                            case KeyCode.F12: {return keyboard.f12Key;}
                        #endregion
                        case KeyCode.Numlock: {return keyboard.numLockKey;}
                        case KeyCode.CapsLock: {return keyboard.capsLockKey;}
                        case KeyCode.ScrollLock: {return keyboard.scrollLockKey;}
                        case KeyCode.RightShift: {return keyboard.rightShiftKey;}
                        case KeyCode.LeftShift: {return keyboard.leftShiftKey;}
                        case KeyCode.RightControl: {return keyboard.rightCtrlKey;}
                        case KeyCode.LeftControl: {return keyboard.leftCtrlKey;}
                        case KeyCode.RightAlt: {return keyboard.rightAltKey;}
                        case KeyCode.LeftAlt: {return keyboard.leftAltKey;}
                        case KeyCode.RightCommand: {return keyboard.rightCommandKey;}
                        case KeyCode.LeftCommand: {return keyboard.leftCommandKey;}
                        default: {return null;}
                    }
                }
                case InputFormat.Mouse:
                {
                    switch (keyCode)
                    {
                        case KeyCode.Mouse0: {return mouse.leftButton;}
                        case KeyCode.Mouse1: {return mouse.rightButton;}
                        case KeyCode.Mouse2: {return mouse.middleButton;}
                        case KeyCode.Mouse4: {return mouse.backButton;}
                        case KeyCode.Mouse5: {return mouse.forwardButton;}
                        default: {return null;}
                    }
                }
                case InputFormat.Gamepad:
                {
                    switch (keyCode)
                    {
                        // ABXY
                        case KeyCode.JoystickButton0: {return gamepad.buttonSouth;}
                        case KeyCode.JoystickButton1: {return gamepad.buttonEast;}
                        case KeyCode.JoystickButton2: {return gamepad.buttonWest;}
                        case KeyCode.JoystickButton3: {return gamepad.buttonNorth;}
                        // bumpers / triggers
                        case KeyCode.JoystickButton4: {return gamepad.leftShoulder;}
                        case KeyCode.JoystickButton5: {return gamepad.rightShoulder;}
                        // view / menu (aka select / start)
                        case KeyCode.JoystickButton6: {return gamepad.selectButton;}
                        case KeyCode.JoystickButton7: {return gamepad.startButton;}
                        // stick buttons
                        case KeyCode.JoystickButton8: {return gamepad.leftStickButton;}
                        case KeyCode.JoystickButton9: {return gamepad.rightStickButton;}
                        default: {return null;}
                    }
                }
                default: return null;
            }
        }

        public static bool OnKeyDown(KeyCode keyCode)
        {
            if(Input.GetKeyDown(keyCode))
            {
                return true;
            }
            else return false;
        }

        public static bool OnKeyUp(KeyCode keyCode)
        {
            if(Input.GetKeyUp(keyCode))
            {
                return true;
            }
            else return false;
        }

        public static bool OnKeyHeld(KeyCode keyCode)
        {
            if(Input.GetKey(keyCode))
            {
                return true;
            }
            else return false;
        }
    }

    public enum InputFormat
    {
        Keyboard = 0,
        Mouse = 1,
        Gamepad = 2
    }
}
