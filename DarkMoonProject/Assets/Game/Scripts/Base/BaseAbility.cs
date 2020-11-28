/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour {

    protected int id = 1;

    protected abstract void effect ();

}