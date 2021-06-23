/*
 * @Author: l hy 
 * @Date: 2021-06-23 18:54:12 
 * @Description: 战斗玩家数据
 */

public class BattlePlayerData {

	public int armor;

	public PlayerData _playerData;

	public int hp {
		get {
			return this._playerData.hpValue;
		}
		set {
			this._playerData.hpValue = value;
		}
	}

	public BattlePlayerData (PlayerData playerData) {
		this._playerData = playerData;
	}
}