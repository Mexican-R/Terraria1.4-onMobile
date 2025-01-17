using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameInput
{
	public class PlayerInput
	{
		public class MiscSettingsTEMP
		{
			public static bool HotbarRadialShouldBeUsed = true;
		}

		public static Vector2 RawMouseScale = Vector2.One;

		public static TriggersPack Triggers = new TriggersPack();

		public static List<string> KnownTriggers = new List<string>
		{
			"MouseLeft",
			"MouseRight",
			"Up",
			"Down",
			"Left",
			"Right",
			"Jump",
			"Throw",
			"Inventory",
			"Grapple",
			"SmartSelect",
			"SmartCursor",
			"QuickMount",
			"QuickHeal",
			"QuickMana",
			"QuickBuff",
			"MapZoomIn",
			"MapZoomOut",
			"MapAlphaUp",
			"MapAlphaDown",
			"MapFull",
			"MapStyle",
			"Hotbar1",
			"Hotbar2",
			"Hotbar3",
			"Hotbar4",
			"Hotbar5",
			"Hotbar6",
			"Hotbar7",
			"Hotbar8",
			"Hotbar9",
			"Hotbar10",
			"HotbarMinus",
			"HotbarPlus",
			"DpadRadial1",
			"DpadRadial2",
			"DpadRadial3",
			"DpadRadial4",
			"RadialHotbar",
			"RadialQuickbar",
			"DpadSnap1",
			"DpadSnap2",
			"DpadSnap3",
			"DpadSnap4",
			"MenuUp",
			"MenuDown",
			"MenuLeft",
			"MenuRight",
			"LockOn",
			"ViewZoomIn",
			"ViewZoomOut",
			"ToggleCreativeMenu"
		};

		private static bool _canReleaseRebindingLock = true;

		private static int _memoOfLastPoint = -1;

		public static int NavigatorRebindingLock;

		public static string BlockedKey = "";

		private static string _listeningTrigger;

		private static InputMode _listeningInputMode;

		public static Dictionary<string, PlayerInputProfile> Profiles = new Dictionary<string, PlayerInputProfile>();

		public static Dictionary<string, PlayerInputProfile> OriginalProfiles = new Dictionary<string, PlayerInputProfile>();

		private static string _selectedProfile;

		private static PlayerInputProfile _currentProfile;

		public static InputMode CurrentInputMode = InputMode.Keyboard;

		private static Buttons[] ButtonsGamepad = (Buttons[])Enum.GetValues(typeof(Buttons));

		public static bool GrappleAndInteractAreShared;

		public static SmartSelectGamepadPointer smartSelectPointer = new SmartSelectGamepadPointer();

		private static string _invalidatorCheck = "";

		private static bool _lastActivityState;

		public static MouseState MouseInfo;

		public static MouseState MouseInfoOld;

		public static int MouseX;

		public static int MouseY;

		public static bool LockGamepadTileUseButton = false;

		public static List<string> MouseKeys = new List<string>();

		public static int PreUIX;

		public static int PreUIY;

		public static int PreLockOnX;

		public static int PreLockOnY;

		public static int ScrollWheelValue;

		public static int ScrollWheelValueOld;

		public static int ScrollWheelDelta;

		public static int ScrollWheelDeltaForUI;

		public static bool GamepadAllowScrolling;

		public static int GamepadScrollValue;

		public static Vector2 GamepadThumbstickLeft = Vector2.Zero;

		public static Vector2 GamepadThumbstickRight = Vector2.Zero;

		private const int _fastUseMouseItemSlotType = -2;

		private const int _fastUseEmpty = -1;

		private static int _fastUseItemInventorySlot = -1;

		private static bool _InBuildingMode;

		private static int _UIPointForBuildingMode = -1;

		public static bool WritingText;

		private static int _originalMouseX;

		private static int _originalMouseY;

		private static int _originalLastMouseX;

		private static int _originalLastMouseY;

		private static int _originalScreenWidth;

		private static int _originalScreenHeight;

		private static ZoomContext _currentWantedZoom;

		private static int[] DpadSnapCooldown = new int[4];

		public static string ListeningTrigger => _listeningTrigger;

		public static bool CurrentlyRebinding => _listeningTrigger != null;

		public static bool InvisibleGamepadInMenus
		{
			get
			{
				if ((!Main.gameMenu && !Main.ingameOptionsWindow && !Main.playerInventory && Main.player[Main.myPlayer].talkNPC == -1 && Main.player[Main.myPlayer].sign == -1 && Main.InGameUI.CurrentState == null) || _InBuildingMode || !Main.InvisibleCursorForGamepad)
				{
					if (CursorIsBusy)
					{
						return !_InBuildingMode;
					}
					return false;
				}
				return true;
			}
		}

		public static PlayerInputProfile CurrentProfile => _currentProfile;

		public static KeyConfiguration ProfileGamepadUI => CurrentProfile.InputModes[InputMode.XBoxGamepadUI];

		public static bool UsingGamepad
		{
			get
			{
				if (CurrentInputMode != InputMode.XBoxGamepad)
				{
					return CurrentInputMode == InputMode.XBoxGamepadUI;
				}
				return true;
			}
		}

		public static bool UsingGamepadUI => CurrentInputMode == InputMode.XBoxGamepadUI;

		public static bool IgnoreMouseInterface
		{
			get
			{
				if (!UsingGamepad || UILinkPointNavigator.Available)
				{
					if (Main.LocalPlayer.itemAnimation > 0)
					{
						return !UsingGamepad;
					}
					return false;
				}
				return true;
			}
		}

		public static bool ShouldFastUseItem => _fastUseItemInventorySlot != -1;

		public static bool InBuildingMode => _InBuildingMode;

		public static int RealScreenWidth => _originalScreenWidth;

		public static int RealScreenHeight => _originalScreenHeight;

		public static bool CursorIsBusy
		{
			get
			{
				if (!(ItemSlot.CircularRadialOpacity > 0f))
				{
					return ItemSlot.QuicksRadialOpacity > 0f;
				}
				return true;
			}
		}

		public static Vector2 OriginalScreenSize => new Vector2(_originalScreenWidth, _originalScreenHeight);

		public static event Action OnBindingChange;

		public static event Action OnActionableInput;

		public static void ListenFor(string triggerName, InputMode inputmode)
		{
			_listeningTrigger = triggerName;
			_listeningInputMode = inputmode;
		}

		private static bool InvalidateKeyboardSwap()
		{
			if (_invalidatorCheck.Length == 0)
			{
				return false;
			}
			string text = "";
			List<Keys> pressedKeys = GetPressedKeys();
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				text = string.Concat(text, pressedKeys[i], ", ");
			}
			if (text == _invalidatorCheck)
			{
				return true;
			}
			_invalidatorCheck = "";
			return false;
		}

		public static void ResetInputsOnActiveStateChange()
		{
			bool isActive = Main.instance.IsActive;
			if (_lastActivityState != isActive)
			{
				MouseInfo = default(MouseState);
				MouseInfoOld = default(MouseState);
				Main.keyState = Keyboard.GetState();
				Main.inputText = Keyboard.GetState();
				Main.oldInputText = Keyboard.GetState();
				Main.keyCount = 0;
				Triggers.Reset();
				Triggers.Reset();
				string text = "";
				List<Keys> pressedKeys = GetPressedKeys();
				for (int i = 0; i < pressedKeys.Count; i++)
				{
					text = string.Concat(text, pressedKeys[i], ", ");
				}
				_invalidatorCheck = text;
			}
			_lastActivityState = isActive;
		}

		public static List<Keys> GetPressedKeys()
		{
			List<Keys> list = Main.keyState.GetPressedKeys().ToList();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				if (list[num] == Keys.None)
				{
					list.RemoveAt(num);
				}
			}
			return list;
		}

		public static void TryEnteringFastUseModeForInventorySlot(int inventorySlot)
		{
			_fastUseItemInventorySlot = inventorySlot;
			if (inventorySlot < 50 && inventorySlot >= 0)
			{
				Player localPlayer = Main.LocalPlayer;
				ItemSlot.PickupItemIntoMouse(localPlayer.inventory, 0, inventorySlot, localPlayer);
			}
		}

		public static void TryEnteringFastUseModeForMouseItem()
		{
			_fastUseItemInventorySlot = -2;
		}

		public static void TryEndingFastUse()
		{
			if (_fastUseItemInventorySlot >= 0 && _fastUseItemInventorySlot != -2)
			{
				Player localPlayer = Main.LocalPlayer;
				if (localPlayer.inventory[_fastUseItemInventorySlot].IsAir)
				{
					Utils.Swap(Main.mouseItem, localPlayer.inventory[_fastUseItemInventorySlot]);
				}
			}
			_fastUseItemInventorySlot = -1;
		}

		public static void EnterBuildingMode()
		{
			SoundEngine.PlaySound(10);
			_InBuildingMode = true;
			_UIPointForBuildingMode = UILinkPointNavigator.CurrentPoint;
			if (Main.mouseItem.stack <= 0)
			{
				int uIPointForBuildingMode = _UIPointForBuildingMode;
				if (uIPointForBuildingMode < 50 && uIPointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uIPointForBuildingMode].stack > 0)
				{
					Utils.Swap(Main.mouseItem, Main.player[Main.myPlayer].inventory[uIPointForBuildingMode]);
				}
			}
		}

		public static void ExitBuildingMode()
		{
			SoundEngine.PlaySound(11);
			_InBuildingMode = false;
			UILinkPointNavigator.ChangePoint(_UIPointForBuildingMode);
			if (Main.mouseItem.stack > 0 && Main.player[Main.myPlayer].itemAnimation == 0)
			{
				int uIPointForBuildingMode = _UIPointForBuildingMode;
				if (uIPointForBuildingMode < 50 && uIPointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uIPointForBuildingMode].stack <= 0)
				{
					Utils.Swap(Main.mouseItem, Main.player[Main.myPlayer].inventory[uIPointForBuildingMode]);
				}
			}
			_UIPointForBuildingMode = -1;
		}

		public static void VerifyBuildingMode()
		{
			if (_InBuildingMode)
			{
				Player obj = Main.player[Main.myPlayer];
				bool flag = false;
				if (Main.mouseItem.stack <= 0)
				{
					flag = true;
				}
				if (obj.dead)
				{
					flag = true;
				}
				if (flag)
				{
					ExitBuildingMode();
				}
			}
		}

		public static void SetSelectedProfile(string name)
		{
			if (Profiles.ContainsKey(name))
			{
				_selectedProfile = name;
				_currentProfile = Profiles[_selectedProfile];
			}
		}

		public static void Initialize()
		{
			Main.InputProfiles.OnProcessText += PrettyPrintProfiles;
			Player.Hooks.OnEnterWorld += Hook_OnEnterWorld;
			PlayerInputProfile playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Custom");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			SetSelectedProfile("Custom");
			Triggers.Initialize();
		}

		public static void Hook_OnEnterWorld(Player player)
		{
			if (UsingGamepad && player.whoAmI == Main.myPlayer)
			{
				Main.SmartCursorEnabled = true;
			}
		}

		public static bool Save()
		{
			Main.InputProfiles.Clear();
			Main.InputProfiles.Put("Selected Profile", _selectedProfile);
			foreach (KeyValuePair<string, PlayerInputProfile> profile in Profiles)
			{
				Main.InputProfiles.Put(profile.Value.Name, profile.Value.Save());
			}
			return Main.InputProfiles.Save();
		}

		public static void Load()
		{
			Main.InputProfiles.Load();
			Dictionary<string, PlayerInputProfile> dictionary = new Dictionary<string, PlayerInputProfile>();
			string currentValue = null;
			Main.InputProfiles.Get("Selected Profile", currentValue);
			List<string> allKeys = Main.InputProfiles.GetAllKeys();
			for (int i = 0; i < allKeys.Count; i++)
			{
				string text = allKeys[i];
				if (text == "Selected Profile" || string.IsNullOrEmpty(text))
				{
					continue;
				}
				Dictionary<string, object> currentValue2 = new Dictionary<string, object>();
				Main.InputProfiles.Get(text, currentValue2);
				if (currentValue2.Count > 0)
				{
					PlayerInputProfile playerInputProfile = new PlayerInputProfile(text);
					playerInputProfile.Initialize(PresetProfiles.None);
					if (playerInputProfile.Load(currentValue2))
					{
						dictionary.Add(text, playerInputProfile);
					}
				}
			}
			if (dictionary.Count > 0)
			{
				Profiles = dictionary;
				if (!string.IsNullOrEmpty(currentValue) && Profiles.ContainsKey(currentValue))
				{
					SetSelectedProfile(currentValue);
				}
				else
				{
					SetSelectedProfile(Profiles.Keys.First());
				}
			}
		}

		public static void ManageVersion_1_3()
		{
			PlayerInputProfile playerInputProfile = Profiles["Custom"];
			string[,] array = new string[20, 2]
			{
				{
					"KeyUp",
					"Up"
				},
				{
					"KeyDown",
					"Down"
				},
				{
					"KeyLeft",
					"Left"
				},
				{
					"KeyRight",
					"Right"
				},
				{
					"KeyJump",
					"Jump"
				},
				{
					"KeyThrowItem",
					"Throw"
				},
				{
					"KeyInventory",
					"Inventory"
				},
				{
					"KeyQuickHeal",
					"QuickHeal"
				},
				{
					"KeyQuickMana",
					"QuickMana"
				},
				{
					"KeyQuickBuff",
					"QuickBuff"
				},
				{
					"KeyUseHook",
					"Grapple"
				},
				{
					"KeyAutoSelect",
					"SmartSelect"
				},
				{
					"KeySmartCursor",
					"SmartCursor"
				},
				{
					"KeyMount",
					"QuickMount"
				},
				{
					"KeyMapStyle",
					"MapStyle"
				},
				{
					"KeyFullscreenMap",
					"MapFull"
				},
				{
					"KeyMapZoomIn",
					"MapZoomIn"
				},
				{
					"KeyMapZoomOut",
					"MapZoomOut"
				},
				{
					"KeyMapAlphaUp",
					"MapAlphaUp"
				},
				{
					"KeyMapAlphaDown",
					"MapAlphaDown"
				}
			};
			for (int i = 0; i < array.GetLength(0); i++)
			{
				string currentValue = null;
				Main.Configuration.Get(array[i, 0], currentValue);
				if (currentValue != null)
				{
					playerInputProfile.InputModes[InputMode.Keyboard].KeyStatus[array[i, 1]] = new List<string>
					{
						currentValue
					};
					playerInputProfile.InputModes[InputMode.KeyboardUI].KeyStatus[array[i, 1]] = new List<string>
					{
						currentValue
					};
				}
			}
		}

		public static void UpdateInput()
		{
			Triggers.Reset();
			ScrollWheelValueOld = ScrollWheelValue;
			ScrollWheelValue = 0;
			GamepadThumbstickLeft = Vector2.Zero;
			GamepadThumbstickRight = Vector2.Zero;
			GrappleAndInteractAreShared = UsingGamepad && CurrentProfile.InputModes[InputMode.XBoxGamepad].DoGrappleAndInteractShareTheSameKey;
			if (InBuildingMode && !UsingGamepad)
			{
				ExitBuildingMode();
			}
			if (_canReleaseRebindingLock && NavigatorRebindingLock > 0)
			{
				NavigatorRebindingLock--;
				Triggers.Current.UsedMovementKey = false;
				if (NavigatorRebindingLock == 0 && _memoOfLastPoint != -1)
				{
					UIManageControls.ForceMoveTo = _memoOfLastPoint;
					_memoOfLastPoint = -1;
				}
			}
			_canReleaseRebindingLock = true;
			VerifyBuildingMode();
			MouseInput();
			int num = 0 | (KeyboardInput() ? 1 : 0) | (GamePadInput() ? 1 : 0);
			Triggers.Update();
			PostInput();
			ScrollWheelDelta = ScrollWheelValue - ScrollWheelValueOld;
			ScrollWheelDeltaForUI = ScrollWheelDelta;
			WritingText = false;
			UpdateMainMouse();
			Main.mouseLeft = Triggers.Current.MouseLeft;
			Main.mouseRight = Triggers.Current.MouseRight;
			CacheZoomableValues();
			if (num != 0 && PlayerInput.OnActionableInput != null)
			{
				PlayerInput.OnActionableInput();
			}
		}

		public static void UpdateMainMouse()
		{
			Main.lastMouseX = Main.mouseX;
			Main.lastMouseY = Main.mouseY;
			Main.mouseX = MouseX;
			Main.mouseY = MouseY;
		}

		public static void CacheZoomableValues()
		{
			CacheOriginalInput();
			CacheOriginalScreenDimensions();
		}

		public static void CacheMousePositionForZoom()
		{
			float num = 1f;
			_originalMouseX = (int)((float)Main.mouseX * num);
			_originalMouseY = (int)((float)Main.mouseY * num);
		}

		private static void CacheOriginalInput()
		{
			_originalMouseX = Main.mouseX;
			_originalMouseY = Main.mouseY;
			_originalLastMouseX = Main.lastMouseX;
			_originalLastMouseY = Main.lastMouseY;
		}

		public static void CacheOriginalScreenDimensions()
		{
			_originalScreenWidth = Main.screenWidth;
			_originalScreenHeight = Main.screenHeight;
		}

		private static bool GamePadInput()
		{
			bool flag = false;
			ScrollWheelValue += GamepadScrollValue;
			GamePadState gamePadState = default(GamePadState);
			bool flag2 = false;
			for (int i = 0; i < 4; i++)
			{
				GamePadState state = GamePad.GetState((PlayerIndex)i);
				if (state.IsConnected)
				{
					flag2 = true;
					gamePadState = state;
					break;
				}
			}
			if (Main.SettingBlockGamepadsEntirely)
			{
				return false;
			}
			if (!flag2)
			{
				return false;
			}
			if (!Main.instance.IsActive && !Main.AllowUnfocusedInputOnGamepad)
			{
				return false;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag3 = UILinkPointNavigator.Available && !InBuildingMode;
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || flag3 || player.talkNPC != -1 || player.sign != -1 || IngameFancyUI.CanCover())
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (!Main.gameMenu && InBuildingMode)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			if (CurrentInputMode == InputMode.XBoxGamepad && inputMode == InputMode.XBoxGamepadUI)
			{
				flag = true;
			}
			if (CurrentInputMode == InputMode.XBoxGamepadUI && inputMode == InputMode.XBoxGamepad)
			{
				flag = true;
			}
			if (flag)
			{
				CurrentInputMode = inputMode;
			}
			KeyConfiguration keyConfiguration = CurrentProfile.InputModes[inputMode];
			int num = 2145386496;
			for (int j = 0; j < ButtonsGamepad.Length; j++)
			{
				if ((int)((uint)num & (uint)ButtonsGamepad[j]) <= 0 && gamePadState.IsButtonDown(ButtonsGamepad[j]))
				{
					if (CheckRebindingProcessGamepad(ButtonsGamepad[j].ToString()))
					{
						return false;
					}
					keyConfiguration.Processkey(Triggers.Current, ButtonsGamepad[j].ToString());
					flag = true;
				}
			}
			GamepadThumbstickLeft = gamePadState.ThumbSticks.Left * new Vector2(1f, -1f) * new Vector2(CurrentProfile.LeftThumbstickInvertX.ToDirectionInt() * -1, CurrentProfile.LeftThumbstickInvertY.ToDirectionInt() * -1);
			GamepadThumbstickRight = gamePadState.ThumbSticks.Right * new Vector2(1f, -1f) * new Vector2(CurrentProfile.RightThumbstickInvertX.ToDirectionInt() * -1, CurrentProfile.RightThumbstickInvertY.ToDirectionInt() * -1);
			Vector2 gamepadThumbstickRight = GamepadThumbstickRight;
			Vector2 gamepadThumbstickLeft = GamepadThumbstickLeft;
			Vector2 vector = gamepadThumbstickRight;
			if (vector != Vector2.Zero)
			{
				vector.Normalize();
			}
			Vector2 vector2 = gamepadThumbstickLeft;
			if (vector2 != Vector2.Zero)
			{
				vector2.Normalize();
			}
			float num2 = 0.6f;
			float triggersDeadzone = CurrentProfile.TriggersDeadzone;
			if (inputMode == InputMode.XBoxGamepadUI)
			{
				num2 = 0.4f;
				if (GamepadAllowScrolling)
				{
					GamepadScrollValue -= (int)(gamepadThumbstickRight.Y * 16f);
				}
				GamepadAllowScrolling = false;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X < 0f - CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (CheckRebindingProcessGamepad(Buttons.LeftThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.LeftThumbstickLeft.ToString());
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X > CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (CheckRebindingProcessGamepad(Buttons.LeftThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.LeftThumbstickRight.ToString());
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y < 0f - CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (CheckRebindingProcessGamepad(Buttons.LeftThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.LeftThumbstickUp.ToString());
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y > CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (CheckRebindingProcessGamepad(Buttons.LeftThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.LeftThumbstickDown.ToString());
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X < 0f - CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (CheckRebindingProcessGamepad(Buttons.RightThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.RightThumbstickLeft.ToString());
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X > CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (CheckRebindingProcessGamepad(Buttons.RightThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.RightThumbstickRight.ToString());
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y < 0f - CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (CheckRebindingProcessGamepad(Buttons.RightThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.RightThumbstickUp.ToString());
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y > CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (CheckRebindingProcessGamepad(Buttons.RightThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.RightThumbstickDown.ToString());
				flag = true;
			}
			if (gamePadState.Triggers.Left > triggersDeadzone)
			{
				if (CheckRebindingProcessGamepad(Buttons.LeftTrigger.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.LeftTrigger.ToString());
				flag = true;
			}
			if (gamePadState.Triggers.Right > triggersDeadzone)
			{
				if (CheckRebindingProcessGamepad(Buttons.RightTrigger.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(Triggers.Current, Buttons.RightTrigger.ToString());
				flag = true;
			}
			bool flag4 = ItemID.Sets.GamepadWholeScreenUseRange[player.inventory[player.selectedItem].type] || player.scope;
			int num3 = player.inventory[player.selectedItem].tileBoost + ItemID.Sets.GamepadExtraRange[player.inventory[player.selectedItem].type];
			if (player.yoyoString && ItemID.Sets.Yoyo[player.inventory[player.selectedItem].type])
			{
				num3 += 5;
			}
			else if (player.inventory[player.selectedItem].createTile < 0 && player.inventory[player.selectedItem].createWall <= 0 && player.inventory[player.selectedItem].shoot > 0)
			{
				num3 += 10;
			}
			else if (player.controlTorch)
			{
				num3++;
			}
			if (flag4)
			{
				num3 += 30;
			}
			if (player.mount.Active && player.mount.Type == 8)
			{
				num3 = 10;
			}
			bool flag5 = false;
			bool flag6 = !Main.gameMenu && !flag3 && Main.SmartCursorEnabled;
			if (!CursorIsBusy)
			{
				bool flag7 = Main.mapFullscreen || (!Main.gameMenu && !flag3);
				int num4 = Main.screenWidth / 2;
				int num5 = Main.screenHeight / 2;
				if (!Main.mapFullscreen && flag7 && !flag4)
				{
					Point point = Main.ReverseGravitySupport(player.Center - Main.screenPosition).ToPoint();
					num4 = point.X;
					num5 = point.Y;
				}
				if (player.velocity == Vector2.Zero && gamepadThumbstickLeft == Vector2.Zero && gamepadThumbstickRight == Vector2.Zero && flag6)
				{
					num4 += player.direction * 10;
				}
				float m = Main.GameViewMatrix.ZoomMatrix.M11;
				smartSelectPointer.UpdateSize(new Vector2(Player.tileRangeX * 16 + num3 * 16, Player.tileRangeY * 16 + num3 * 16) * m);
				if (flag4)
				{
					smartSelectPointer.UpdateSize(new Vector2(Math.Max(Main.screenWidth, Main.screenHeight) / 2));
				}
				smartSelectPointer.UpdateCenter(new Vector2(num4, num5));
				if (gamepadThumbstickRight != Vector2.Zero && flag7)
				{
					Vector2 value = new Vector2(8f);
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						value = new Vector2(16f);
					}
					if (flag6)
					{
						value = new Vector2(Player.tileRangeX * 16, Player.tileRangeY * 16);
						if (num3 != 0)
						{
							value += new Vector2(num3 * 16, num3 * 16);
						}
						if (flag4)
						{
							value = new Vector2(Math.Max(Main.screenWidth, Main.screenHeight) / 2);
						}
					}
					else if (!Main.mapFullscreen)
					{
						if (player.inventory[player.selectedItem].mech)
						{
							value += Vector2.Zero;
						}
						else
						{
							value += new Vector2(num3) / 4f;
						}
					}
					float m2 = Main.GameViewMatrix.ZoomMatrix.M11;
					Vector2 vector3 = gamepadThumbstickRight * value * m2;
					int num6 = MouseX - num4;
					int num7 = MouseY - num5;
					if (flag6)
					{
						num6 = 0;
						num7 = 0;
					}
					num6 += (int)vector3.X;
					num7 += (int)vector3.Y;
					MouseX = num6 + num4;
					MouseY = num7 + num5;
					flag = true;
					flag5 = true;
				}
				if (gamepadThumbstickLeft != Vector2.Zero && flag7)
				{
					float scaleFactor = 8f;
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						scaleFactor = 3f;
					}
					if (Main.mapFullscreen)
					{
						Vector2 value2 = gamepadThumbstickLeft * scaleFactor;
						Main.mapFullscreenPos += value2 * scaleFactor * (1f / Main.mapFullscreenScale);
						flag = true;
					}
					else if (!flag5 && Main.SmartCursorEnabled)
					{
						float m3 = Main.GameViewMatrix.ZoomMatrix.M11;
						Vector2 vector4 = gamepadThumbstickLeft * new Vector2(Player.tileRangeX * 16, Player.tileRangeY * 16) * m3;
						if (num3 != 0)
						{
							vector4 = gamepadThumbstickLeft * new Vector2((Player.tileRangeX + num3) * 16, (Player.tileRangeY + num3) * 16) * m3;
						}
						if (flag4)
						{
							vector4 = new Vector2(Math.Max(Main.screenWidth, Main.screenHeight) / 2) * gamepadThumbstickLeft;
						}
						int num8 = (int)vector4.X;
						int num9 = (int)vector4.Y;
						MouseX = num8 + num4;
						MouseY = num9 + num5;
						flag5 = true;
					}
					flag = true;
				}
				if (CurrentInputMode == InputMode.XBoxGamepad)
				{
					HandleDpadSnap();
					int num10 = MouseX - num4;
					int num11 = MouseY - num5;
					if (!Main.gameMenu && !flag3)
					{
						if (flag4 && !Main.mapFullscreen)
						{
							float num12 = 1f;
							int num13 = Main.screenWidth / 2;
							int num14 = Main.screenHeight / 2;
							num10 = (int)Utils.Clamp(num10, (float)(-num13) * num12, (float)num13 * num12);
							num11 = (int)Utils.Clamp(num11, (float)(-num14) * num12, (float)num14 * num12);
						}
						else
						{
							float num15 = 0f;
							if (player.HeldItem.createTile >= 0 || player.HeldItem.createWall > 0 || player.HeldItem.tileWand >= 0)
							{
								num15 = 0.5f;
							}
							float m4 = Main.GameViewMatrix.ZoomMatrix.M11;
							float num16 = (0f - ((float)(Player.tileRangeY + num3) - num15)) * 16f * m4;
							float max = ((float)(Player.tileRangeY + num3) - num15) * 16f * m4;
							num16 -= (float)(player.height / 16 / 2 * 16);
							num10 = (int)Utils.Clamp(num10, (0f - ((float)(Player.tileRangeX + num3) - num15)) * 16f * m4, ((float)(Player.tileRangeX + num3) - num15) * 16f * m4);
							num11 = (int)Utils.Clamp(num11, num16, max);
						}
						if (flag6 && (!flag || flag4))
						{
							float num17 = 0.81f;
							if (flag4)
							{
								num17 = 0.95f;
							}
							num10 = (int)((float)num10 * num17);
							num11 = (int)((float)num11 * num17);
						}
					}
					else
					{
						num10 = Utils.Clamp(num10, -num4 + 10, num4 - 10);
						num11 = Utils.Clamp(num11, -num5 + 10, num5 - 10);
					}
					MouseX = num10 + num4;
					MouseY = num11 + num5;
				}
			}
			if (flag)
			{
				CurrentInputMode = inputMode;
			}
			if (CurrentInputMode == InputMode.XBoxGamepad)
			{
				Main.SetCameraGamepadLerp(0.1f);
			}
			return flag;
		}

		private static void MouseInput()
		{
			bool flag = false;
			MouseInfoOld = MouseInfo;
			MouseInfo = Mouse.GetState();
			ScrollWheelValue += MouseInfo.ScrollWheelValue;
			if (MouseInfo.X != MouseInfoOld.X || MouseInfo.Y != MouseInfoOld.Y || MouseInfo.ScrollWheelValue != MouseInfoOld.ScrollWheelValue)
			{
				MouseX = (int)((float)MouseInfo.X * RawMouseScale.X);
				MouseY = (int)((float)MouseInfo.Y * RawMouseScale.Y);
				flag = true;
			}
			MouseKeys.Clear();
			if (Main.instance.IsActive)
			{
				if (MouseInfo.LeftButton == ButtonState.Pressed)
				{
					MouseKeys.Add("Mouse1");
					flag = true;
				}
				if (MouseInfo.RightButton == ButtonState.Pressed)
				{
					MouseKeys.Add("Mouse2");
					flag = true;
				}
				if (MouseInfo.MiddleButton == ButtonState.Pressed)
				{
					MouseKeys.Add("Mouse3");
					flag = true;
				}
				if (MouseInfo.XButton1 == ButtonState.Pressed)
				{
					MouseKeys.Add("Mouse4");
					flag = true;
				}
				if (MouseInfo.XButton2 == ButtonState.Pressed)
				{
					MouseKeys.Add("Mouse5");
					flag = true;
				}
			}
			if (flag)
			{
				CurrentInputMode = InputMode.Mouse;
				Triggers.Current.UsedMovementKey = false;
			}
		}

		private static bool KeyboardInput()
		{
			bool flag = false;
			bool flag2 = false;
			List<Keys> pressedKeys = GetPressedKeys();
			DebugKeys(pressedKeys);
			if (pressedKeys.Count == 0 && MouseKeys.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				if (pressedKeys[i] == Keys.LeftShift || pressedKeys[i] == Keys.RightShift)
				{
					flag = true;
				}
				else if (pressedKeys[i] == Keys.LeftAlt || pressedKeys[i] == Keys.RightAlt)
				{
					flag2 = true;
				}
				Main.ChromaPainter.PressKey(pressedKeys[i]);
			}
			if (Main.blockKey != Keys.None.ToString())
			{
				bool flag3 = false;
				for (int j = 0; j < pressedKeys.Count; j++)
				{
					if (pressedKeys[j].ToString() == Main.blockKey)
					{
						pressedKeys[j] = Keys.None;
						flag3 = true;
					}
				}
				if (!flag3)
				{
					Main.blockKey = Keys.None.ToString();
				}
			}
			KeyConfiguration keyConfiguration = CurrentProfile.InputModes[InputMode.Keyboard];
			if (Main.gameMenu && !WritingText)
			{
				keyConfiguration = CurrentProfile.InputModes[InputMode.KeyboardUI];
			}
			List<string> list = new List<string>(pressedKeys.Count);
			for (int k = 0; k < pressedKeys.Count; k++)
			{
				list.Add(pressedKeys[k].ToString());
			}
			if (WritingText)
			{
				list.Clear();
			}
			int count = list.Count;
			list.AddRange(MouseKeys);
			bool flag4 = false;
			for (int l = 0; l < list.Count; l++)
			{
				if (l < count && pressedKeys[l] == Keys.None)
				{
					continue;
				}
				string newKey = list[l];
				if (!(list[l] == Keys.Tab.ToString()) || !((flag && SocialAPI.Mode == SocialMode.Steam) || flag2))
				{
					if (CheckRebindingProcessKeyboard(newKey))
					{
						return false;
					}
					_ = Main.oldKeyState;
					if (l >= count || !Main.oldKeyState.IsKeyDown(pressedKeys[l]))
					{
						keyConfiguration.Processkey(Triggers.Current, newKey);
					}
					else
					{
						keyConfiguration.CopyKeyState(Triggers.Old, Triggers.Current, newKey);
					}
					if (l >= count || pressedKeys[l] != 0)
					{
						flag4 = true;
					}
				}
			}
			if (flag4)
			{
				CurrentInputMode = InputMode.Keyboard;
			}
			return flag4;
		}

		private static void DebugKeys(List<Keys> keys)
		{
		}

		private static void FixDerpedRebinds()
		{
			List<string> list = new List<string>
			{
				"MouseLeft",
				"MouseRight",
				"Inventory"
			};
			foreach (InputMode value in Enum.GetValues(typeof(InputMode)))
			{
				if (value == InputMode.Mouse)
				{
					continue;
				}
				FixKeysConflict(value, list);
				foreach (string item in list)
				{
					if (CurrentProfile.InputModes[value].KeyStatus[item].Count < 1)
					{
						ResetKeyBinding(value, item);
					}
				}
			}
		}

		private static void FixKeysConflict(InputMode inputMode, List<string> triggers)
		{
			for (int i = 0; i < triggers.Count; i++)
			{
				for (int j = i + 1; j < triggers.Count; j++)
				{
					List<string> list = CurrentProfile.InputModes[inputMode].KeyStatus[triggers[i]];
					List<string> list2 = CurrentProfile.InputModes[inputMode].KeyStatus[triggers[j]];
					foreach (string item in list.Intersect(list2).ToList())
					{
						list.Remove(item);
						list2.Remove(item);
					}
				}
			}
		}

		private static void ResetKeyBinding(InputMode inputMode, string trigger)
		{
			string key = "Redigit's Pick";
			if (OriginalProfiles.ContainsKey(_selectedProfile))
			{
				key = _selectedProfile;
			}
			CurrentProfile.InputModes[inputMode].KeyStatus[trigger].Clear();
			CurrentProfile.InputModes[inputMode].KeyStatus[trigger].AddRange(OriginalProfiles[key].InputModes[inputMode].KeyStatus[trigger]);
		}

		private static bool CheckRebindingProcessGamepad(string newKey)
		{
			_canReleaseRebindingLock = false;
			if (CurrentlyRebinding && _listeningInputMode == InputMode.XBoxGamepad)
			{
				NavigatorRebindingLock = 3;
				_memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12);
				if (CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[ListeningTrigger].Contains(newKey))
				{
					CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[ListeningTrigger].Remove(newKey);
				}
				else
				{
					CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				ListenFor(null, InputMode.XBoxGamepad);
			}
			if (CurrentlyRebinding && _listeningInputMode == InputMode.XBoxGamepadUI)
			{
				NavigatorRebindingLock = 3;
				_memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12);
				if (CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[ListeningTrigger].Contains(newKey))
				{
					CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[ListeningTrigger].Remove(newKey);
				}
				else
				{
					CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				ListenFor(null, InputMode.XBoxGamepadUI);
			}
			FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return NavigatorRebindingLock > 0;
		}

		private static bool CheckRebindingProcessKeyboard(string newKey)
		{
			_canReleaseRebindingLock = false;
			if (CurrentlyRebinding && _listeningInputMode == InputMode.Keyboard)
			{
				NavigatorRebindingLock = 3;
				_memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12);
				if (CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[ListeningTrigger].Contains(newKey))
				{
					CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[ListeningTrigger].Remove(newKey);
				}
				else
				{
					CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				ListenFor(null, InputMode.Keyboard);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			if (CurrentlyRebinding && _listeningInputMode == InputMode.KeyboardUI)
			{
				NavigatorRebindingLock = 3;
				_memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12);
				if (CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[ListeningTrigger].Contains(newKey))
				{
					CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[ListeningTrigger].Remove(newKey);
				}
				else
				{
					CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[ListeningTrigger] = new List<string>
					{
						newKey
					};
				}
				ListenFor(null, InputMode.KeyboardUI);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return NavigatorRebindingLock > 0;
		}

		private static void PostInput()
		{
			Main.GamepadCursorAlpha = MathHelper.Clamp(Main.GamepadCursorAlpha + ((Main.SmartCursorEnabled && !UILinkPointNavigator.Available && GamepadThumbstickLeft == Vector2.Zero && GamepadThumbstickRight == Vector2.Zero) ? (-0.05f) : 0.05f), 0f, 1f);
			if (CurrentProfile.HotbarAllowsRadial)
			{
				int num = Triggers.Current.HotbarPlus.ToInt() - Triggers.Current.HotbarMinus.ToInt();
				if (MiscSettingsTEMP.HotbarRadialShouldBeUsed)
				{
					switch (num)
					{
					case 1:
						Triggers.Current.RadialHotbar = true;
						Triggers.JustReleased.RadialHotbar = false;
						break;
					case -1:
						Triggers.Current.RadialQuickbar = true;
						Triggers.JustReleased.RadialQuickbar = false;
						break;
					}
				}
			}
			MiscSettingsTEMP.HotbarRadialShouldBeUsed = false;
		}

		private static void HandleDpadSnap()
		{
			Vector2 zero = Vector2.Zero;
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < 4; i++)
			{
				bool flag = false;
				Vector2 vector = Vector2.Zero;
				if (Main.gameMenu || (UILinkPointNavigator.Available && !InBuildingMode))
				{
					return;
				}
				switch (i)
				{
				case 0:
					flag = Triggers.Current.DpadMouseSnap1;
					vector = -Vector2.UnitY;
					break;
				case 1:
					flag = Triggers.Current.DpadMouseSnap2;
					vector = Vector2.UnitX;
					break;
				case 2:
					flag = Triggers.Current.DpadMouseSnap3;
					vector = Vector2.UnitY;
					break;
				case 3:
					flag = Triggers.Current.DpadMouseSnap4;
					vector = -Vector2.UnitX;
					break;
				}
				if (DpadSnapCooldown[i] > 0)
				{
					DpadSnapCooldown[i]--;
				}
				if (flag)
				{
					if (DpadSnapCooldown[i] == 0)
					{
						int num = 6;
						if (ItemSlot.IsABuildingItem(player.inventory[player.selectedItem]))
						{
							num = player.inventory[player.selectedItem].useTime;
						}
						DpadSnapCooldown[i] = num;
						zero += vector;
					}
				}
				else
				{
					DpadSnapCooldown[i] = 0;
				}
			}
			if (zero != Vector2.Zero)
			{
				Main.SmartCursorEnabled = false;
				Matrix zoomMatrix = Main.GameViewMatrix.ZoomMatrix;
				Matrix matrix = Matrix.Invert(zoomMatrix);
				Vector2 mouseScreen = Main.MouseScreen;
				Vector2.Transform(Main.screenPosition, matrix);
				Vector2 vector2 = Vector2.Transform((Vector2.Transform(mouseScreen, matrix) + zero * new Vector2(16f) + Main.screenPosition).ToTileCoordinates().ToWorldCoordinates() - Main.screenPosition, zoomMatrix);
				MouseX = (int)vector2.X;
				MouseY = (int)vector2.Y;
			}
		}

		public static string ComposeInstructionsForGamepad()
		{
			string text = "";
			if (!UsingGamepad)
			{
				return text;
			}
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || UILinkPointNavigator.Available)
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (InBuildingMode && !Main.gameMenu)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			KeyConfiguration keyConfiguration = CurrentProfile.InputModes[inputMode];
			if (Main.mapFullscreen && !Main.gameMenu)
			{
				text += "          ";
				text += BuildCommand(Lang.misc[56].Value, false, ProfileGamepadUI.KeyStatus["Inventory"]);
				text += BuildCommand(Lang.inter[118].Value, false, ProfileGamepadUI.KeyStatus["HotbarPlus"]);
				text += BuildCommand(Lang.inter[119].Value, false, ProfileGamepadUI.KeyStatus["HotbarMinus"]);
				if (Main.netMode == 1 && Main.player[Main.myPlayer].HasItem(2997))
				{
					text += BuildCommand(Lang.inter[120].Value, false, ProfileGamepadUI.KeyStatus["MouseRight"]);
				}
			}
			else if (inputMode == InputMode.XBoxGamepadUI && !InBuildingMode)
			{
				text = UILinkPointNavigator.GetInstructions();
			}
			else
			{
				text += BuildCommand(Lang.misc[58].Value, false, keyConfiguration.KeyStatus["Jump"]);
				text += BuildCommand(Lang.misc[59].Value, false, keyConfiguration.KeyStatus["HotbarMinus"], keyConfiguration.KeyStatus["HotbarPlus"]);
				if (InBuildingMode)
				{
					text += BuildCommand(Lang.menu[6].Value, false, keyConfiguration.KeyStatus["Inventory"], keyConfiguration.KeyStatus["MouseRight"]);
				}
				if (WiresUI.Open)
				{
					text += BuildCommand(Lang.misc[53].Value, false, keyConfiguration.KeyStatus["MouseLeft"]);
					text += BuildCommand(Lang.misc[56].Value, false, keyConfiguration.KeyStatus["MouseRight"]);
				}
				else
				{
					Item item = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
					text = ((item.damage > 0 && item.ammo == 0) ? (text + BuildCommand(Lang.misc[60].Value, false, keyConfiguration.KeyStatus["MouseLeft"])) : ((item.createTile < 0 && item.createWall <= 0) ? (text + BuildCommand(Lang.misc[63].Value, false, keyConfiguration.KeyStatus["MouseLeft"])) : (text + BuildCommand(Lang.misc[61].Value, false, keyConfiguration.KeyStatus["MouseLeft"]))));
					bool flag = true;
					bool flag2 = Main.SmartInteractProj != -1 || Main.HasInteractibleObjectThatIsNotATile;
					bool flag3 = !Main.SmartInteractShowingGenuine && Main.SmartInteractShowingFake;
					if (Main.SmartInteractShowingGenuine || Main.SmartInteractShowingFake || flag2)
					{
						if (Main.SmartInteractNPC != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							text += BuildCommand(Lang.misc[80].Value, false, keyConfiguration.KeyStatus["MouseRight"]);
						}
						else if (flag2)
						{
							if (flag3)
							{
								flag = false;
							}
							text += BuildCommand(Lang.misc[79].Value, false, keyConfiguration.KeyStatus["MouseRight"]);
						}
						else if (Main.SmartInteractX != -1 && Main.SmartInteractY != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							Tile tile = Main.tile[Main.SmartInteractX, Main.SmartInteractY];
							text = ((!TileID.Sets.TileInteractRead[tile.type]) ? (text + BuildCommand(Lang.misc[79].Value, false, keyConfiguration.KeyStatus["MouseRight"])) : (text + BuildCommand(Lang.misc[81].Value, false, keyConfiguration.KeyStatus["MouseRight"])));
						}
					}
					else if (WiresUI.Settings.DrawToolModeUI)
					{
						text += BuildCommand(Lang.misc[89].Value, false, keyConfiguration.KeyStatus["MouseRight"]);
					}
					if ((!GrappleAndInteractAreShared || (!WiresUI.Settings.DrawToolModeUI && (!Main.SmartInteractShowingGenuine || !Main.HasSmartInteractTarget) && (!Main.SmartInteractShowingFake || flag))) && Main.LocalPlayer.QuickGrapple_GetItemToUse() != null)
					{
						text += BuildCommand(Lang.misc[57].Value, false, keyConfiguration.KeyStatus["Grapple"]);
					}
				}
			}
			return text;
		}

		public static string BuildCommand(string CommandText, bool Last, params List<string>[] Bindings)
		{
			string text = "";
			if (Bindings.Length == 0)
			{
				return text;
			}
			text += GenerateGlyphList(Bindings[0]);
			for (int i = 1; i < Bindings.Length; i++)
			{
				string text2 = GenerateGlyphList(Bindings[i]);
				if (text2.Length > 0)
				{
					text = text + "/" + text2;
				}
			}
			if (text.Length > 0)
			{
				text = text + ": " + CommandText;
				if (!Last)
				{
					text += "   ";
				}
			}
			return text;
		}

		public static string GenerateInputTag_ForCurrentGamemode_WithHacks(bool tagForGameplay, string triggerName)
		{
			InputMode inputMode = CurrentInputMode;
			if (inputMode == InputMode.Mouse || inputMode == InputMode.KeyboardUI)
			{
				inputMode = InputMode.Keyboard;
			}
			if (!(triggerName == "SmartSelect"))
			{
				if (triggerName == "SmartCursor" && inputMode == InputMode.Keyboard)
				{
					return GenerateRawInputList(new List<string>
					{
						Keys.LeftAlt.ToString()
					});
				}
			}
			else if (inputMode == InputMode.Keyboard)
			{
				return GenerateRawInputList(new List<string>
				{
					Keys.LeftControl.ToString()
				});
			}
			return GenerateInputTag_ForCurrentGamemode(tagForGameplay, triggerName);
		}

		public static string GenerateInputTag_ForCurrentGamemode(bool tagForGameplay, string triggerName)
		{
			InputMode inputMode = CurrentInputMode;
			if (inputMode == InputMode.Mouse || inputMode == InputMode.KeyboardUI)
			{
				inputMode = InputMode.Keyboard;
			}
			if (tagForGameplay)
			{
				if ((uint)(inputMode - 3) > 1u)
				{
					return GenerateRawInputList(CurrentProfile.InputModes[inputMode].KeyStatus[triggerName]);
				}
				return GenerateGlyphList(CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[triggerName]);
			}
			if ((uint)(inputMode - 3) > 1u)
			{
				return GenerateRawInputList(CurrentProfile.InputModes[inputMode].KeyStatus[triggerName]);
			}
			return GenerateGlyphList(CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
		}

		public static string GenerateInputTags_GamepadUI(string triggerName)
		{
			return GenerateGlyphList(CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
		}

		public static string GenerateInputTags_Gamepad(string triggerName)
		{
			return GenerateGlyphList(CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[triggerName]);
		}

		private static string GenerateGlyphList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = GlyphTagHandler.GenerateTag(list[0]);
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + GlyphTagHandler.GenerateTag(list[i]);
			}
			return text;
		}

		private static string GenerateRawInputList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + list[i];
			}
			return text;
		}

		public static void NavigatorCachePosition()
		{
			PreUIX = MouseX;
			PreUIY = MouseY;
		}

		public static void NavigatorUnCachePosition()
		{
			MouseX = PreUIX;
			MouseY = PreUIY;
		}

		public static void LockOnCachePosition()
		{
			PreLockOnX = MouseX;
			PreLockOnY = MouseY;
		}

		public static void LockOnUnCachePosition()
		{
			MouseX = PreLockOnX;
			MouseY = PreLockOnY;
		}

		public static void PrettyPrintProfiles(string text)
		{
			string[] array = text.Split(new string[1]
			{
				"\r\n"
			}, StringSplitOptions.None);
			foreach (string text2 in array)
			{
				if (text2.Contains(": {"))
				{
					string str = text2.Substring(0, text2.IndexOf('"'));
					string text3 = text2 + "\r\n  ";
					string newValue = text3.Replace(": {\r\n  ", ": \r\n" + str + "{\r\n  ");
					text = text.Replace(text3, newValue);
				}
			}
			text = text.Replace("[\r\n        ", "[");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n        ", "\", ");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		public static void PrettyPrintProfilesOld(string text)
		{
			text = text.Replace(": {\r\n  ", ": \r\n  {\r\n  ");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		public static void Reset(KeyConfiguration c, PresetProfiles style, InputMode mode)
		{
			switch (style)
			{
			case PresetProfiles.Redigit:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					break;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.B));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.A));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					break;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.Mouse:
					break;
				}
				break;
			case PresetProfiles.Yoraiz0r:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					break;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickHeal"].Add(string.Concat(Buttons.A));
					c.KeyStatus["RadialHotbar"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					break;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.Mouse:
					break;
				}
				break;
			case PresetProfiles.ConsolePS:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					break;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					break;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.Mouse:
					break;
				}
				break;
			case PresetProfiles.ConsoleXBox:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					break;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					break;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				case InputMode.Mouse:
					break;
				}
				break;
			}
		}

		public static void SetZoom_UI()
		{
			float uIScale = Main.UIScale;
			SetZoom_Scaled(1f / uIScale);
		}

		public static void SetZoom_World()
		{
			SetZoom_Scaled(1f);
			SetZoom_MouseInWorld();
		}

		public static void SetZoom_Unscaled()
		{
			Main.lastMouseX = _originalLastMouseX;
			Main.lastMouseY = _originalLastMouseY;
			Main.mouseX = _originalMouseX;
			Main.mouseY = _originalMouseY;
			Main.screenWidth = _originalScreenWidth;
			Main.screenHeight = _originalScreenHeight;
		}

		public static void SetZoom_Test()
		{
			Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
			Vector2 value = Main.screenPosition + new Vector2(_originalMouseX, _originalMouseY);
			Vector2 value2 = Main.screenPosition + new Vector2(_originalLastMouseX, _originalLastMouseY);
			Vector2 value3 = Main.screenPosition + new Vector2(0f, 0f);
			Vector2 value4 = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight);
			Vector2 value5 = value - vector;
			Vector2 value6 = value2 - vector;
			Vector2 value7 = value3 - vector;
			_ = value4 - vector;
			float scaleFactor = 1f / Main.GameViewMatrix.Zoom.X;
			float num = 1f;
			Vector2 vector2 = vector - Main.screenPosition + value5 * scaleFactor;
			Vector2 vector3 = vector - Main.screenPosition + value6 * scaleFactor;
			Vector2 screenPosition = vector + value7 * num;
			Main.mouseX = (int)vector2.X;
			Main.mouseY = (int)vector2.Y;
			Main.lastMouseX = (int)vector3.X;
			Main.lastMouseY = (int)vector3.Y;
			Main.screenPosition = screenPosition;
			Main.screenWidth = (int)((float)_originalScreenWidth * num);
			Main.screenHeight = (int)((float)_originalScreenHeight * num);
		}

		public static void SetZoom_MouseInWorld()
		{
			Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
			Vector2 value = Main.screenPosition + new Vector2(_originalMouseX, _originalMouseY);
			Vector2 value2 = Main.screenPosition + new Vector2(_originalLastMouseX, _originalLastMouseY);
			Vector2 value3 = value - vector;
			Vector2 value4 = value2 - vector;
			float scaleFactor = 1f / Main.GameViewMatrix.Zoom.X;
			Vector2 vector2 = vector - Main.screenPosition + value3 * scaleFactor;
			Main.mouseX = (int)vector2.X;
			Main.mouseY = (int)vector2.Y;
			Vector2 vector3 = vector - Main.screenPosition + value4 * scaleFactor;
			Main.lastMouseX = (int)vector3.X;
			Main.lastMouseY = (int)vector3.Y;
		}

		public static void SetDesiredZoomContext(ZoomContext context)
		{
			_currentWantedZoom = context;
		}

		public static void SetZoom_Context()
		{
			switch (_currentWantedZoom)
			{
			case ZoomContext.Unscaled:
				SetZoom_Unscaled();
				Main.SetRecommendedZoomContext(Matrix.Identity);
				break;
			case ZoomContext.Unscaled_MouseInWorld:
				SetZoom_Unscaled();
				SetZoom_MouseInWorld();
				Main.SetRecommendedZoomContext(Main.GameViewMatrix.ZoomMatrix);
				break;
			case ZoomContext.UI:
				SetZoom_UI();
				Main.SetRecommendedZoomContext(Main.UIScaleMatrix);
				break;
			case ZoomContext.World:
				SetZoom_World();
				Main.SetRecommendedZoomContext(Main.GameViewMatrix.ZoomMatrix);
				break;
			}
		}

		private static void SetZoom_Scaled(float scale)
		{
			Main.lastMouseX = (int)((float)_originalLastMouseX * scale);
			Main.lastMouseY = (int)((float)_originalLastMouseY * scale);
			Main.mouseX = (int)((float)_originalMouseX * scale);
			Main.mouseY = (int)((float)_originalMouseY * scale);
			Main.screenWidth = (int)((float)_originalScreenWidth * scale);
			Main.screenHeight = (int)((float)_originalScreenHeight * scale);
		}
	}
}
