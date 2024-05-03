/*
Copyright (C) 2012 Anomalous Underdog

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using ThreadNinja;

[DefaultExecutionOrder(-999)]
public class DerivedMapControl : MapServer
{
	[SerializeField]
	GridDisplay _display;

	public float weightMap1=1f;
	public bool abs1;
	public MapServer map1;

	public float weightMap2=1f;
	public bool abs2;
	public MapServer map2;

	void CreateMap()
	{
		// how many of gridsize is in Mathf.Abs(_upperRight.positon.x - _bottomLeft.position.x)
		int width = (int)(Mathf.Abs(map1.GetUpperRight().position.x - map1.GetBottomLeft().position.x) / map1.GetGridSize());
		int height = (int)(Mathf.Abs(map1.GetUpperRight().position.z - map1.GetBottomLeft().position.z) / map1.GetGridSize());
		
		_influenceMap = new InfluenceMap(width, height, 0, 0);
		
		_display.SetGridData(_influenceMap);
		_display.CreateMesh(map1.GetBottomLeft().position, map1.GetGridSize());
	}

	public override Vector2I GetGridPosition(Vector3 pos)
	{
		int x = (int)((pos.x - map1.GetBottomLeft().position.x)/map1.GetGridSize());
		int y = (int)((pos.z - map1.GetBottomLeft().position.z)/map1.GetGridSize());

		return new Vector2I(x, y);
	}

	public override Transform GetUpperRight ()
	{
		return map1.GetUpperRight ();
	}

	public override Transform GetBottomLeft ()
	{
		return map1.GetBottomLeft ();
	}

	public override float GetGridSize ()
	{
		return map1.GetGridSize ();
	}
		
	void Awake()
	{
		CreateMap();	
	}
}
