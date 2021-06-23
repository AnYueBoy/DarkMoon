/*
 * @Author: l hy 
 * @Date: 2021-06-23 18:43:53 
 * @Description: 玩家
 */

public class Player {

	private BattlePlayerData battlePlayerData;

	public Player () {
		this.battlePlayerData = new BattlePlayerData (AppContext.instance.playerDataManager.playerData);
	}

	public void damage (int damage) {
		int armor = this.battlePlayerData.armor;
		if (armor >= damage) {
			this.battlePlayerData.armor -= damage;
			return;
		}

		this.battlePlayerData.hp -= (damage - armor);
		this.battlePlayerData.armor = 0;
	}
}