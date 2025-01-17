using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	public class OneFromOptionsDropRule : IItemDropRule
	{
		private int[] _dropIds;

		private int _outOfY;

		private int _xoutOfY;

		public List<IItemDropRuleChainAttempt> ChainedRules
		{
			get;
			private set;
		}

		public OneFromOptionsDropRule(int outOfY, int xoutOfY, params int[] options)
		{
			_outOfY = outOfY;
			_xoutOfY = xoutOfY;
			_dropIds = options;
			ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			ItemDropAttemptResult result;
			if (info.player.RollLuck(_outOfY) < _xoutOfY)
			{
				CommonCode.DropItemFromNPC(info.npc, _dropIds[info.rng.Next(_dropIds.Length)], 1);
				result = default(ItemDropAttemptResult);
				result.State = ItemDropAttemptResultState.Success;
				return result;
			}
			result = default(ItemDropAttemptResult);
			result.State = ItemDropAttemptResultState.FailedRandomRoll;
			return result;
		}

		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)_xoutOfY / (float)_outOfY;
			float num2 = num * ratesInfo.parentDroprateChance;
			float dropRate = 1f / (float)_dropIds.Length * num2;
			for (int i = 0; i < _dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(_dropIds[i], 1, 1, dropRate, ratesInfo.conditions));
			}
			Chains.ReportDroprates(ChainedRules, num, drops, ratesInfo);
		}
	}
}
