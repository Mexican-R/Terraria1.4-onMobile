namespace Terraria.GameContent.Skies.CreditsRoll
{
	public interface ICreditsRollSegment
	{
		float DedicatedTimeNeeded
		{
			get;
		}

		void Draw(CreditsRollInfo info);
	}
}
