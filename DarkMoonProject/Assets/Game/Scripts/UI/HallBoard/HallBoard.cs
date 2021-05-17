﻿/*
 * @Author: l hy 
 * @Date: 2020-12-06 11:03:09 
 * @Description: 大厅界面
 * @Last Modified by: l hy
 * @Last Modified time: 2021-05-17 21:54:50
 */

using System.Collections;
using System.Collections.Generic;
using UFramework.GameCommon;
using UnityEngine;

public class HallBoard : BaseUI {

    public void startGame_click () {
        AppContext.instance.uIManager.showBoard (UIPath.BattleBoard);
    }

    public void continueGame_click () {

    }

    public void showCardEditor_click () {
        AppContext.instance.uIManager.showBoard (UIPath.CardEditorBoard);
    }
}