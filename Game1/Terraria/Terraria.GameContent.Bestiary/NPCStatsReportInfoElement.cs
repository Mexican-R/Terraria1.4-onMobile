using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	public class NPCStatsReportInfoElement : IBestiaryInfoElement
	{
		public int NpcId;

		public int GameMode;

		public int Damage;

		public int LifeMax;

		public float MonetaryValue;

		public int Defense;

		public float KnockbackResist;

		public NPCStatsReportInfoElement(int npcNetId, int gameMode)
		{
			NpcId = npcNetId;
			GameMode = gameMode;
			if (Main.RegisterdGameModes.TryGetValue(GameMode, out var value))
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(NpcId, new NPCSpawnParams
				{
					gameModeData = value,
					strengthMultiplierOverride = 1f
				});
				Damage = nPC.damage;
				LifeMax = nPC.lifeMax;
				MonetaryValue = nPC.value;
				Defense = nPC.defense;
				KnockbackResist = nPC.knockBackResist;
			}
		}

		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			int gameMode = Main.GameMode;
			if (GameMode != gameMode)
			{
				return null;
			}
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(109f, 0f)
			};
			int num = 99;
			int num2 = 35;
			int num3 = 3;
			int num4 = 0;
			UIImage uIImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP", Main.content, (AssetRequestMode)1))
			{
				Top = new StyleDimension(num4, 0f),
				Left = new StyleDimension(num3, 0f)
			};
			UIImage uIImage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Attack", Main.content, (AssetRequestMode)1))
			{
				Top = new StyleDimension(num4 + num2, 0f),
				Left = new StyleDimension(num3, 0f)
			};
			UIImage uIImage3 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense", Main.content, (AssetRequestMode)1))
			{
				Top = new StyleDimension(num4 + num2, 0f),
				Left = new StyleDimension(num3 + num, 0f)
			};
			UIImage uIImage4 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Knockback", Main.content, (AssetRequestMode)1))
			{
				Top = new StyleDimension(num4, 0f),
				Left = new StyleDimension(num3 + num, 0f)
			};
			uIElement.Append(uIImage);
			uIElement.Append(uIImage2);
			uIElement.Append(uIImage3);
			uIElement.Append(uIImage4);
			int num5 = -10;
			int num6 = 0;
			int num7 = (int)MonetaryValue;
			string text = Utils.Clamp(num7 / 1000000, 0, 999).ToString();
			string text2 = Utils.Clamp(num7 % 1000000 / 10000, 0, 99).ToString();
			string text3 = Utils.Clamp(num7 % 10000 / 100, 0, 99).ToString();
			string text4 = Utils.Clamp(num7 % 100 / 1, 0, 99).ToString();
			if (num7 / 1000000 < 1)
			{
				text = "-";
			}
			if (num7 / 10000 < 1)
			{
				text2 = "-";
			}
			if (num7 / 100 < 1)
			{
				text3 = "-";
			}
			if (num7 < 1)
			{
				text4 = "-";
			}
			string text5 = LifeMax.ToString();
			string text6 = Damage.ToString();
			string text7 = Defense.ToString();
			string text8 = ((KnockbackResist > 0.8f) ? Language.GetText("BestiaryInfo.KnockbackHigh").Value : ((KnockbackResist > 0.4f) ? Language.GetText("BestiaryInfo.KnockbackMedium").Value : ((!(KnockbackResist > 0f)) ? Language.GetText("BestiaryInfo.KnockbackNone").Value : Language.GetText("BestiaryInfo.KnockbackLow").Value)));
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				text = (text2 = (text3 = (text4 = "?")));
				text5 = (text6 = (text7 = (text8 = "???")));
			}
			UIText element = new UIText(text5)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(num5, 0f),
				Top = new StyleDimension(num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element2 = new UIText(text8)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(num5, 0f),
				Top = new StyleDimension(num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element3 = new UIText(text6)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(num5, 0f),
				Top = new StyleDimension(num6, 0f),
				IgnoresMouseInteraction = true
			};
			UIText element4 = new UIText(text7)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(num5, 0f),
				Top = new StyleDimension(num6, 0f),
				IgnoresMouseInteraction = true
			};
			uIImage.Append(element);
			uIImage2.Append(element3);
			uIImage3.Append(element4);
			uIImage4.Append(element2);
			if (num7 > 0)
			{
				UIHorizontalSeparator element5 = new UIHorizontalSeparator
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Color = new Color(89, 116, 213, 255) * 0.9f,
					Left = new StyleDimension(0f, 0f),
					Top = new StyleDimension(num6 + num2 * 2, 0f)
				};
				uIElement.Append(element5);
				int num8 = num3;
				int num9 = num6 + num2 * 2 + 8;
				int num10 = 49;
				UIImage uIImage5 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Platinum", Main.content, (AssetRequestMode)1))
				{
					Top = new StyleDimension(num9, 0f),
					Left = new StyleDimension(num8, 0f)
				};
				UIImage uIImage6 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Gold", Main.content, (AssetRequestMode)1))
				{
					Top = new StyleDimension(num9, 0f),
					Left = new StyleDimension(num8 + num10, 0f)
				};
				UIImage uIImage7 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Silver", Main.content, (AssetRequestMode)1))
				{
					Top = new StyleDimension(num9, 0f),
					Left = new StyleDimension(num8 + num10 * 2 + 1, 0f)
				};
				UIImage uIImage8 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Copper", Main.content, (AssetRequestMode)1))
				{
					Top = new StyleDimension(num9, 0f),
					Left = new StyleDimension(num8 + num10 * 3 + 1, 0f)
				};
				if (text != "-")
				{
					uIElement.Append(uIImage5);
				}
				if (text2 != "-")
				{
					uIElement.Append(uIImage6);
				}
				if (text3 != "-")
				{
					uIElement.Append(uIImage7);
				}
				if (text4 != "-")
				{
					uIElement.Append(uIImage8);
				}
				int num11 = num5 + 3;
				float textScale = 0.85f;
				UIText element6 = new UIText(text, textScale)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension(num11, 0f),
					Top = new StyleDimension(num6, 0f)
				};
				UIText element7 = new UIText(text2, textScale)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension(num11, 0f),
					Top = new StyleDimension(num6, 0f)
				};
				UIText element8 = new UIText(text3, textScale)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension(num11, 0f),
					Top = new StyleDimension(num6, 0f)
				};
				UIText element9 = new UIText(text4, textScale)
				{
					HAlign = 1f,
					VAlign = 0.5f,
					Left = new StyleDimension(num11, 0f),
					Top = new StyleDimension(num6, 0f)
				};
				uIImage5.Append(element6);
				uIImage6.Append(element7);
				uIImage7.Append(element8);
				uIImage8.Append(element9);
			}
			else
			{
				uIElement.Height.Pixels = num6 + num2 * 2 - 4;
			}
			uIImage2.OnUpdate += ShowStats_Attack;
			uIImage3.OnUpdate += ShowStats_Defense;
			uIImage.OnUpdate += ShowStats_Life;
			uIImage4.OnUpdate += ShowStats_Knockback;
			return uIElement;
		}

		private void ShowStats_Attack(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Attack"), 0, 0);
			}
		}

		private void ShowStats_Defense(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Defense"), 0, 0);
			}
		}

		private void ShowStats_Knockback(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Knockback"), 0, 0);
			}
		}

		private void ShowStats_Life(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Life"), 0, 0);
			}
		}
	}
}
