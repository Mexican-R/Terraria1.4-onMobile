using Terraria.WorldBuilding;

namespace Terraria.ID
{
	public class ProjectileID
	{
		public static class Sets
		{
			public static SetFactory Factory = new SetFactory(950);

			public static bool[] DontApplyParryDamageBuff = Factory.CreateBoolSet(false, 524, 321, 181, 566);

			public static bool[] IsAWhip = Factory.CreateBoolSet(false, 847, 841, 848, 849, 912, 913, 914, 915);

			public static bool[] ImmediatelyUpdatesNPCBuffFlags = Factory.CreateBoolSet(636);

			public static bool?[] WindPhysicsImmunity = Factory.CreateCustomSet<bool?>(null, new object[138]
			{
				(short)20,
				true,
				(short)27,
				true,
				(short)83,
				true,
				(short)84,
				true,
				(short)88,
				true,
				(short)100,
				true,
				(short)359,
				true,
				(short)119,
				true,
				(short)121,
				true,
				(short)122,
				true,
				(short)123,
				true,
				(short)124,
				true,
				(short)125,
				true,
				(short)126,
				true,
				(short)309,
				true,
				(short)128,
				true,
				(short)129,
				true,
				(short)257,
				true,
				(short)258,
				true,
				(short)259,
				true,
				(short)299,
				true,
				(short)496,
				true,
				(short)302,
				true,
				(short)306,
				true,
				(short)337,
				true,
				(short)344,
				true,
				(short)343,
				true,
				(short)342,
				true,
				(short)348,
				true,
				(short)349,
				true,
				(short)389,
				true,
				(short)436,
				true,
				(short)435,
				true,
				(short)437,
				true,
				(short)439,
				true,
				(short)592,
				true,
				(short)449,
				true,
				(short)442,
				true,
				(short)459,
				true,
				(short)462,
				true,
				(short)467,
				true,
				(short)468,
				true,
				(short)538,
				true,
				(short)576,
				true,
				(short)577,
				true,
				(short)584,
				true,
				(short)583,
				true,
				(short)594,
				true,
				(short)622,
				true,
				(short)597,
				true,
				(short)601,
				true,
				(short)617,
				true,
				(short)619,
				true,
				(short)620,
				true,
				(short)618,
				true,
				(short)634,
				true,
				(short)635,
				true,
				(short)640,
				true,
				(short)639,
				true,
				(short)645,
				true,
				(short)660,
				true,
				(short)661,
				true,
				(short)675,
				true,
				(short)684,
				true,
				(short)709,
				true,
				(short)593,
				true,
				(short)606,
				true,
				(short)732,
				true,
				(short)731,
				true
			});

			public static bool[] RocketsSkipDamageForPlayers = Factory.CreateBoolSet(338, 339, 340, 341, 803, 804, 862, 863, 805, 806, 807, 808, 809, 810, 930);

			public static float[] YoyosLifeTimeMultiplier = Factory.CreateFloatSet(-1f, 541f, 3f, 548f, 5f, 542f, 7f, 543f, 6f, 544f, 8f, 534f, 9f, 564f, 11f, 545f, 13f, 563f, 10f, 562f, 8f, 553f, 12f, 546f, 16f, 552f, 15f, 549f, 14f);

			public static float[] YoyosMaximumRange = Factory.CreateFloatSet(200f, 541f, 130f, 548f, 170f, 542f, 195f, 543f, 207f, 544f, 215f, 534f, 220f, 564f, 225f, 545f, 235f, 562f, 235f, 563f, 250f, 546f, 275f, 552f, 270f, 553f, 275f, 547f, 280f, 549f, 290f, 554f, 340f, 550f, 370f, 551f, 370f, 555f, 360f, 603f, 400f);

			public static bool[] IsAGolfBall = Factory.CreateBoolSet(false, 721, 739, 740, 741, 742, 743, 744, 745, 746, 747, 748, 749, 750, 751, 752);

			public static float[] YoyosTopSpeed = Factory.CreateFloatSet(10f, 541f, 9f, 548f, 11f, 542f, 12.5f, 543f, 12f, 544f, 13f, 534f, 13f, 564f, 14f, 545f, 14f, 562f, 15f, 563f, 12f, 546f, 17f, 552f, 14f, 553f, 15f, 547f, 17f, 549f, 16f, 554f, 16f, 550f, 16f, 551f, 16f, 555f, 16.5f, 603f, 17.5f);

			public static bool[] CanDistortWater = Factory.CreateBoolSet(true, 7, 8, 152, 151, 150, 493, 494);

			public static bool[] MinionShot = Factory.CreateBoolSet(374, 376, 389, 195, 408, 433, 614);

			public static bool[] SentryShot = Factory.CreateBoolSet(680, 664, 666, 668, 694, 695, 696, 644, 642, 378, 379, 309);

			public static bool?[] ForcePlateDetection = Factory.CreateCustomSet<bool?>(null, new object[66]
			{
				(short)397,
				true,
				(short)37,
				true,
				(short)470,
				true,
				(short)53,
				true,
				(short)911,
				true,
				(short)773,
				true,
				(short)519,
				true,
				(short)171,
				true,
				(short)505,
				true,
				(short)475,
				true,
				(short)506,
				true,
				(short)186,
				true,
				(short)80,
				true,
				(short)40,
				true,
				(short)241,
				true,
				(short)411,
				true,
				(short)56,
				true,
				(short)413,
				true,
				(short)67,
				true,
				(short)414,
				true,
				(short)31,
				true,
				(short)412,
				true,
				(short)812,
				true,
				(short)17,
				true,
				(short)166,
				true,
				(short)109,
				true,
				(short)354,
				true,
				(short)65,
				true,
				(short)68,
				true,
				(short)42,
				true,
				(short)99,
				false,
				(short)727,
				false,
				(short)655,
				false
			});

			public static int[] TrailingMode = Factory.CreateIntSet(-1, 94, 0, 301, 0, 388, 0, 385, 0, 408, 0, 409, 0, 435, 0, 436, 0, 437, 0, 438, 0, 452, 0, 459, 0, 462, 0, 502, 0, 503, 0, 466, 1, 532, 0, 533, 0, 573, 0, 580, 1, 582, 0, 585, 0, 592, 0, 601, 0, 617, 0, 636, 0, 638, 0, 639, 0, 640, 0, 424, 0, 425, 0, 426, 0, 660, 0, 661, 0, 671, 2, 664, 0, 666, 0, 668, 0, 675, 0, 680, 2, 682, 0, 684, 0, 686, 2, 700, 0, 706, 0, 709, 0, 710, 2, 711, 2, 712, 0, 715, 2, 716, 2, 717, 2, 718, 2, 261, 0, 721, 0, 729, 2, 732, 0, 731, 0, 739, 0, 740, 0, 741, 0, 742, 0, 743, 0, 744, 0, 745, 0, 746, 0, 747, 0, 748, 0, 749, 0, 750, 0, 751, 0, 752, 0, 755, 2, 766, 2, 767, 2, 768, 2, 769, 2, 770, 2, 771, 2, 811, 2, 814, 2, 822, 2, 823, 2, 824, 2, 826, 2, 827, 2, 828, 2, 829, 2, 830, 2, 838, 2, 839, 2, 840, 2, 843, 2, 844, 2, 845, 2, 846, 2, 850, 2, 852, 2, 853, 2, 856, 0, 857, 0, 864, 2, 873, 2, 872, 2, 833, 2, 834, 2, 835, 2, 818, 2, 902, 0, 883, 0, 887, 0, 893, 0, 894, 0, 909, 0, 916, 2, 34, 3, 16, 3, 79, 3, 931, 2, 933, 4, 946, 2);

			public static int[] TrailCacheLength = Factory.CreateIntSet(10, 466, 20, 502, 25, 580, 20, 636, 20, 640, 20, 686, 20, 711, 20, 715, 20, 716, 20, 717, 20, 718, 20, 261, 20, 721, 20, 729, 20, 731, 20, 739, 20, 740, 20, 741, 20, 742, 20, 743, 20, 744, 20, 745, 20, 746, 20, 747, 20, 748, 20, 749, 20, 750, 20, 751, 20, 752, 20, 766, 60, 767, 60, 768, 60, 769, 60, 770, 60, 771, 80, 814, 40, 822, 80, 823, 80, 824, 60, 826, 60, 827, 65, 828, 60, 829, 60, 830, 80, 838, 80, 839, 60, 840, 60, 843, 60, 844, 65, 845, 80, 846, 80, 850, 80, 852, 60, 853, 60, 856, 2, 857, 2, 864, 60, 873, 60, 872, 120, 833, 20, 834, 20, 835, 20, 818, 20, 883, 41, 887, 51, 893, 71, 894, 10, 909, 10, 916, 20, 34, 30, 16, 30, 79, 60, 931, 20, 933, 60, 946, 20);

			public static bool[] LightPet = Factory.CreateBoolSet(18, 500, 72, 87, 86, 211, 492, 650, 702, 891, 896, 895);

			public static bool[] CountsAsHoming = Factory.CreateBoolSet(207, 182, 247, 338, 339, 340, 341, 191, 192, 193, 194, 266, 390, 391, 392, 307, 316, 190, 227, 226, 254, 255, 297, 308, 317, 321, 407, 423, 375, 373, 376, 374, 379, 387, 408, 389, 388, 405, 409, 451, 535, 536, 483, 484, 477);

			public static bool[] IsADD2Turret = Factory.CreateBoolSet(663, 665, 667, 677, 678, 679, 688, 689, 690, 691, 692, 693);

			public static bool[] TurretFeature = Factory.CreateBoolSet();

			public static bool[] MinionTargettingFeature = Factory.CreateBoolSet(191, 192, 193, 194, 266, 317, 373, 375, 387, 388, 390, 393, 407, 423, 533, 613, 625, 755, 758, 759, 831, 833, 834, 835, 864, 946, 377, 308, 643, 641, 663, 665, 667, 677, 678, 679, 688, 689, 690, 691, 692, 693);

			public static bool[] MinionSacrificable = Factory.CreateBoolSet(191, 192, 193, 194, 266, 317, 373, 375, 387, 388, 390, 393, 407, 423, 533, 613, 755, 758, 759, 831, 864, 946, 625, 626, 627, 628);

			public static bool[] DontAttachHideToAlpha = Factory.CreateBoolSet(598, 641, 617, 636, 579, 578, 625, 626, 627, 628, 759, 813, 525);

			public static GenSearch[] ExtendedCanHitCheckSearch = Factory.CreateCustomSet<GenSearch>(null, new object[6]
			{
				(short)833,
				new Searches.Up(3),
				(short)834,
				new Searches.Up(3),
				(short)835,
				new Searches.Up(3)
			});

			public static float[] ExtendedCanHitCheckRange = Factory.CreateFloatSet(0f, 833f, 48f, 834f, 48f, 835f, 48f);

			public static bool[] NeedsUUID = Factory.CreateBoolSet(625, 626, 627, 628);

			public static bool[] StardustDragon = Factory.CreateBoolSet(625, 626, 627, 628);

			public static bool[] StormTiger = Factory.CreateBoolSet(833, 834, 835);

			public static int[] StormTigerIds = new int[3]
			{
				833,
				834,
				835
			};

			public static bool[] IsARocketThatDealsDoubleDamageToPrimaryEnemy = Factory.CreateBoolSet(134, 137, 140, 143, 776, 780, 793, 796, 799, 784, 787, 790, 246);

			public static bool[] IsAMineThatDealsTripleDamageWhenStationary = Factory.CreateBoolSet(135, 138, 141, 144, 778, 782, 795, 798, 801, 786, 789, 792);

			public static bool[] NoLiquidDistortion = Factory.CreateBoolSet(511, 512, 513);

			public static bool[] DismountsPlayersOnHit = Factory.CreateBoolSet(877, 878, 879);

			public static int[] DrawScreenCheckFluff = Factory.CreateIntSet(480, 461, 1600, 632, 1600, 447, 1600, 455, 2400, 754, 800, 872, 1600, 873, 1600, 871, 1600, 919, 2400, 923, 2400, 931, 960, 16, 960, 34, 960, 79, 960, 933, 480);
		}

		public const short None = 0;

		public const short WoodenArrowFriendly = 1;

		public const short FireArrow = 2;

		public const short Shuriken = 3;

		public const short UnholyArrow = 4;

		public const short JestersArrow = 5;

		public const short EnchantedBoomerang = 6;

		public const short VilethornBase = 7;

		public const short VilethornTip = 8;

		public const short Starfury = 9;

		public const short PurificationPowder = 10;

		public const short VilePowder = 11;

		public const short FallingStar = 12;

		public const short Hook = 13;

		public const short Bullet = 14;

		public const short BallofFire = 15;

		public const short MagicMissile = 16;

		public const short DirtBall = 17;

		public const short ShadowOrb = 18;

		public const short Flamarang = 19;

		public const short GreenLaser = 20;

		public const short Bone = 21;

		public const short WaterStream = 22;

		public const short Harpoon = 23;

		public const short SpikyBall = 24;

		public const short BallOHurt = 25;

		public const short BlueMoon = 26;

		public const short WaterBolt = 27;

		public const short Bomb = 28;

		public const short Dynamite = 29;

		public const short Grenade = 30;

		public const short SandBallFalling = 31;

		public const short IvyWhip = 32;

		public const short ThornChakram = 33;

		public const short Flamelash = 34;

		public const short Sunfury = 35;

		public const short MeteorShot = 36;

		public const short StickyBomb = 37;

		public const short HarpyFeather = 38;

		public const short MudBall = 39;

		public const short AshBallFalling = 40;

		public const short HellfireArrow = 41;

		public const short SandBallGun = 42;

		public const short Tombstone = 43;

		public const short DemonSickle = 44;

		public const short DemonScythe = 45;

		public const short DarkLance = 46;

		public const short Trident = 47;

		public const short ThrowingKnife = 48;

		public const short Spear = 49;

		public const short Glowstick = 50;

		public const short Seed = 51;

		public const short WoodenBoomerang = 52;

		public const short StickyGlowstick = 53;

		public const short PoisonedKnife = 54;

		public const short Stinger = 55;

		public const short EbonsandBallFalling = 56;

		public const short CobaltChainsaw = 57;

		public const short MythrilChainsaw = 58;

		public const short CobaltDrill = 59;

		public const short MythrilDrill = 60;

		public const short AdamantiteChainsaw = 61;

		public const short AdamantiteDrill = 62;

		public const short TheDaoofPow = 63;

		public const short MythrilHalberd = 64;

		public const short EbonsandBallGun = 65;

		public const short AdamantiteGlaive = 66;

		public const short PearlSandBallFalling = 67;

		public const short PearlSandBallGun = 68;

		public const short HolyWater = 69;

		public const short UnholyWater = 70;

		public const short SiltBall = 71;

		public const short BlueFairy = 72;

		public const short DualHookBlue = 73;

		public const short DualHookRed = 74;

		public const short HappyBomb = 75;

		public const short QuarterNote = 76;

		public const short EighthNote = 77;

		public const short TiedEighthNote = 78;

		public const short RainbowRodBullet = 79;

		public const short IceBlock = 80;

		public const short WoodenArrowHostile = 81;

		public const short FlamingArrow = 82;

		public const short EyeLaser = 83;

		public const short PinkLaser = 84;

		public const short Flames = 85;

		public const short PinkFairy = 86;

		public const short GreenFairy = 87;

		public const short PurpleLaser = 88;

		public const short CrystalBullet = 89;

		public const short CrystalShard = 90;

		public const short HolyArrow = 91;

		public const short HallowStar = 92;

		public const short MagicDagger = 93;

		public const short CrystalStorm = 94;

		public const short CursedFlameFriendly = 95;

		public const short CursedFlameHostile = 96;

		public const short CobaltNaginata = 97;

		public const short PoisonDart = 98;

		public const short Boulder = 99;

		public const short DeathLaser = 100;

		public const short EyeFire = 101;

		public const short BombSkeletronPrime = 102;

		public const short CursedArrow = 103;

		public const short CursedBullet = 104;

		public const short Gungnir = 105;

		public const short LightDisc = 106;

		public const short Hamdrax = 107;

		public const short Explosives = 108;

		public const short SnowBallHostile = 109;

		public const short BulletSnowman = 110;

		public const short Bunny = 111;

		public const short Penguin = 112;

		public const short IceBoomerang = 113;

		public const short UnholyTridentFriendly = 114;

		public const short UnholyTridentHostile = 115;

		public const short SwordBeam = 116;

		public const short BoneArrow = 117;

		public const short IceBolt = 118;

		public const short FrostBoltSword = 119;

		public const short FrostArrow = 120;

		public const short AmethystBolt = 121;

		public const short TopazBolt = 122;

		public const short SapphireBolt = 123;

		public const short EmeraldBolt = 124;

		public const short RubyBolt = 125;

		public const short DiamondBolt = 126;

		public const short Turtle = 127;

		public const short FrostBlastHostile = 128;

		public const short RuneBlast = 129;

		public const short MushroomSpear = 130;

		public const short Mushroom = 131;

		public const short TerraBeam = 132;

		public const short GrenadeI = 133;

		public const short RocketI = 134;

		public const short ProximityMineI = 135;

		public const short GrenadeII = 136;

		public const short RocketII = 137;

		public const short ProximityMineII = 138;

		public const short GrenadeIII = 139;

		public const short RocketIII = 140;

		public const short ProximityMineIII = 141;

		public const short GrenadeIV = 142;

		public const short RocketIV = 143;

		public const short ProximityMineIV = 144;

		public const short PureSpray = 145;

		public const short HallowSpray = 146;

		public const short CorruptSpray = 147;

		public const short MushroomSpray = 148;

		public const short CrimsonSpray = 149;

		public const short NettleBurstRight = 150;

		public const short NettleBurstLeft = 151;

		public const short NettleBurstEnd = 152;

		public const short TheRottedFork = 153;

		public const short TheMeatball = 154;

		public const short BeachBall = 155;

		public const short LightBeam = 156;

		public const short NightBeam = 157;

		public const short CopperCoin = 158;

		public const short SilverCoin = 159;

		public const short GoldCoin = 160;

		public const short PlatinumCoin = 161;

		public const short CannonballFriendly = 162;

		public const short Flare = 163;

		public const short Landmine = 164;

		public const short Web = 165;

		public const short SnowBallFriendly = 166;

		public const short RocketFireworkRed = 167;

		public const short RocketFireworkGreen = 168;

		public const short RocketFireworkBlue = 169;

		public const short RocketFireworkYellow = 170;

		public const short RopeCoil = 171;

		public const short FrostburnArrow = 172;

		public const short EnchantedBeam = 173;

		public const short IceSpike = 174;

		public const short BabyEater = 175;

		public const short JungleSpike = 176;

		public const short IcewaterSpit = 177;

		public const short ConfettiGun = 178;

		public const short SlushBall = 179;

		public const short BulletDeadeye = 180;

		public const short Bee = 181;

		public const short PossessedHatchet = 182;

		public const short Beenade = 183;

		public const short PoisonDartTrap = 184;

		public const short SpikyBallTrap = 185;

		public const short SpearTrap = 186;

		public const short FlamethrowerTrap = 187;

		public const short FlamesTrap = 188;

		public const short Wasp = 189;

		public const short MechanicalPiranha = 190;

		public const short Pygmy = 191;

		public const short Pygmy2 = 192;

		public const short Pygmy3 = 193;

		public const short Pygmy4 = 194;

		public const short PygmySpear = 195;

		public const short SmokeBomb = 196;

		public const short BabySkeletronHead = 197;

		public const short BabyHornet = 198;

		public const short TikiSpirit = 199;

		public const short PetLizard = 200;

		public const short GraveMarker = 201;

		public const short CrossGraveMarker = 202;

		public const short Headstone = 203;

		public const short Gravestone = 204;

		public const short Obelisk = 205;

		public const short Leaf = 206;

		public const short ChlorophyteBullet = 207;

		public const short Parrot = 208;

		public const short Truffle = 209;

		public const short Sapling = 210;

		public const short Wisp = 211;

		public const short PalladiumPike = 212;

		public const short PalladiumDrill = 213;

		public const short PalladiumChainsaw = 214;

		public const short OrichalcumHalberd = 215;

		public const short OrichalcumDrill = 216;

		public const short OrichalcumChainsaw = 217;

		public const short TitaniumTrident = 218;

		public const short TitaniumDrill = 219;

		public const short TitaniumChainsaw = 220;

		public const short FlowerPetal = 221;

		public const short ChlorophytePartisan = 222;

		public const short ChlorophyteDrill = 223;

		public const short ChlorophyteChainsaw = 224;

		public const short ChlorophyteArrow = 225;

		public const short CrystalLeaf = 226;

		public const short CrystalLeafShot = 227;

		public const short SporeCloud = 228;

		public const short ChlorophyteOrb = 229;

		public const short GemHookAmethyst = 230;

		public const short GemHookTopaz = 231;

		public const short GemHookSapphire = 232;

		public const short GemHookEmerald = 233;

		public const short GemHookRuby = 234;

		public const short GemHookDiamond = 235;

		public const short BabyDino = 236;

		public const short RainCloudMoving = 237;

		public const short RainCloudRaining = 238;

		public const short RainFriendly = 239;

		public const short CannonballHostile = 240;

		public const short CrimsandBallFalling = 241;

		public const short BulletHighVelocity = 242;

		public const short BloodCloudMoving = 243;

		public const short BloodCloudRaining = 244;

		public const short BloodRain = 245;

		public const short Stynger = 246;

		public const short FlowerPow = 247;

		public const short FlowerPowPetal = 248;

		public const short StyngerShrapnel = 249;

		public const short RainbowFront = 250;

		public const short RainbowBack = 251;

		public const short ChlorophyteJackhammer = 252;

		public const short BallofFrost = 253;

		public const short MagnetSphereBall = 254;

		public const short MagnetSphereBolt = 255;

		public const short SkeletronHand = 256;

		public const short FrostBeam = 257;

		public const short Fireball = 258;

		public const short EyeBeam = 259;

		public const short HeatRay = 260;

		public const short BoulderStaffOfEarth = 261;

		public const short GolemFist = 262;

		public const short IceSickle = 263;

		public const short RainNimbus = 264;

		public const short PoisonFang = 265;

		public const short BabySlime = 266;

		public const short PoisonDartBlowgun = 267;

		public const short EyeSpring = 268;

		public const short BabySnowman = 269;

		public const short Skull = 270;

		public const short BoxingGlove = 271;

		public const short Bananarang = 272;

		public const short ChainKnife = 273;

		public const short DeathSickle = 274;

		public const short SeedPlantera = 275;

		public const short PoisonSeedPlantera = 276;

		public const short ThornBall = 277;

		public const short IchorArrow = 278;

		public const short IchorBullet = 279;

		public const short GoldenShowerFriendly = 280;

		public const short ExplosiveBunny = 281;

		public const short VenomArrow = 282;

		public const short VenomBullet = 283;

		public const short PartyBullet = 284;

		public const short NanoBullet = 285;

		public const short ExplosiveBullet = 286;

		public const short GoldenBullet = 287;

		public const short GoldenShowerHostile = 288;

		public const short ConfettiMelee = 289;

		public const short ShadowBeamHostile = 290;

		public const short InfernoHostileBolt = 291;

		public const short InfernoHostileBlast = 292;

		public const short LostSoulHostile = 293;

		public const short ShadowBeamFriendly = 294;

		public const short InfernoFriendlyBolt = 295;

		public const short InfernoFriendlyBlast = 296;

		public const short LostSoulFriendly = 297;

		public const short SpiritHeal = 298;

		public const short Shadowflames = 299;

		public const short PaladinsHammerHostile = 300;

		public const short PaladinsHammerFriendly = 301;

		public const short SniperBullet = 302;

		public const short RocketSkeleton = 303;

		public const short VampireKnife = 304;

		public const short VampireHeal = 305;

		public const short EatersBite = 306;

		public const short TinyEater = 307;

		public const short FrostHydra = 308;

		public const short FrostBlastFriendly = 309;

		public const short BlueFlare = 310;

		public const short CandyCorn = 311;

		public const short JackOLantern = 312;

		public const short Spider = 313;

		public const short Squashling = 314;

		public const short BatHook = 315;

		public const short Bat = 316;

		public const short Raven = 317;

		public const short RottenEgg = 318;

		public const short BlackCat = 319;

		public const short BloodyMachete = 320;

		public const short FlamingJack = 321;

		public const short WoodHook = 322;

		public const short Stake = 323;

		public const short CursedSapling = 324;

		public const short FlamingWood = 325;

		public const short GreekFire1 = 326;

		public const short GreekFire2 = 327;

		public const short GreekFire3 = 328;

		public const short FlamingScythe = 329;

		public const short StarAnise = 330;

		public const short CandyCaneHook = 331;

		public const short ChristmasHook = 332;

		public const short FruitcakeChakram = 333;

		public const short Puppy = 334;

		public const short OrnamentFriendly = 335;

		public const short PineNeedleFriendly = 336;

		public const short Blizzard = 337;

		public const short RocketSnowmanI = 338;

		public const short RocketSnowmanII = 339;

		public const short RocketSnowmanIII = 340;

		public const short RocketSnowmanIV = 341;

		public const short NorthPoleWeapon = 342;

		public const short NorthPoleSpear = 343;

		public const short NorthPoleSnowflake = 344;

		public const short PineNeedleHostile = 345;

		public const short OrnamentHostile = 346;

		public const short OrnamentHostileShrapnel = 347;

		public const short FrostWave = 348;

		public const short FrostShard = 349;

		public const short Missile = 350;

		public const short Present = 351;

		public const short Spike = 352;

		public const short BabyGrinch = 353;

		public const short CrimsandBallGun = 354;

		public const short VenomFang = 355;

		public const short SpectreWrath = 356;

		public const short PulseBolt = 357;

		public const short WaterGun = 358;

		public const short FrostBoltStaff = 359;

		public const short BobberWooden = 360;

		public const short BobberReinforced = 361;

		public const short BobberFiberglass = 362;

		public const short BobberFisherOfSouls = 363;

		public const short BobberGolden = 364;

		public const short BobberMechanics = 365;

		public const short BobbersittingDuck = 366;

		public const short ObsidianSwordfish = 367;

		public const short Swordfish = 368;

		public const short SawtoothShark = 369;

		public const short LovePotion = 370;

		public const short FoulPotion = 371;

		public const short FishHook = 372;

		public const short Hornet = 373;

		public const short HornetStinger = 374;

		public const short FlyingImp = 375;

		public const short ImpFireball = 376;

		public const short SpiderHiver = 377;

		public const short SpiderEgg = 378;

		public const short BabySpider = 379;

		public const short ZephyrFish = 380;

		public const short BobberFleshcatcher = 381;

		public const short BobberHotline = 382;

		public const short Anchor = 383;

		public const short Sharknado = 384;

		public const short SharknadoBolt = 385;

		public const short Cthulunado = 386;

		public const short Retanimini = 387;

		public const short Spazmamini = 388;

		public const short MiniRetinaLaser = 389;

		public const short VenomSpider = 390;

		public const short JumperSpider = 391;

		public const short DangerousSpider = 392;

		public const short OneEyedPirate = 393;

		public const short SoulscourgePirate = 394;

		public const short PirateCaptain = 395;

		public const short SlimeHook = 396;

		public const short StickyGrenade = 397;

		public const short MiniMinotaur = 398;

		public const short MolotovCocktail = 399;

		public const short MolotovFire = 400;

		public const short MolotovFire2 = 401;

		public const short MolotovFire3 = 402;

		public const short TrackHook = 403;

		public const short Flairon = 404;

		public const short FlaironBubble = 405;

		public const short SlimeGun = 406;

		public const short Tempest = 407;

		public const short MiniSharkron = 408;

		public const short Typhoon = 409;

		public const short Bubble = 410;

		public const short CopperCoinsFalling = 411;

		public const short SilverCoinsFalling = 412;

		public const short GoldCoinsFalling = 413;

		public const short PlatinumCoinsFalling = 414;

		public const short RocketFireworksBoxRed = 415;

		public const short RocketFireworksBoxGreen = 416;

		public const short RocketFireworksBoxBlue = 417;

		public const short RocketFireworksBoxYellow = 418;

		public const short FireworkFountainYellow = 419;

		public const short FireworkFountainRed = 420;

		public const short FireworkFountainBlue = 421;

		public const short FireworkFountainRainbow = 422;

		public const short UFOMinion = 423;

		public const short Meteor1 = 424;

		public const short Meteor2 = 425;

		public const short Meteor3 = 426;

		public const short VortexChainsaw = 427;

		public const short VortexDrill = 428;

		public const short NebulaChainsaw = 429;

		public const short NebulaDrill = 430;

		public const short SolarFlareChainsaw = 431;

		public const short SolarFlareDrill = 432;

		public const short UFOLaser = 433;

		public const short ScutlixLaserFriendly = 434;

		public const short MartianTurretBolt = 435;

		public const short BrainScramblerBolt = 436;

		public const short GigaZapperSpear = 437;

		public const short RayGunnerLaser = 438;

		public const short LaserMachinegun = 439;

		public const short LaserMachinegunLaser = 440;

		public const short ScutlixLaserCrosshair = 441;

		public const short ElectrosphereMissile = 442;

		public const short Electrosphere = 443;

		public const short Xenopopper = 444;

		public const short LaserDrill = 445;

		public const short AntiGravityHook = 446;

		public const short SaucerDeathray = 447;

		public const short SaucerMissile = 448;

		public const short SaucerLaser = 449;

		public const short SaucerScrap = 450;

		public const short InfluxWaver = 451;

		public const short PhantasmalEye = 452;

		public const short DrillMountCrosshair = 453;

		public const short PhantasmalSphere = 454;

		public const short PhantasmalDeathray = 455;

		public const short MoonLeech = 456;

		public const short PhasicWarpEjector = 457;

		public const short PhasicWarpDisc = 458;

		public const short ChargedBlasterOrb = 459;

		public const short ChargedBlasterCannon = 460;

		public const short ChargedBlasterLaser = 461;

		public const short PhantasmalBolt = 462;

		public const short ViciousPowder = 463;

		public const short CultistBossIceMist = 464;

		public const short CultistBossLightningOrb = 465;

		public const short CultistBossLightningOrbArc = 466;

		public const short CultistBossFireBall = 467;

		public const short CultistBossFireBallClone = 468;

		public const short BeeArrow = 469;

		public const short StickyDynamite = 470;

		public const short SkeletonBone = 471;

		public const short WebSpit = 472;

		public const short SpelunkerGlowstick = 473;

		public const short BoneArrowFromMerchant = 474;

		public const short VineRopeCoil = 475;

		public const short SoulDrain = 476;

		public const short CrystalDart = 477;

		public const short CursedDart = 478;

		public const short IchorDart = 479;

		public const short CursedDartFlame = 480;

		public const short ChainGuillotine = 481;

		public const short ClingerStaff = 482;

		public const short SeedlerNut = 483;

		public const short SeedlerThorn = 484;

		public const short Hellwing = 485;

		public const short TendonHook = 486;

		public const short ThornHook = 487;

		public const short IlluminantHook = 488;

		public const short WormHook = 489;

		public const short CultistRitual = 490;

		public const short FlyingKnife = 491;

		public const short MagicLantern = 492;

		public const short CrystalVileShardHead = 493;

		public const short CrystalVileShardShaft = 494;

		public const short ShadowFlameArrow = 495;

		public const short ShadowFlame = 496;

		public const short ShadowFlameKnife = 497;

		public const short Nail = 498;

		public const short BabyFaceMonster = 499;

		public const short CrimsonHeart = 500;

		public const short DrManFlyFlask = 501;

		public const short Meowmere = 502;

		public const short StarWrath = 503;

		public const short Spark = 504;

		public const short SilkRopeCoil = 505;

		public const short WebRopeCoil = 506;

		public const short JavelinFriendly = 507;

		public const short JavelinHostile = 508;

		public const short ButchersChainsaw = 509;

		public const short ToxicFlask = 510;

		public const short ToxicCloud = 511;

		public const short ToxicCloud2 = 512;

		public const short ToxicCloud3 = 513;

		public const short NailFriendly = 514;

		public const short BouncyGlowstick = 515;

		public const short BouncyBomb = 516;

		public const short BouncyGrenade = 517;

		public const short CoinPortal = 518;

		public const short BombFish = 519;

		public const short FrostDaggerfish = 520;

		public const short CrystalPulse = 521;

		public const short CrystalPulse2 = 522;

		public const short ToxicBubble = 523;

		public const short IchorSplash = 524;

		public const short FlyingPiggyBank = 525;

		public const short CultistBossParticle = 526;

		public const short RichGravestone1 = 527;

		public const short RichGravestone2 = 528;

		public const short RichGravestone3 = 529;

		public const short RichGravestone4 = 530;

		public const short RichGravestone5 = 531;

		public const short BoneGloveProj = 532;

		public const short DeadlySphere = 533;

		public const short Code1 = 534;

		public const short MedusaHead = 535;

		public const short MedusaHeadRay = 536;

		public const short StardustSoldierLaser = 537;

		public const short Twinkle = 538;

		public const short StardustJellyfishSmall = 539;

		public const short StardustTowerMark = 540;

		public const short WoodYoyo = 541;

		public const short CorruptYoyo = 542;

		public const short CrimsonYoyo = 543;

		public const short JungleYoyo = 544;

		public const short Cascade = 545;

		public const short Chik = 546;

		public const short Code2 = 547;

		public const short Rally = 548;

		public const short Yelets = 549;

		public const short RedsYoyo = 550;

		public const short ValkyrieYoyo = 551;

		public const short Amarok = 552;

		public const short HelFire = 553;

		public const short Kraken = 554;

		public const short TheEyeOfCthulhu = 555;

		public const short BlackCounterweight = 556;

		public const short BlueCounterweight = 557;

		public const short GreenCounterweight = 558;

		public const short PurpleCounterweight = 559;

		public const short RedCounterweight = 560;

		public const short YellowCounterweight = 561;

		public const short FormatC = 562;

		public const short Gradient = 563;

		public const short Valor = 564;

		public const short BrainOfConfusion = 565;

		public const short GiantBee = 566;

		public const short SporeTrap = 567;

		public const short SporeTrap2 = 568;

		public const short SporeGas = 569;

		public const short SporeGas2 = 570;

		public const short SporeGas3 = 571;

		public const short SalamanderSpit = 572;

		public const short NebulaBolt = 573;

		public const short NebulaEye = 574;

		public const short NebulaSphere = 575;

		public const short NebulaLaser = 576;

		public const short VortexLaser = 577;

		public const short VortexVortexLightning = 578;

		public const short VortexVortexPortal = 579;

		public const short VortexLightning = 580;

		public const short VortexAcid = 581;

		public const short MechanicWrench = 582;

		public const short NurseSyringeHurt = 583;

		public const short NurseSyringeHeal = 584;

		public const short ClothiersCurse = 585;

		public const short DryadsWardCircle = 586;

		public const short PainterPaintball = 587;

		public const short PartyGirlGrenade = 588;

		public const short SantaBombs = 589;

		public const short TruffleSpore = 590;

		public const short MinecartMechLaser = 591;

		public const short MartianWalkerLaser = 592;

		public const short AncientDoomProjectile = 593;

		public const short BlowupSmoke = 594;

		public const short Arkhalis = 595;

		public const short DesertDjinnCurse = 596;

		public const short AmberBolt = 597;

		public const short BoneJavelin = 598;

		public const short BoneDagger = 599;

		public const short PortalGun = 600;

		public const short PortalGunBolt = 601;

		public const short PortalGunGate = 602;

		public const short Terrarian = 603;

		public const short TerrarianBeam = 604;

		public const short SpikedSlimeSpike = 605;

		public const short ScutlixLaser = 606;

		public const short SolarFlareRay = 607;

		public const short SolarCounter = 608;

		public const short StardustDrill = 609;

		public const short StardustChainsaw = 610;

		public const short SolarWhipSword = 611;

		public const short SolarWhipSwordExplosion = 612;

		public const short StardustCellMinion = 613;

		public const short StardustCellMinionShot = 614;

		public const short VortexBeater = 615;

		public const short VortexBeaterRocket = 616;

		public const short NebulaArcanum = 617;

		public const short NebulaArcanumSubshot = 618;

		public const short NebulaArcanumExplosionShot = 619;

		public const short NebulaArcanumExplosionShotShard = 620;

		public const short BloodWater = 621;

		public const short BlowupSmokeMoonlord = 622;

		public const short StardustGuardian = 623;

		public const short StardustGuardianExplosion = 624;

		public const short StardustDragon1 = 625;

		public const short StardustDragon2 = 626;

		public const short StardustDragon3 = 627;

		public const short StardustDragon4 = 628;

		public const short TowerDamageBolt = 629;

		public const short Phantasm = 630;

		public const short PhantasmArrow = 631;

		public const short LastPrismLaser = 632;

		public const short LastPrism = 633;

		public const short NebulaBlaze1 = 634;

		public const short NebulaBlaze2 = 635;

		public const short Daybreak = 636;

		public const short BouncyDynamite = 637;

		public const short MoonlordBullet = 638;

		public const short MoonlordArrow = 639;

		public const short MoonlordArrowTrail = 640;

		public const short MoonlordTurret = 641;

		public const short MoonlordTurretLaser = 642;

		public const short RainbowCrystal = 643;

		public const short RainbowCrystalExplosion = 644;

		public const short LunarFlare = 645;

		public const short LunarHookSolar = 646;

		public const short LunarHookVortex = 647;

		public const short LunarHookNebula = 648;

		public const short LunarHookStardust = 649;

		public const short SuspiciousTentacle = 650;

		public const short WireKite = 651;

		public const short StaticHook = 652;

		public const short CompanionCube = 653;

		public const short GeyserTrap = 654;

		public const short BeeHive = 655;

		public const short SandnadoFriendly = 656;

		public const short SandnadoHostile = 657;

		public const short SandnadoHostileMark = 658;

		public const short SpiritFlame = 659;

		public const short SkyFracture = 660;

		public const short BlackBolt = 661;

		public const short DD2JavelinHostile = 662;

		public const short DD2FlameBurstTowerT1 = 663;

		public const short DD2FlameBurstTowerT1Shot = 664;

		public const short DD2FlameBurstTowerT2 = 665;

		public const short DD2FlameBurstTowerT2Shot = 666;

		public const short DD2FlameBurstTowerT3 = 667;

		public const short DD2FlameBurstTowerT3Shot = 668;

		public const short Ale = 669;

		public const short DD2OgreStomp = 670;

		public const short DD2DrakinShot = 671;

		public const short DD2ElderWins = 672;

		public const short DD2DarkMageRaise = 673;

		public const short DD2DarkMageHeal = 674;

		public const short DD2DarkMageBolt = 675;

		public const short DD2OgreSpit = 676;

		public const short DD2BallistraTowerT1 = 677;

		public const short DD2BallistraTowerT2 = 678;

		public const short DD2BallistraTowerT3 = 679;

		public const short DD2BallistraProj = 680;

		public const short DD2GoblinBomb = 681;

		public const short DD2LightningBugZap = 682;

		public const short DD2OgreSmash = 683;

		public const short DD2SquireSonicBoom = 684;

		public const short DD2JavelinHostileT3 = 685;

		public const short DD2BetsyFireball = 686;

		public const short DD2BetsyFlameBreath = 687;

		public const short DD2LightningAuraT1 = 688;

		public const short DD2LightningAuraT2 = 689;

		public const short DD2LightningAuraT3 = 690;

		public const short DD2ExplosiveTrapT1 = 691;

		public const short DD2ExplosiveTrapT2 = 692;

		public const short DD2ExplosiveTrapT3 = 693;

		public const short DD2ExplosiveTrapT1Explosion = 694;

		public const short DD2ExplosiveTrapT2Explosion = 695;

		public const short DD2ExplosiveTrapT3Explosion = 696;

		public const short MonkStaffT1 = 697;

		public const short MonkStaffT1Explosion = 698;

		public const short MonkStaffT2 = 699;

		public const short MonkStaffT2Ghast = 700;

		public const short DD2PetDragon = 701;

		public const short DD2PetGhost = 702;

		public const short DD2PetGato = 703;

		public const short DD2ApprenticeStorm = 704;

		public const short DD2PhoenixBow = 705;

		public const short DD2PhoenixBowShot = 706;

		public const short MonkStaffT3 = 707;

		public const short MonkStaffT3_Alt = 708;

		public const short MonkStaffT3_AltShot = 709;

		public const short DD2BetsyArrow = 710;

		public const short ApprenticeStaffT3Shot = 711;

		public const short BookStaffShot = 712;

		public const short DD2Win = 713;

		public const short Celeb2Weapon = 714;

		public const short Celeb2Rocket = 715;

		public const short Celeb2RocketExplosive = 716;

		public const short Celeb2RocketLarge = 717;

		public const short Celeb2RocketExplosiveLarge = 718;

		public const short QueenBeeStinger = 719;

		public const short FallingStarSpawner = 720;

		public const short DirtGolfBall = 721;

		public const short GolfClubHelper = 722;

		public const short ManaCloakStar = 723;

		public const short BeeCloakStar = 724;

		public const short StarVeilStar = 725;

		public const short StarCloakStar = 726;

		public const short RollingCactus = 727;

		public const short SuperStar = 728;

		public const short SuperStarSlash = 729;

		public const short ThunderSpear = 730;

		public const short ThunderStaffShot = 731;

		public const short ThunderSpearShot = 732;

		public const short ToiletEffect = 733;

		public const short VoidLens = 734;

		public const short Terragrim = 735;

		public const short BlueDungeonDebris = 736;

		public const short GreenDungeonDebris = 737;

		public const short PinkDungeonDebris = 738;

		public const short GolfBallDyedBlack = 739;

		public const short GolfBallDyedBlue = 740;

		public const short GolfBallDyedBrown = 741;

		public const short GolfBallDyedCyan = 742;

		public const short GolfBallDyedGreen = 743;

		public const short GolfBallDyedLimeGreen = 744;

		public const short GolfBallDyedOrange = 745;

		public const short GolfBallDyedPink = 746;

		public const short GolfBallDyedPurple = 747;

		public const short GolfBallDyedRed = 748;

		public const short GolfBallDyedSkyBlue = 749;

		public const short GolfBallDyedTeal = 750;

		public const short GolfBallDyedViolet = 751;

		public const short GolfBallDyedYellow = 752;

		public const short AmberHook = 753;

		public const short MysticSnakeCoil = 754;

		public const short BatOfLight = 755;

		public const short SharpTears = 756;

		public const short DripplerFlail = 757;

		public const short VampireFrog = 758;

		public const short BabyBird = 759;

		public const short BobberBloody = 760;

		public const short PaperAirplaneA = 761;

		public const short PaperAirplaneB = 762;

		public const short RollingCactusSpike = 763;

		public const short UpbeatStar = 764;

		public const short SugarGlider = 765;

		public const short KiteBlue = 766;

		public const short KiteBlueAndYellow = 767;

		public const short KiteRed = 768;

		public const short KiteRedAndYellow = 769;

		public const short KiteYellow = 770;

		public const short KiteWyvern = 771;

		public const short Geode = 772;

		public const short ScarabBomb = 773;

		public const short SharkPup = 774;

		public const short BobberScarab = 775;

		public const short ClusterRocketI = 776;

		public const short ClusterGrenadeI = 777;

		public const short ClusterMineI = 778;

		public const short ClusterFragmentsI = 779;

		public const short ClusterRocketII = 780;

		public const short ClusterGrenadeII = 781;

		public const short ClusterMineII = 782;

		public const short ClusterFragmentsII = 783;

		public const short WetRocket = 784;

		public const short WetGrenade = 785;

		public const short WetMine = 786;

		public const short LavaRocket = 787;

		public const short LavaGrenade = 788;

		public const short LavaMine = 789;

		public const short HoneyRocket = 790;

		public const short HoneyGrenade = 791;

		public const short HoneyMine = 792;

		public const short MiniNukeRocketI = 793;

		public const short MiniNukeGrenadeI = 794;

		public const short MiniNukeMineI = 795;

		public const short MiniNukeRocketII = 796;

		public const short MiniNukeGrenadeII = 797;

		public const short MiniNukeMineII = 798;

		public const short DryRocket = 799;

		public const short DryGrenade = 800;

		public const short DryMine = 801;

		public const short GladiusStab = 802;

		public const short ClusterSnowmanRocketI = 803;

		public const short ClusterSnowmanRocketII = 804;

		public const short WetSnowmanRocket = 805;

		public const short LavaSnowmanRocket = 806;

		public const short HoneySnowmanRocket = 807;

		public const short MiniNukeSnowmanRocketI = 808;

		public const short MiniNukeSnowmanRocketII = 809;

		public const short DrySnowmanRocket = 810;

		public const short BloodShot = 811;

		public const short ShellPileFalling = 812;

		public const short BloodNautilusTears = 813;

		public const short BloodNautilusShot = 814;

		public const short LilHarpy = 815;

		public const short FennecFox = 816;

		public const short GlitteryButterfly = 817;

		public const short WhiteTigerPounce = 818;

		public const short BloodArrow = 819;

		public const short ChumBucket = 820;

		public const short BabyImp = 821;

		public const short KiteBoneSerpent = 822;

		public const short KiteWorldFeeder = 823;

		public const short KiteBunny = 824;

		public const short BabyRedPanda = 825;

		public const short KitePigron = 826;

		public const short KiteManEater = 827;

		public const short KiteJellyfishBlue = 828;

		public const short KiteJellyfishPink = 829;

		public const short KiteShark = 830;

		public const short StormTigerGem = 831;

		public const short StormTigerAttack = 832;

		public const short StormTigerTier1 = 833;

		public const short StormTigerTier2 = 834;

		public const short StormTigerTier3 = 835;

		public const short DandelionSeed = 836;

		public const short BookOfSkullsSkull = 837;

		public const short KiteSandShark = 838;

		public const short KiteBunnyCorrupt = 839;

		public const short KiteBunnyCrimson = 840;

		public const short BlandWhip = 841;

		public const short RulerStab = 842;

		public const short KiteGoldfish = 843;

		public const short KiteAngryTrapper = 844;

		public const short KiteKoi = 845;

		public const short KiteCrawltipede = 846;

		public const short SwordWhip = 847;

		public const short MaceWhip = 848;

		public const short ScytheWhip = 849;

		public const short KiteSpectrum = 850;

		public const short ReleaseDoves = 851;

		public const short KiteWanderingEye = 852;

		public const short KiteUnicorn = 853;

		public const short Plantero = 854;

		public const short ReleaseLantern = 855;

		public const short SparkleGuitar = 856;

		public const short FirstFractal = 857;

		public const short DynamiteKitten = 858;

		public const short BabyWerewolf = 859;

		public const short ShadowMimic = 860;

		public const short Football = 861;

		public const short ClusterSnowmanFragmentsI = 862;

		public const short ClusterSnowmanFragmentsII = 863;

		public const short Smolstar = 864;

		public const short SquirrelHook = 865;

		public const short BouncingShield = 866;

		public const short Shroomerang = 867;

		public const short TreeGlobe = 868;

		public const short WorldGlobe = 869;

		public const short FairyGlowstick = 870;

		public const short HallowBossSplitShotCore = 871;

		public const short HallowBossLastingRainbow = 872;

		public const short HallowBossRainbowStreak = 873;

		public const short HallowBossDeathAurora = 874;

		public const short VoltBunny = 875;

		public const short ZapinatorLaser = 876;

		public const short JoustingLance = 877;

		public const short ShadowJoustingLance = 878;

		public const short HallowJoustingLance = 879;

		public const short ZoologistStrikeGreen = 880;

		public const short KingSlimePet = 881;

		public const short EyeOfCthulhuPet = 882;

		public const short EaterOfWorldsPet = 883;

		public const short BrainOfCthulhuPet = 884;

		public const short SkeletronPet = 885;

		public const short QueenBeePet = 886;

		public const short DestroyerPet = 887;

		public const short TwinsPet = 888;

		public const short SkeletronPrimePet = 889;

		public const short PlanteraPet = 890;

		public const short GolemPet = 891;

		public const short DukeFishronPet = 892;

		public const short LunaticCultistPet = 893;

		public const short MoonLordPet = 894;

		public const short FairyQueenPet = 895;

		public const short PumpkingPet = 896;

		public const short EverscreamPet = 897;

		public const short IceQueenPet = 898;

		public const short MartianPet = 899;

		public const short DD2OgrePet = 900;

		public const short DD2BetsyPet = 901;

		public const short CombatWrench = 902;

		public const short WetBomb = 903;

		public const short LavaBomb = 904;

		public const short HoneyBomb = 905;

		public const short DryBomb = 906;

		public const short OrnamentStar = 907;

		public const short TitaniumStormShard = 908;

		public const short RockGolemRock = 909;

		public const short DirtBomb = 910;

		public const short DirtStickyBomb = 911;

		public const short CoolWhip = 912;

		public const short FireWhip = 913;

		public const short ThornWhip = 914;

		public const short RainbowWhip = 915;

		public const short ScytheWhipProj = 916;

		public const short CoolWhipProj = 917;

		public const short FireWhipProj = 918;

		public const short FairyQueenLance = 919;

		public const short QueenSlimeMinionBlueSpike = 920;

		public const short QueenSlimeMinionPinkBall = 921;

		public const short QueenSlimeSmash = 922;

		public const short FairyQueenSunDance = 923;

		public const short FairyQueenHymn = 924;

		public const short StardustPunch = 925;

		public const short QueenSlimeGelAttack = 926;

		public const short PiercingStarlight = 927;

		public const short DripplerFlailExtraBall = 928;

		public const short ZoologistStrikeRed = 929;

		public const short SantankMountRocket = 930;

		public const short FairyQueenMagicItemShot = 931;

		public const short FairyQueenRangedItemShot = 932;

		public const short FinalFractal = 933;

		public const short QueenSlimePet = 934;

		public const short QueenSlimeHook = 935;

		public const short GelBalloon = 936;

		public const short VolatileGelatinBall = 937;

		public const short CopperShortswordStab = 938;

		public const short TinShortswordStab = 939;

		public const short IronShortswordStab = 940;

		public const short LeadShortswordStab = 941;

		public const short SilverShortswordStab = 942;

		public const short TungstenShortswordStab = 943;

		public const short GoldShortswordStab = 944;

		public const short PlatinumShortswordStab = 945;

		public const short EmpressBlade = 946;

		public const short Mace = 947;

		public const short FlamingMace = 948;

		public const short TorchGod = 949;

		public const short Count = 950;
	}
}
