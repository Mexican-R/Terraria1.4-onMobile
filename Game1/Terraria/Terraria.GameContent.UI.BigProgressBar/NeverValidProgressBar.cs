using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	public class NeverValidProgressBar : IBigProgressBar
	{
		public bool ValidateAndCollectNecessaryInfo(BigProgressBarInfo info)
		{
			return false;
		}

		public void Draw(BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
		}
	}
}
